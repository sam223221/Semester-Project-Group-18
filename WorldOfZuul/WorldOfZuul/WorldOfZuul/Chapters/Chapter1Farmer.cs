using System;

namespace WorldOfZuul
{
    public class Chapter1Farmer : IChapter // Ensure it implements IChapter
    {
        Introduction intro = new Introduction();
        public List<Room> Rooms { get; private set; }
        public List<Quest> Quests { get; set; }
        private Room? village;
        private Room? house;
        private Room? market;
        private Room? farm;
        private Room? lake;
        private Room? watermill;
        private int farmerScore = 0;

        public Chapter1Farmer()
        {
            Rooms = new List<Room>();
            Quests = new List<Quest>();
            CreateRoomsAndQuests();
            ShowIntroduction();
        }


        public void ShowIntroduction()
        {
            intro.PlayIntro();
            Console.ReadKey();
        }

        public Room GetStartRoom() => village;

        public void CreateRoomsAndQuests()
        {
            // Initialize rooms

            village = new Room("Village", "you are at the center of your beautiful village!");
            house = new Room("House", "House is your knowledge temple... You better use it!");
            farm = new Room("Farm", "Welcome to the farm... Mr. Farmer!");
            market = new Room("Market", "You are on the village market... Look around!");
            lake = new Room("Lake", "What you see is a lovely lake right in front of you!");
            watermill = new Room("Watermill", "A watermill owned by your friend... What could be found there?");

            // Set exits -> (north, east, south, west) & Set exit -> ("north/east/south/west", "place")

            lake.SetExit("east", house);
            house.SetExits(null, village, null, lake);
            village.SetExits(null, farm, market, house);
            farm.SetExits(null, null, watermill, market);
            market.SetExits(village, watermill, null, null);
            watermill.SetExits(farm, null, null, market);


            // Create quests (short desc, long desc)

            Quest houseQuest = new Quest("Quizzes", "Finish the quizes about sustainability!");

            /* Initialize tasks public Task(string name, string description, Quest relatedQuest , Room room , TaskAction action, Item? requiredItem = null, Item? rewardItem = null)*/

            // houseCourseSDGTask = new Task("SDGCourse", "Might be useful for \ncompleting the quiz...", houseQuest, house, SDGCourseTask);
            Task houseQuizSDGTask = new Task("SDGQuiz", "Complete a Quiz about \nSustainable Development Goals.", houseQuest, house, SDGQuizTask);

            // Add quests to the chapter's quest list
            Quests.Add(houseQuest);

            // Add task to the quest list

            //houseQuest.AddTask(houseCourseSDGTask);
            houseQuest.AddTask(houseQuizSDGTask);


            // Add task to the room

            house.AddTask(houseQuizSDGTask);
            //   house.AddTask(houseCourseSDGTask);

            // Adding things to rooms

            Rooms.Add(lake);
            Rooms.Add(house);
            Rooms.Add(village);
            Rooms.Add(farm);
            Rooms.Add(market);
            Rooms.Add(watermill);

        }

        // Optional: farmerScore
        public string PlayerScore()
        {
            return $@"Your farmer score is : {farmerScore}";
        }

        /******************task section*********************/

        private int SDGCourseTask()
        {
            return 5;
        }


        private int SDGQuizTask()
        {
            SDGQuiz sdgquiz = new();

            if (sdgquiz.correctAnswersCount == 5)
            {
                return 5;
            }

            return 0;

        }




        /******************map*********************/
        public void showMap(Room currentRoom)
        {
            string lake = "     ";
            string house = "     ";
            string village = "     ";
            string farm = "     ";
            string market = "     ";
            string watermill = "     ";

            // Mark the current room
            switch (currentRoom.ShortDescription)
            {
                case "Lake":
                    lake = "*You*";
                    break;
                case "House":
                    house = "*You*";
                    break;
                case "Village":
                    village = "*You*";
                    break;
                case "Farm":
                    farm = "*You*";
                    break;
                case "Market":
                    market = "*You*";
                    break;
                case "Watermill":
                    watermill = "*You*";
                    break;
            }
            string map = $@"
            +---------------+         +---------------+          +---------------+          +---------------+
            |               |         |               |          |               |          |               |
            |     Lake      +---------+     House     +----------+    Village    +----------+     Farm      |
            |     {lake}     |         |     {house}     |          |     {village}     |          |     {farm}     |
            +---------------+         +---------------+          +-------+-------+          +-------+-------+ 
                                                                         |                          |
                                                                         |                          |
                                                                         |                          |
                                                                         |                          |
                                                                 +-------+-------+          +-------+-------+
                                                                 |               |          |               |
                                                                 +    Market     +----------+   Water Mill  |
                                                                 |     {market}     |          |     {watermill}     |
                                                                 +---------------+          +---------------+


                        ";


            Console.WriteLine(map);
        }






