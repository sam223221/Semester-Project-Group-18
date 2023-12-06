using System.Security.Cryptography.X509Certificates;

namespace WorldOfZuul
{
    public class Game 
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private Parser parser = new();
        private IChapter? currentChapter; // Declare as nullable if it can be null
        private bool continuePlaying = true;
        private List<Quest> currentChapterQuests = new List<Quest>();
        public int SocialScore { get; private set; }
        private List<IChapter> unlockedChapters;
        private List<Item> inventory;
        private Random random = new();

        public Game()
        {
            unlockedChapters = new List<IChapter>();
            UnlockChapter(new Chapter4Engineer()); // Assuming Chapter 4 is the starting chapter
            currentChapter = unlockedChapters.First(); // Ensure currentChapter is initialized
            StartChapter(currentChapter);
            inventory = new List<Item>();

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
        }

           private void UnlockChapter(IChapter chapter) => unlockedChapters.Add(chapter);







        // *** here is where the game logic is ***
        public void Play()
        {
            Console.Clear();
            Console.WriteLine(TextArtManager.GetTextArt(currentRoom?.ShortDescription));
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

            Console.WriteLine("Thank you for playing World of Zuul!");
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
                            
command list for see:

~ task
~ quest
~ socialscore
~ inventory
~ map
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
                case "quit":
                    continuePlaying = false;
                    break;
                case "next":
                if (command.SecondWord == "chapter")
                {
                    if (CanAdvanceToNextChapter())
                    {
                    UnlockNextChapter();
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
            // Example: Unlock Chapter 5 after Chapter 4
            if (currentChapter is Chapter4Engineer)
            {
                UnlockChapter(new ChapterExample());
            }
            if (currentChapter is ChapterExample)
            {
                UnlockChapter(new Chapter2Teacher());
            }
            // Add more conditions for other chapters
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
            string[] chapterList = new string[unlockedChapters.Count];

            for (int i = 0; i < unlockedChapters.Count; i++)
            {
                chapterList[i] = $"{i + 1}. {unlockedChapters[i].GetType().Name}";
            }

            string choice = "Choose a chapter:";
            int chapterIndex = InteractiveMenu.MultichoiceQuestion(choice, chapterList);

            // Ensure the selected index is within the range
            if (chapterIndex >= 0 && chapterIndex <= unlockedChapters.Count)
            {
                StartChapter(unlockedChapters[chapterIndex]);
                Console.Clear();
                Console.WriteLine(TextArtManager.GetTextArt(currentRoom?.ShortDescription));

            }
            else
            {
                Console.WriteLine("Invalid choice.");
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
                    Thread.Sleep(8000);
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
            Console.WriteLine(currentChapter.PlayerScore());
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
             "The goat is among the cleanest of animals, and is a much more selective feeder than cows, sheep, pigs, chickens and even dogs.\nGoats do eat many different species of plants, but do not want to eat food that has been contaminated or that has been on the floor or the ground.",
             "Cows have a memory of about three years.",
             "Cows are social animals who form bonds with each other. In a herd of cows, many will form cliques together.",
             "Female sheep are called ewes, male sheep are called rams, and baby sheep are called lambs.",
             "Ducklings are born ready to leave the nest within hours of hatching their eyes are open and they are able to find some of their own food.",
             "Sheep have two toes on each foot.",
             "Some breeds of chickens can lay colored eggs. The Ameraucana and Araucana can lay eggs of green or blue."
            };

            Console.WriteLine($"Fun fact :\n\n{facts[random.Next(facts.Length)]}\n");
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
            Console.WriteLine("~ Quit   :   This will end the game :(");
        }
    }
}