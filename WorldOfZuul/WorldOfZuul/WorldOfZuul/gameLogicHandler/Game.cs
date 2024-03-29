﻿using System.Security.Cryptography.X509Certificates;

namespace WorldOfZuul
{
    public class Game 
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private Parser parser = new();
        private IChapter? currentChapter; // Declare as nullable if it can be null
        private bool continuePlaying = true;
        private bool gameRunning = false;
        private List<Quest> currentChapterQuests = new List<Quest>();
        public int SocialScore { get; private set; }
        private List<IChapter> unlockedChapters;
        private List<Item> inventory;
        private Random random = new();

        public Game()
        {

            ShowMainMenu();
            if (gameRunning)
            {
            unlockedChapters = new List<IChapter>();
            UnlockChapter(new Chapter1Farmer()); // Assuming Chapter 4 is the starting chapter
            currentChapter = unlockedChapters.First(); // Ensure currentChapter is initialized
            StartChapter(currentChapter);
            inventory = new List<Item>();
            }

        }
        private void StartChapter(IChapter chapter)
        {
            currentChapter = chapter;
            currentChapter.CreateRoomsAndQuests();
            currentChapter.ShowIntroduction();
            currentRoom = currentChapter.GetStartRoom();
            previousRoom = null;
            currentChapterQuests.Clear();
            foreach (var quest in chapter.Quests)
            {
                
                currentChapterQuests.Add(quest);
            }
            Console.Clear();
            Console.WriteLine(TextArtManager.GetTextArt(currentRoom?.ShortDescription));
        }

           private void UnlockChapter(IChapter chapter) => unlockedChapters.Add(chapter);







        // *** here is where the game logic is ***
        public void Play()
        {
            while (continuePlaying)
            {

                // The start where we ask for input
                Console.Write("> ");
                string? input = Console.ReadLine();

               

                // check if input has any input
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }
                
                //check if its a valid command
                Command? command = parser.GetCommand(input);

                // check if input retuned null
                if (command == null)
                {
                    Console.WriteLine("I don't know that command, plese try somthing else :).");
                    continue;
                }


                Console.Clear();
                ProcessCommand(command);

                

            }