        /******************introduction*********************/

        public class Introduction
        {
            public bool introplay = true;
            public string farmerName = "";
            public string farmerBackstory = "";
            private string? FarmerName { get; set; }

            //Encapsulates Introduction to one method

            public void PlayIntro()
            {
                while (introplay)
                {
                    IntroPart1();
                    Console.Clear();
                    IntroPart2();
                    Console.Clear();
                    IntroPart3();
                    Console.Clear();
                    introplay = false;
                }
            }

            //***Methods of different introduction parts

            //Part 1 of Introduction
            void IntroPart1()
            {
                TitleAnimation("The Farmer");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("TextArt 'farmerText' here");
                //Console.WriteLine(textArts.farmerText);                   TEXT ART HERE!!!
                int numberOfLines = 3;
                for (int i = 0; i < numberOfLines; i++)
                {
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tYou are about to start your farmer's journey!");
                Console.WriteLine();
                GetFarmerName();
                int numberOfLines2 = 2;
                for (int i = 0; i < numberOfLines2; i++)
                {
                    Console.WriteLine();
                }
                Console.WriteLine($"Cool name {FarmerName}... I hope you are ready!");
                Console.WriteLine();
                Console.WriteLine("Now press any key to start the game.");
                Console.ReadKey();
            }

            //Part 2 of Introduction
            void IntroPart2()
            {
                Console.WriteLine();
                Console.WriteLine("TextArt 'farmerArtStory' here");
                //Console.Write(textArts.farmerArtStory);    TEXT ART HERE
            }

            //Part 3 of Introduction
            void IntroPart3()
            {
                Console.WriteLine("TextArt 'farmerBackstory' here");
                // Console.Write(textArts.farmerBackstory);     TEXT ART HERE
                GetFarmerBackStory();
                Console.ReadKey();
            }

            //*** Other Methods:

            //Animated Title
            static void TitleAnimation(string titleText)
            {
                Console.Title = "";
                string Progressbar = "The Farmer";
                var title = "";

                for (int i = 0; i < Progressbar.Length; i++)
                {
                    title += Progressbar[i];
                    Console.Title = title;
                    Thread.Sleep(100);
                }
            }

            //Collects a Farmer Name
            public void GetFarmerName()
            {
                Console.WriteLine("\t\tLet's start with your character's name:");
                Console.WriteLine();
                string? farmerName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(farmerName))
                {
                    Console.WriteLine("Please enter a valid name with letters only.");
                    GetFarmerName();
                }
                else if (farmerName.All(char.IsLetter) && farmerName.Length >= 3 && farmerName.Length <= 10)
                {
                    FarmerName = farmerName;
                }
                else
                {
                    Console.WriteLine("Please enter a valid name with alphabetic characters between 3 and 10 characters.");
                    GetFarmerName();
                }
            }

            //Collects a Farmer's Backstory
            string GetFarmerBackStory()
            {
                static int ReadIntFromConsole(string prompt)
                {
                    Console.WriteLine(prompt);
                    string? userInput = Console.ReadLine();

                    if (int.TryParse(userInput, out int result))
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1, 2, or 3.");
                        return ReadIntFromConsole(prompt);
                    }
                }

                int userChoice;

                do
                {
                    userChoice = ReadIntFromConsole("");

                    if (userChoice == 1)
                    {
                        farmerBackstory = "Former Environmental Activist";
                    }
                    else if (userChoice == 2)
                    {
                        farmerBackstory = "Family Tradition Farmer";
                    }
                    else if (userChoice == 3)
                    {
                        farmerBackstory = "Former Wanderer";
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
                    }
                } while (userChoice != 1 && userChoice != 2 && userChoice != 3);

