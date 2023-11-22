﻿namespace WorldOfZuul
{
    public class Game 
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private IChapter? currentChapter; // Declare as nullable if it can be null
        private List<IChapter> unlockedChapters;
        private Parser parser = new();
        private bool continuePlaying = true;


        public Game()
        {
            unlockedChapters = new List<IChapter>();
            UnlockChapter(new Chapter4Engineer()); // Assuming Chapter 4 is the starting chapter
            currentChapter = unlockedChapters.First(); // Ensure currentChapter is initialized
            StartChapter(currentChapter);
        }
        private void StartChapter(IChapter chapter)
        {
            currentChapter = chapter;
            currentRoom = currentChapter.GetStartRoom();
        }

           private void UnlockChapter(IChapter chapter)
        {
            unlockedChapters.Add(chapter);
        }
























        // ***vv here is where the game logic is vv***
        public void Play()
        {

            PrintWelcome();


            while (continuePlaying)
            {
            
                
            
                Console.WriteLine(currentRoom?.ShortDescription);
                Console.Write("> ");

                // The start where we ask for input
                string? input = Console.ReadLine();

               

                // check if input has any input
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }
                
                //now we know that there is a input! we want to check if its valid
                Command? command = parser.GetCommand(input);

                // check if input is valid
                if (command == null)
                {
                    Console.WriteLine("I don't know that command, plese try somthing else :).");
                    continue;
                }



                ProcessCommand(command);

                if (currentChapter.CanAdvanceToNextChapter())
                {
                UnlockNextChapter();
                PromptForNextChapter();
                }
                

            }

            Console.WriteLine("Thank you for playing World of Zuul!");
        }





































        private void CompleteQuest(string? questName)
        {
            // Check if currentRoom is not null and it has quests
            if (currentRoom?.Quests != null)
            {
                // Find the quest with the specified name
                Quest? quest = currentRoom.Quests.Find(q => q.Name.Equals(questName, StringComparison.OrdinalIgnoreCase));

                // Check if the quest is found
                if (quest != null)
                {
                    // Check if the quest is not completed
                    if (!quest.IsCompleted)
                    {
                        quest.IsCompleted = true;
                        Console.WriteLine($"Quest '{quest.Name}' completed.");
                    }
                    else
                    {
                        Console.WriteLine("Quest already completed.");
                    }
                }
                else
                {
                    Console.WriteLine("No such quest found.");
                }
            }
            else
            {
                Console.WriteLine("There are no quests in this room or the room is not set.");
            }
        }





        private void UnlockNextChapter()
        {
            // Logic to unlock the next chapter
            // Example: Unlock Chapter 5 after Chapter 4
            if (currentChapter is Chapter4Engineer)
            {
                UnlockChapter(new Chapter5Adventure());
            }
            // Add more conditions for other chapters
        }




        public void ChooseChapter()
        {
            Console.WriteLine("Choose a chapter:");
            for (int i = 0; i < unlockedChapters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {unlockedChapters[i].GetType().Name}");
            }

            string? choice = Console.ReadLine();
            int chapterIndex;
            if (int.TryParse(choice, out chapterIndex) && chapterIndex > 0 && chapterIndex <= unlockedChapters.Count)
            {
                StartChapter(unlockedChapters[chapterIndex - 1]);
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
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





        private void ProcessCommand(Command command)
        {

            // Example of completing a quest
            if (command.Name == "complete" && command.SecondWord != null)
            {
                currentChapter.CompleteQuest(command.SecondWord);
            }

            
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
                        currentRoom = previousRoom;
                    break;

                // diractions command is processed here
                case "north":
                case "south":
                case "east":
                case "west":
                    Move(command.Name);
                    break;

                case "complete":
                    if(command.SecondWord != null)
                    {
                        CompleteQuest(command.SecondWord);
                    }
                    break;

                case "quests":
                    ShowRoomQuests();
                    break;


                // Quit command is processed here
                case "quit":
                    continuePlaying = false;
                    break;

                // Help command is processed here
                case "help":
                    PrintHelp();
                    break;

                default:
                    Console.WriteLine("I don't know that command, plese try somthing else :)\n");
                    break;
            }
        }





        private void ShowRoomQuests()
        {
            if (currentRoom?.Quests.Count > 0)
            {
                Console.WriteLine("Quests in this room:");
                foreach (var quest in currentRoom.Quests)
                {
                    string status = quest.IsCompleted ? "Completed" : "Pending";
                    Console.WriteLine($"- {quest.Name} ({status}): {quest.Description}");
                }
            }
            else
            {
                Console.WriteLine("There are no quests in this room.");
            }
        }





        private void Move(string direction)
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
                ShowRoomQuests();
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
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
            Console.WriteLine("You are lost. You are alone. You wander");
            Console.WriteLine("around the university.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }
    }
}