namespace WorldOfZuul
{
    public class Game 
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private IChapter? currentChapter; // Declare as nullable if it can be null
        private List<IChapter> unlockedChapters;
        private Parser parser = new();
        private bool continuePlaying = true;
        private List<Quest> allQuests = new List<Quest>();


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
            allQuests.Clear();
        
            unlockedChapters.Add(chapter);
            
            foreach (var quest in chapter.Quests)
            {
                allQuests.Add(quest);
            }
        }







        // ***vv here is where the game logic is vv***
        public void Play()
        {

            PrintWelcome();


            while (continuePlaying)
            {
                Console.WriteLine();
                Console.WriteLine();
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

                

            }

            Console.WriteLine("Thank you for playing World of Zuul!");
        }






        private void ProcessCommand(Command command)
        {

            
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
                    if (command.SecondWord == "task" || command.SecondWord == "tasks")
                    {
                        ShowRoomTasks();

                    }else if(command.SecondWord == "quest" || command.SecondWord == "quests")
                    {
                        ShowQuests();
                    }else
                    {
                    Console.WriteLine("you forgot to ask what you want to see or that is a invalid comand plese try again");
                        
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
            // Add more conditions for other chapters
        }

        private bool CanAdvanceToNextChapter()
        {
            // Iterate through all quests
            foreach (var quest in allQuests)
            {
                // Check if all tasks related to this quest are completed
                if (!AreAllTasksCompletedForQuest(quest))
                {
                    return false;
                }
            }
            return true;
        }

        private bool AreAllTasksCompletedForQuest(Quest quest)
        {
            // Iterate through all rooms to find tasks related to the quest
            foreach (var Quest in allQuests)
            {
                if(!Quest.IsCompleted)
                {
                    return false;
                }

            }
            return true;
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





        private void ExecuteTask(string taskName)
        {
            foreach (var task in currentRoom.Tasks)
            {
                if (task.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase) && !task.IsCompleted)
                {
                    task.Execute();
                    if (task.RelatedQuest.AreAllTasksCompleted())
                    {
                        task.RelatedQuest.IsCompleted = true;
                        Console.WriteLine($"Quest '{task.RelatedQuest.Name}' completed.");
                    }
                    return;
                }
            }
            Console.WriteLine("Task not found or already completed.");
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
            foreach (var quest in allQuests)
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
                Console.Clear();
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
                Console.WriteLine(currentRoom?.ShortDescription);
                Console.WriteLine(currentRoom?.TextArt);
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