                Console.WriteLine();
                Console.WriteLine($"You've selected: {farmerBackstory}.");
                return farmerBackstory;
            }

        }


        /******************sdg quiz*************************/


        public class SDGQuiz
        {

            public interface IQuizQuestionSDG
            {
                string Question { get; }
                string CorrectAnswer { get; }
                bool CheckAnswer(string userAnswer);
            }

            public class SDGQuizQuestion : IQuizQuestionSDG
            {
                public string Question { get; }
                public string CorrectAnswer { get; }

                public SDGQuizQuestion(string question, string correctAnswer)
                {
                    Question = question;
                    CorrectAnswer = correctAnswer;
                }

                public bool CheckAnswer(string userAnswer)
                {
                    return userAnswer.Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase);
                }
            }


            private List<IQuizQuestionSDG> quizQuestions;
            public int correctAnswersCount;

            public SDGQuiz()
            {
                quizQuestions = new List<IQuizQuestionSDG>
        {
new SDGQuizQuestion("SDG 1 (No *over*y:)", "no poverty"),
new SDGQuizQuestion("SDG 2 (Z**o *un**r):", "zero hunger"),
new SDGQuizQuestion("SDG 3 (G**d **alth and W**l-b***g):", "good health and well-being"),
new SDGQuizQuestion("SDG 4 (Qua**t* *d*ca*i*n):", "quality education"),
new SDGQuizQuestion("SDG 5 (G*n**r **uali*y):", "gender equality"),
new SDGQuizQuestion("SDG 6 (Cl**n *a**r and S*n**a*ion):", "clean water and sanitation"),
new SDGQuizQuestion("SDG 7 (A*f*rda*l* and Cl**n *n**gy):", "affordable and clean energy"),
new SDGQuizQuestion("SDG 8 (D*c*nt **rk and *co*omi* *ro*th):", "decent work and economic growth"),
new SDGQuizQuestion("SDG 9 (**dus**y, *nn*v*tion, and **frastruc**r*):", "industry, innovation, and infrastructure"),
new SDGQuizQuestion("SDG 10 (R**uc*d Ine***lit*):", "reduced inequality"),
new SDGQuizQuestion("SDG 11 (Su***inabl* *i*ies and ***muniti*s):", "sustainable cities and communities"),
new SDGQuizQuestion("SDG 12 (**sp*nsibl* **ns*mp*ion and ***duc*ion):", "responsible consumption and production"),
new SDGQuizQuestion("SDG 13 (**ima*e **tion):", "climate action"),
new SDGQuizQuestion("SDG 14 (*if* **low *a*er):", "life below water"),
new SDGQuizQuestion("SDG 15 (*if* on *and):", "life on land"),
new SDGQuizQuestion("SDG 16 (**ace, J*stic*, and **rong ***tituti**s):", "peace, justice, and strong institutions"),
new SDGQuizQuestion("SDG 17 (*artn*rs**ps for the *oals):", "partnerships for the goals"),
        };
                correctAnswersCount = 0;
            }

            public void PrintQuizTxt()
            {
                Console.WriteLine("Let me test your knowledge of Sustainable Development Goals!");
                Console.WriteLine("Do you know what each of them stands for?");
                Console.WriteLine();
            }
            public void StartSDGQuiz()
            {
                PrintQuizTxt();

                Random random = new Random();
                quizQuestions.Sort((x, y) => random.Next(-1, 2));

                foreach (var question in quizQuestions)
                {
                    Console.WriteLine(question.Question);
                    Console.WriteLine();
                    Console.Write("Your answer: ");
                    string? userAnswer = Console.ReadLine();

                    if (question.CheckAnswer(userAnswer))
                    {
                        Console.Clear();
                        PrintQuizTxt();
                        Console.WriteLine("Correct!\n");

                        correctAnswersCount++;

                        if (correctAnswersCount == 5)
                        {
                            Console.Clear();
                            PrintQuizTxt();
                            Console.WriteLine("Congratulations! You've answered 5 questions correctly.");
                            Console.WriteLine("This quiz is completed!");
                            break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        PrintQuizTxt();
                        Console.WriteLine($"Incorrect. The correct answer was: {question.CorrectAnswer}\n");
                    }
                }
            }
        }



    }
}