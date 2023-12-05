using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class Chapter3Medic : IChapter // Ensure it implements IChapter
    {
        
        public List<Room> Rooms { get; private set; } /*POKOJE*/
        
        public List<Quest> Quests { get; set; }
        private Room? hospital;
        private Room? VillageCenter;
        private Room? forest;
        private Room? church;
        private Room? market;
        //private Room? lab;
        private int medicScore = 0;



        public Chapter3Medic()
        {
            Rooms = new List<Room>();
            Quests = new List<Quest>();
;
        }

        public void ShowIntroduction()
        {
            



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
            
            church.SetExit("south", VillageCenter);
            
            hospital.SetExit("west", VillageCenter);
            
            market.SetExit("north", VillageCenter);
            
            forest.SetExit("east", VillageCenter);
            


            // Create quests
            // every quest has to have a sort and a long describtun
            //Quest labQuest = new Quest("Data", "Locate the missing data in the lab.");

            // Initialize tasks
            //Task(shortDec, longDec, What quest the task relates to , what room the task has to go in, this is to exicute, if the task need a key to run);
            //Task labTask = new Task("question", "awnser a tricky question", labQuest, lab, QuestionTask, officeKey);
            //Task officeTask = new Task("Key", "this is the key for the question task", labQuest, office, FindMyKey, null, officeKey);


            // Add quests to the chapter's quest list
            //Quests.Add(labQuest);

            // Add task to the quest list
            //labQuest.AddTask(officeTask);
            //labQuest.AddTask(labTask);

            // Add task to the room
            //lab.AddTask(labTask);
            //office.AddTask(officeTask);



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



        /****************** Down from here are only tasks *********************/




        private int QuestionTask()
        {


            string header = "Here's a tricky question for you:\nWhat is the capital of France?";
            string[] option = { "1. Paris", "2. London", "3. Berlin" };

            int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, option);

            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("Correct! Paris is the capital of France.\n");

                    return 10;

                case 1:
                case 2:
                    Console.WriteLine("That's not correct.\n");
                    return -5;

                default:
                    Console.WriteLine("Invalid choice.");
                    return 0;
            }
        }

        public int FindMyKey()
        {
            Console.WriteLine("you found my key thanks\n");
            return 5;
        }

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
    }
}

