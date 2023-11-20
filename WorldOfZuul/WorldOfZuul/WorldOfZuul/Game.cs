namespace WorldOfZuul
{
    public class Game 
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private IChapter currentChapter;
        private bool questCheck = true;
        Parser parser = new();

        

        public Game()
        {
            StartChapter(new Chapter4Engineer());
        
        }
        private void StartChapter(IChapter chapter)
        {
            currentChapter = chapter;
            currentChapter.CreateRooms();
            currentRoom = currentChapter.GetStartRoom();
        }







        // ***vv here is where the game logic is vv***
        public void Play()
        {

            PrintWelcome();


            bool continuePlaying = true;
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





                if (command.Name == "chapter" && command.SecondWord == "change" && questCheck)
                {
                    ChooseChapter();
                }
                switch(command.Name)
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

            Console.WriteLine("Thank you for playing World of Zuul!");
        }






        public void ChooseChapter()
        {
            Console.WriteLine("Choose a chapter:");
            Console.WriteLine("1. Chapter 4 - Engineer");
            Console.WriteLine("2. Chapter 5 - Adventure\n");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    StartChapter(new Chapter4Engineer());
                    break;
                case "2":
                    StartChapter(new Chapter5Adventure());
                    break;

                default:
                    Console.WriteLine("Invalid choice. Starting Chapter 4 by default.");
                    StartChapter(new Chapter4Engineer());
                    break;
            }

            Console.WriteLine($"You have now found yourself in a new Chapter :) this is chapter {choice}");

        }




        private void Move(string direction)
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
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
