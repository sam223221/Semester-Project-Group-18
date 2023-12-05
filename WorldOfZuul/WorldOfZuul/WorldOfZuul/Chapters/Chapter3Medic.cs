using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace WorldOfZuul
{
    public class Chapter3Medic : IChapter // Ensure it implements IChapter
    {
        public List<Room> Rooms { get; private set; }
        public List<Quest> Quests { get; set; }
        private Room? hospital;
        private Room? VillageCenter;
        private Room? forest;
        private Room? church;
        private Room? market;
        //private Room? lab;
        private int medicScore = 0;

        MedicIntroduction medicIntroduction = new MedicIntroduction();

        public Chapter3Medic()
        {
            Rooms = new List<Room>();
            Quests = new List<Quest>();
        }

        public void ShowIntroduction()
        {
            medicIntroduction.Intro();

            Console.ReadKey();
        }

        public Room GetStartRoom() => VillageCenter;


        public void CreateRoomsAndQuests()
        {

            // Create items
            Item officeKey = new Item("officeKey", "these keys are for opning a back dor in the office");


            // Initialize rooms
            VillageCenter = new Room("Village Center", "\nYou stand in the heart of a charming village. Cobblestone paths lead in different directions. To the east is a bustling market, to the south is a quaint church, and to the west is a peaceful forest.");

            hospital = new Room("Hospital", "\nYou find yourself in a sterile environment filled with the scent of antiseptic. White walls surround you, and the sound of distant footsteps echoes through the corridors.");

            forest = new Room("Forest", "\nYou step into a dense and enchanting forest. Tall trees tower above, creating a canopy of leaves that filters sunlight. Birds chirp in the distance, and the air is filled with the scent of pine.");

            church = new Room("Church", "\nAs you enter, the atmosphere becomes serene. Stained glass windows filter colorful light into the church. Wooden pews line the aisles, and silence is broken only by the occasional creaking of old wooden floorboards.");

            market = new Room("Market", "\nThe hustle and bustle of a vibrant market surround you. Stalls are filled with colorful fruits, vegetables, and various goods. Merchants call out their prices, creating a lively and dynamic atmosphere.");


            // Set exits

            VillageCenter.SetExits(church, forest, market, hospital);
            
            church.SetExit("morth", VillageCenter);
            
            hospital.SetExit("east", VillageCenter);
            
            market.SetExit("south", VillageCenter);
            
            forest.SetExit("west", VillageCenter);

            /***************** QUEST SECTION ****************/

            //Create quest (short and long description)

            Quest ForestQuest = new Quest("Battle", "Defeat the sickness, You've received information from the mayor of the village " +
                                                                          "that the source of the disease was in the forest. You should nip this in the bud...");

            Quest HospitalQuest = new Quest("Quiz", "You need to finish this quiz to prepare for healing citizens!");


            //Initialize tasks

            Task DefeatingSickness = new Task("Sickness", "You need to free villagers from the sickness", ForestQuest, forest, BattleWithSickness);

            Task MedicalQuiz = new Task("MedicalQuiz", "Go on and finish this quiz!", HospitalQuest, hospital, QuizInHospital);

            //Add quests to the chapters quests lists

            Quests.Add(ForestQuest);
            Quests.Add(HospitalQuest);

            // Add task to the quest list

            ForestQuest.AddTask(DefeatingSickness);
            HospitalQuest.AddTask(MedicalQuiz);

            //Add task to the room

            forest.AddTask(DefeatingSickness);
            hospital.AddTask(MedicalQuiz);


            // Adding thigs to rooms
            Rooms.Add(church);
            Rooms.Add(hospital);
            Rooms.Add(market);
            Rooms.Add(forest);

        }
        public string PlayerScore()
        {
            return $@"Your Medic score is : {medicScore}";
        }

        /****************** TASK SECTION *********************/

        private int BattleWithSickness()
        {
            // Medic stats
            int medicHealth = 100;
            int medicAttackPower = 15;
            int medicHealingPower = 20;

            // Sickness stats
            int sicknessHealth = 50;
            int sicknessAttackPower = 10;

            Console.WriteLine("The battle begins!\n");

            while (medicHealth > 0 && sicknessHealth > 0)
            {
                Console.WriteLine(TextArtManager.GetTextArt("MedicVirus"));
                
                Console.WriteLine($"Medic Health: {medicHealth} | Sickness Health: {sicknessHealth}\n");

                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Attack the Sickness");
                Console.WriteLine("2. Heal");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        // Medic attacks the sickness
                        int damageDealt = new Random().Next(medicAttackPower / 2, medicAttackPower + 1);
                        sicknessHealth -= damageDealt;
                        Console.WriteLine($"You attacked the sickness and dealt {damageDealt} damage.");

                        // Sickness attacks back
                        int damageReceived = new Random().Next(sicknessAttackPower / 2, sicknessAttackPower + 1);
                        medicHealth -= damageReceived;
                        Console.WriteLine($"The sickness retaliated and dealt {damageReceived} damage.");

                        Thread.Sleep(3500);
                        Console.Clear();
                        break;

                    case "2":
                        // Medic chooses to heal
                        int healingAmount = new Random().Next(medicHealingPower / 2, medicHealingPower + 1);
                        medicHealth += healingAmount;
                        Console.WriteLine($"You healed yourself and gained {healingAmount} health.");

                        // Sickness attacks
                        damageReceived = new Random().Next(sicknessAttackPower / 2, sicknessAttackPower + 1);
                        medicHealth -= damageReceived;
                        Console.WriteLine($"The sickness attacked and dealt {damageReceived} damage.");

                        Thread.Sleep(3500);
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid action.");

                        Thread.Sleep(3500);
                        Console.Clear();
                        continue;
                }

                Console.WriteLine(); // Empty line for better readability
                System.Threading.Thread.Sleep(1000); // Add a delay to simulate the passage of time
            }

            if (medicHealth <= 0)
            {
                Console.WriteLine("The sickness has overwhelmed you. You fought valiantly, but it wasn't enough.");
            }
            else
            {
                Console.WriteLine("Congratulations! You have successfully defeated the sickness. The world is now a healthier place!");
            }




            return 5;
        }

        private int QuizInHospital()
        {
            Console.WriteLine("Welcome to the Medic Quiz!");
            Console.WriteLine("You need to prepare your medic knowledge if you want to help the citiznes\n");

            int score = 0;

            // Question 1
            Console.WriteLine("Question 1: What is the powerhouse of the cell?");
            Console.WriteLine("a) Nucleus");
            Console.WriteLine("b) Mitochondria");
            Console.WriteLine("c) Endoplasmic reticulum");
            Console.WriteLine("d) Golgi apparatus");

            string answer1 = Console.ReadLine();
            if (answer1.ToLower() == "b")
            {
                Console.WriteLine("Correct!\n");
                score++;
            }
            else
            {
                Console.WriteLine("Incorrect. The correct answer is b) Mitochondria.\n");
            }

            // Question 2
            Console.WriteLine("Question 2: Which organ produces insulin?");
            Console.WriteLine("a) Liver");
            Console.WriteLine("b) Pancreas");
            Console.WriteLine("c) Kidney");
            Console.WriteLine("d) Heart");

            string answer2 = Console.ReadLine();
            if (answer2.ToLower() == "b")
            {
                Console.WriteLine("Correct!\n");
                score++;
            }
            else
            {
                Console.WriteLine("Incorrect. The correct answer is b) Pancreas.\n");
            }

            // Question 3
            Console.WriteLine("Question 3: What is the largest organ in the human body?");
            Console.WriteLine("a) Liver");
            Console.WriteLine("b) Skin");
            Console.WriteLine("c) Heart");
            Console.WriteLine("d) Lungs");

            string answer3 = Console.ReadLine();
            if (answer3.ToLower() == "b")
            {
                Console.WriteLine("Correct!\n");
                score++;
            }
            else
            {
                Console.WriteLine("Incorrect. The correct answer is b) Skin.\n");
            }

            // Display final score
            Console.WriteLine($"Your final score: {score} out of 3");

            // Provide feedback based on the score
            if (score == 3)
            {
                Console.WriteLine("Congratulations! You're a medical genius!");

                medicScore += 15;
                return 15;
            }
            else if (score >= 1)
            {
                Console.WriteLine("Well done! You have a good understanding of medical concepts.");

                medicScore += 5;
                return 5;
            }
            else
            {
                Console.WriteLine("You might want to brush up on your medical knowledge. Keep learning!");

                medicScore += 0;
                return 0;
            }
        }
        
        /****************** MAP SECTION *********************/

        public void showMap(Room currentRoom)
        {
            string VillageCenter = "     ";
            string church = "     ";
            string forest = "     ";
            string market = "     ";
            string hospital = "     ";

            // Mark the current room
            switch (currentRoom.ShortDescription)
            {
                case "Village Center":
                    VillageCenter = "*You*";
                    break;
                case "Church":
                    church = "*You*";
                    break;
                case "Forest":
                    forest = "*You*";
                    break;
                case "Hospital":
                    hospital = "*You*";
                    break;
                case "Market":
                    market = "*You*";
                    break;
            }

            string map = $@"

                                                                +-----------------+
                                                                |                 |
                                                                |      Church     |
                                                                |     {church}       |
                                                                +--------+--------+
                                                                         |
                                                                         |
                                                                         |
                                                                         |
                                    +---------------+           +----------------+          +---------------+
                                    |               |           |                |          |               |
                                    |    Forest     +-----------+ Village Center +----------+    Hospital   |
                                    |   {forest}       |           |     {VillageCenter}      |          |   {hospital}       |
                                    +---------------+           +-------+--------+          +---------------+
                                                                         |
                                                                         |
                                                                         |
                                                                         |
                                                                 +-------+--------+
                                                                 |                |
                                                                 +     Market     +
                                                                 |    {market}       |
                                                                 +----------------+          


                        ";

            Console.WriteLine(map);
        }

        /****************** INTRODUCTION *********************/

        public class MedicIntroduction
        {
            public bool IntroBool = true;

            public string? MedicName = "";

            public void Intro()
            {
                while(IntroBool)
                {
                    Intro1();


                    IntroBool = false;
                }
            }

            void Intro1()
            {
                TitleAnimation("The Medic");
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("\nTEXT ART MANAGER\n");

                PrintText("\nBefore you start playing turn on fullscreen\n"+
                 "\nHello Brave Adventurer! In Chapter III, you're the important Medic character. You're not just playing a game... " +
                 "\nYou're a healer, life saver and you are bringing hope to your friends and whole village." +
                 "\nGood luck, Medic! I hope you will save lots of lives and win many battles!" +
                 "\nFirstly you need to choose your hero name.\n");

                string hero_name = GetHeroName();

                Console.WriteLine("\nHello " + hero_name + "! This name is perfect. Now let's begin journey!");

                Console.WriteLine("\nPress any key to continue...");

                PressKey();



            }

            void Intro2()
            {


            }

            void Intro3()
            {


            }
        }



        /******************* FUNCTIONALITY METHODS SECTION *****************/

        private static void PrintText(string text, int speed = 40)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            Console.WriteLine();
        }

        private static string GetHeroName()
        {
            String hero_name;

            do
            {
                Console.Write
                ("\nName of your hero must consist minimum 3 and a maximum 9 characters...");
                Console.Write
                ("\nNow to begin your journey, type in your hero name: ");

                hero_name = Console.ReadLine();

                if (string.IsNullOrEmpty(hero_name) || hero_name.Length < 3 || hero_name.Length > 9)
                {
                    Console.Write("\nType name correctly, please... \nRemember the requirements are 3-9 characters...\n");
                }
            } while (string.IsNullOrEmpty(hero_name) || hero_name.Length < 3 || hero_name.Length > 9);

            return hero_name;
        }
        private static void PressKey()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            Console.Clear();
        }
        static void TitleAnimation(string titleText)
        {
            Console.Title = "";

            foreach (char character in titleText)
            {
                Console.Title += character;
                Thread.Sleep(100);
            }
        }



    }
}