            Console.WriteLine("Thank you for playing Hope Rising, a new begining!");
        }






        private void ProcessCommand(Command command)
        {

            Console.WriteLine(TextArtManager.GetTextArt(currentRoom?.ShortDescription));

            switch (command.Name)
            {
                
                // Look command is processed here
                case "look":
                    Console.WriteLine(currentRoom?.LongDescription);
                    break;
                // back command is processed here
                case "back":
                    if (previousRoom == null)
                        Console.WriteLine("You can't go back from here!");
                    else
                    {
                        currentRoom = previousRoom;
                        previousRoom = null;
                        AnimateTravel(5000);

                    }

                    break;

                // diractions command is processed here
                case "north":
                case "south":
                case "east":
                case "west":
                    Move(command.Name);
                    break;

                case "do":
                    if (command.SecondWord != null)
                    {
                        ExecuteTask(command.SecondWord);

                    }else
                    {
                    Console.WriteLine("you forgot to ask what you want to do or that is a invalid comand plese try again");
                        
                    }
                    break;

                case "see":
                    switch (command.SecondWord)
                    {
                        case "task":
                        case "tasks":
                            ShowRoomTasks();
                            break;

                        case "quest":
                        case "quests":
                            ShowQuests();
                            break;

                        case "socialscore":
                            ShowSocialScore();
                            break;

                        case "inventory":
                            ShowInventory();
                            break;

                        case "map":
                            currentChapter.showMap(currentRoom);
                            break;

                        case null:
                            
                            Console.WriteLine(@"
                            
Command list for see:

~ Task
~ Quest
~ Socialscore
~ Inventory
~ Map
                            ");
                            break;

                        default:
                            Console.WriteLine("Invalid command or missing second word. Please try again.");
                            break;
                    }
                    break;

                    // Help command is processed here
                case "help":
                    PrintHelp();
                    break;

                    // Quit command is processed here
                case "menu":
                    ShowMainMenu();
                    break;
                case "next":
                if (command.SecondWord == "chapter")
                {
                    if (CanAdvanceToNextChapter())
                    {
                        UnlockNextChapter();

                        if (AreAllChaptersCompleted())
                        {
                            ShowOutro();
                            continuePlaying = false;
                            break;
                        }

                        PromptForNextChapter();

                    }else
                    {
                        Console.WriteLine("you are not done with all your quests! pleas do them first :)");
                    }
                

                }
                break;
                default:
                    Console.WriteLine("I don't know that command, plese try somthing else :)\n");
                    break;
            }
        }



        private void UnlockNextChapter()
        {

            if (currentChapter != null)
            {
                currentChapter.IsCompleted = true;
            }
            
            inventory.Clear();
            if (currentChapter is Chapter1Farmer)
            {
                UnlockChapter(new Chapter2Teacher());
            }
            if (currentChapter is Chapter2Teacher)
            {
                UnlockChapter(new Chapter3Medic());
            }if (currentChapter is Chapter3Medic)
            {
                UnlockChapter(new Chapter4Engineer());
            }
        }

        private bool CanAdvanceToNextChapter()
        {
            // Iterate through all quests
            foreach (var quest in currentChapterQuests)
            {
                // Check if all tasks related to this quest are completed
                if (!AreAllTasksCompletedForQuest(quest))
                {
                    return false;
                }
            }
            return true;
            
        }
        public void ChooseChapter()
        {
            List<string> chapterList = new List<string>();

            for (int i = 0; i < unlockedChapters.Count; i++)
            {
                string chapterStatus = unlockedChapters[i].IsCompleted ? "(completed)" : "";
                chapterList.Add($"{i + 1}. {unlockedChapters[i].GetType().Name} {chapterStatus}");
            }

            string choice = "Choose a chapter:";
            int chapterIndex = InteractiveMenu.MultichoiceQuestion(choice, chapterList.ToArray());

            if (chapterIndex >= 0 && chapterIndex < unlockedChapters.Count && !unlockedChapters[chapterIndex].IsCompleted)
            {
                StartChapter(unlockedChapters[chapterIndex]);
                Console.Clear();
                Console.WriteLine(TextArtManager.GetTextArt(currentRoom?.ShortDescription));
            }
            else
            {
                Console.WriteLine("Invalid choice or chapter already completed.");
            }
        }

        public void AddItemToInventory(Item item)
        {
            inventory.Add(item);
            Console.WriteLine($"Added {item.Name} to your inventory.");
        }

        private void ShowInventory()
        {
            if (inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                Console.WriteLine("Items in your inventory:");
                foreach (var item in inventory)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }
        }

        private bool AreAllTasksCompletedForQuest(Quest quest)
        {
            // Iterate through all rooms to find tasks related to the quest
            foreach (var Quest in currentChapterQuests)
            {
                if(!Quest.IsCompleted)
                {
                    return false;
                }

            }
            return true;
        }




        private void PromptForNextChapter()
        {
            Console.WriteLine("You have completed all quests in this chapter.");
            Console.WriteLine("Would you like to advance to the next chapter? (yes/no)");
            string? response = Console.ReadLine();
            if (response?.ToLower() == "yes")
            {
                ChooseChapter();
            }
        }
        private bool AreAllChaptersCompleted()
        {
            return unlockedChapters.All(chapter => chapter.IsCompleted);
        }

        private void ShowOutro()
        {
            Console.Clear();
            Printer.PrintLine("Congratulations on completing your journey in Hope Rising, a new begining!");
            Printer.PrintLine($"Your final social score is: {SocialScore}");

            if (SocialScore > 100)
            {
                Printer.PrintLine("You've become a legend and will be rememberd as the hero that came to save there future.");
            }
            else if (SocialScore > 50)
            {
                Printer.PrintLine("You've made a positive impact, and are leaving a trail of good deeds behind.");
            }
            else if (SocialScore > 0)
            {
                Printer.PrintLine("You've had a fair journey, with both ups and downs along the way.");
                Printer.PrintLine("But it really seems that you are not made for right choses");
            }
            else
            {
                Printer.PrintLine("Your journey was challenging, with many tough decisions and hardships.");
            }

            Printer.PrintLine("\nThank you for playing! Press any key to exit.");
            Console.ReadKey();
            continuePlaying = false;
        }


        private void ExecuteTask(string taskName)
        {
            foreach (var task in currentRoom.Tasks)
            {
                if (task.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase) && !task.IsCompleted)
                {
                   if (task.CanExecute(inventory))
                   {
                    
                        int scoreChange = task.Execute(this);
                        SocialScore += scoreChange;
                        Console.WriteLine($"\nTask '{task.Name}' executed.\n\nSocial score changed by {scoreChange}. Current social score: {SocialScore}\n\n");

                        if (task.RelatedQuest.AreAllTasksCompleted())
                        {
                            task.RelatedQuest.IsCompleted = true;
                            Console.WriteLine($"Quest '{task.RelatedQuest.Name}' completed.\n");
                        }
                   
                   }else
                   {

                    Console.WriteLine($"You need {task.RequiredItem?.Name} to perform this task.\n");
                    if (task.RequiredItem.WhereTofindItem != null)
                    {
                        Console.WriteLine($"{task.RequiredItem.WhereTofindItem}");
                    }
                   }
                    Thread.Sleep(5000);
                    Console.Clear();
                    Console.WriteLine(TextArtManager.GetTextArt(currentRoom?.ShortDescription));
                    return;
                }
            }
            Console.WriteLine("Task not found or already completed.");
        }


        
        
                private void ShowSocialScore()
        {
            Console.WriteLine($"Your current social score is: {SocialScore}");
        }

        private void ShowRoomTasks()
        {
            if (currentRoom != null && currentRoom.Tasks.Count > 0)
            {
                Console.WriteLine("Tasks in this room:");
                foreach (var task in currentRoom.Tasks)
                {
                    string status = task.IsCompleted ? "Completed" : "Available";
                    Console.WriteLine($"- {task.Name} ({status}): {task.Description}");
                }
            }else
            {
                Console.WriteLine("There are no tasks in this room.");
            }
        }



        private void ShowQuests()
        {
            foreach (var quest in currentChapterQuests)
            {
                Task currentTask = quest.GetCurrentTask();
                if (currentTask != null)
                {
                    Console.WriteLine($"{quest.Name}: Current task is in {currentTask.Room.ShortDescription} [{quest.Tasks.Count(t => t.IsCompleted)}/{quest.Tasks.Count}]");
                }
                else
                {
                    Console.WriteLine($"{quest.Name}: All tasks completed.");
                }
            }
        }





        private void Move(string direction)
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                Room nextRoom = currentRoom.Exits[direction];
                if (nextRoom.CanEnter(inventory))
                {
                    previousRoom = currentRoom;
                    currentRoom = nextRoom;
                    AnimateTravel(5000); 
                    
                }
                else
                {
                    Console.WriteLine($"The room is locked. You need {nextRoom.RequiredItem?.Name} to enter.");
                }
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }

        private void AnimateTravel(int totalDurationMilliseconds)
        {
            Console.Clear();
            string[] sequence = { "/", "-", "\\", "|" ,"|"}; // Simple spinning line animation
            int cycles = totalDurationMilliseconds/1000;
            string[] facts =  
            {
             "Sheep make a bleating sound. A baby lamb can identify its mother by her bleat.",
             "Cows have a memory of about three years.",
             "Cows are social animals who form bonds with each other. In a herd of cows, many will form cliques together.",
             "Female sheep are called ewes, male sheep are called rams, and baby sheep are called lambs.",
             "Ducklings are born ready to leave the nest within hours of hatching their eyes are open and they are able to find some of their own food.",
             "Sheep have two toes on each foot.",
             "Some breeds of chickens can lay colored eggs. The Ameraucana and Araucana can lay eggs of green or blue.",
             "The longest lecture on record lasted for over 139 hours! It was delivered by Dr. Kavan Ratnatunga in Sri Lanka in 2012, covering topics in astronomy.",
             "The University of Bologna in Italy, founded in 1088, is recognized as the world's oldest university still in operation.",
             "The average pencil can draw a line 35 miles long or write approximately 45,000 words.",
             "Smallpox is the only human disease to have been eradicated globally. In 1980, the World Health Assembly declared the world free of smallpox.",
             "The human body has the incredible ability to repair itself. For example, the liver can regenerate lost tissue, and broken bones can knit back together.",
             "The first successful human organ transplant was a kidney transplant performed in 1954 by Dr. Joseph Murray and his team.",
             "The Great Pyramid of Giza, built around 2560 BCE, is an engineering marvel that aligns with incredible precision to the cardinal points of the compass.",
             "In hot weather, the Eiffel Tower expands, causing the metal structure to grow taller. Conversely, in cold weather, it contracts.",
             "The Antikythera mechanism, an ancient Greek analog computer dating back to around 100 BCE, is considered the world's first known mechanical computer.",
             "The Apollo 11 mission that landed humans on the moon in 1969 relied on a computer with less processing power than a modern smartphone.",
             "Wind turbines have been used for centuries to pump water and grind grain. In modern times, they are harnessed for generating electricity from wind energy.",
             "The design of the eggshell is an engineering marvel. Despite being fragile, an egg can withstand a surprising amount of pressure due to its arched shape",
             "The Kuranui Primary School in New Zealand, established in 1867, is recognized as the world's oldest operating school.",
             "The Library of Congress in the United States has the world's largest catalog of books and other media, with over 168 million items.",
             "The best movie ever from an objective point of view was Shrek created in 2001!",
             "There are over 7,500 known varieties of apples, each with its own unique taste and characteristics.",
             "Bees are crucial for pollinating crops. About one-third of the world's food production depends on pollinators, primarily bees.",
             "Pumpkins are believed to have originated in North America over 7,000 years ago.",
             "Rice is a staple food for over half of the world's population.",
             "Howard Berg holds the Guinness World Record for the fastest reader, reading over 25,000 words per minute.",
             "The scent of old books is due to the breakdown of compounds in the paper, releasing a mix of pleasant odors known as bibliosmia",
             "Aspirin, one of the most widely used medications, was originally derived from willow bark and has been used for centuries to relieve pain.",
             "The human body contains about 60,000 miles (96,560 km) of blood vessels.",
             "The human body generates enough heat in 30 minutes to boil half a gallon (about 2 liters) of water.",

            };

            Printer.PrintLine($"Fun fact :\n\n{facts[random.Next(facts.Length)]}\n");
            for (int i = 0; i < cycles; i++)
            {
                foreach (var s in sequence)
                {
                    Console.Write(s);
                    Thread.Sleep(200); // Adjust sleep time to maintain total duration
                    Console.Write("\b"); // Backspace to overwrite the character
                }
            }
            Console.Clear();
            Console.WriteLine(TextArtManager.GetTextArt(currentRoom?.ShortDescription));


        }

        public void ShowMainMenu()
        {
            bool exitMenu = false;


            while (!exitMenu)
            {
                Console.Clear();
                    string header ="Welcome to Hope Rising, a new begining!";
                    string[] options = {"Start New Game","Continue Game","Tutorial","Exit"};
                Console.Write("\nEnter your choice: ");
                int choice = InteractiveMenu.MultichoiceQuestion(header,options);

                switch (choice)
                {
                    case 0:
                        
                        if (!gameRunning)
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                Console.Clear();
                                
                                Console.WriteLine("Starting a new game...");
                            
                                for (int ii = 0; ii < 5; ii++)
                                    {
                                        Console.Write("#");
                                        Thread.Sleep(500);
                                    }
                                
                            }
                            gameRunning = true;
                            return;
                        }else
                        {
                            Console.WriteLine("A game is currently running");
                            Console.ReadKey();
                        }
                        break;

                    case 1:

                        if (gameRunning)
                        {

                        Console.WriteLine("Continuing game...");
                        Thread.Sleep(2000);
                        Console.Clear();
                        Console.WriteLine(TextArtManager.GetTextArt(currentRoom?.ShortDescription));
                        return;

                        }else
                        {
                            Console.WriteLine("there is no game running");
                            Console.ReadKey();
                        }
                        break;

                    case 2:
                        Tutorial();
                        break;
                    case 3:
                        continuePlaying = false;
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }



        public void Tutorial()
        {
            // Introduction to the game
            ShowSlide("Welcome to the Hope Rising, a new begining! This tutorial will guide you through the basics of the game.");

            // Basic controls
            ShowSlide("To move around, use the commands: north, south, east, and west.");

            // Interaction commands
            ShowSlide("To interact with objects characters or tasks, use commands:  'Do' folowed with the name of the task\nExample : \nTask: Quiz\n> Do Quiz \n\nThis would execute the task");

            // Quests and tasks
            ShowSlide("Throughout your journey, you will encounter various quests and tasks. Complete them to progress in the game.");

            // Inventory management
            ShowSlide("You can carry items in your inventory. Use the 'see inventory' command to view your items.");

            // Ending the tutorial
            ShowSlide("That's the end of the tutorial. if you need any Help just type 'Help' then you will be show what comands you can use\nYour adventure awaits!");

        }

    private void ShowSlide(string text)
    {
        Console.Clear();
        Printer.PrintLine(text);
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }



        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the World of Zuul!");
            Console.WriteLine("World of Zuul is a new, incredibly boring adventure game.");
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("I see that you need help");
            Console.WriteLine("here are the comands that you can use");
            Console.WriteLine();
            Console.WriteLine("To navigate type 'north', 'south', 'east', 'west'");
            Console.WriteLine();
            Console.WriteLine("~ Do     :   executes the task you want to do, demo 'do (task name)'");
            Console.WriteLine("~ Look   :   Gives you a description of your surrounding");
            Console.WriteLine("~ Back   :   Makes you go back to the room you just came from");
            Console.WriteLine("~ See    :   Type 'see' to see what comands are related to it");
            Console.WriteLine("~ Help   :   To see this message XD");
            Console.WriteLine("~ Menu   :   This will send you to the Menu");
            Console.WriteLine();
            Console.WriteLine("~ Next Chapter   :  when you are done with all quest write this to advance to next chapter");
        }
    }
}