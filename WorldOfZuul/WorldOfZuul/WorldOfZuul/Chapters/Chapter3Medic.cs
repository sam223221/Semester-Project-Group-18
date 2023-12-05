using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

            //Initialize tasks

            Task DefeatingSickness = new Task("Sickness", "You need to free the villagers", ForestQuest, forest, BattleWithSickness);

            //Add quests to the chapters quests lists

            Quests.Add(ForestQuest);

            // Add task to the quest list

            ForestQuest.AddTask(DefeatingSickness);

            //Add task to the room

            forest.AddTask(DefeatingSickness);


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

            return 5;
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

                Console.WriteLine("\nCHAPTERTextArtHERE\n");

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

