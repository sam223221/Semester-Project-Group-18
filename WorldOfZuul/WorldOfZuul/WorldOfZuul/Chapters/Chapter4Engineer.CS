namespace WorldOfZuul
{
    public class Chapter4Engineer : IChapter // Ensure it implements IChapter
    {
        public List<Room> Rooms { get; private set; }
        public List<Quest> Quests {get; set;}
        private Room? waterTreatmentFacility;
        private Room? villageCenter;
        private Room? powerStation;
        private Room? theatre;
        private Room? office;
        private Room? lab;
        private int engineerScore = 0;



        public Chapter4Engineer()
        {

            Rooms = new List<Room>();
            Quests = new List<Quest>();
            CreateRoomsAndQuests();
            ShowIntroduction();

        }    

        public void ShowIntroduction()
        {

        }

        public Room GetStartRoom() => villageCenter;


        public void CreateRoomsAndQuests()
        {

            //creat Items
            Item officeKey = new Item("officeKey","these keys are for opning a back dor in the office");


            // Initialize rooms
            waterTreatmentFacility  = new Room("somewhere"      , "you're somewhere, don't know where but somewhere",officeKey);
            villageCenter           = new Room("villageCenter"  , "You're standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");
            theatre                 = new Room("Theatre"        , "You're finding yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");
            office                  = new Room("Office"         , "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");
            powerStation            = new Room("Powerstation"   , "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");
            lab                     = new Room("Lab"            , "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.");


            // Set exits
            waterTreatmentFacility.SetExit("east", powerStation);
            villageCenter.SetExits(null, theatre, lab, powerStation);
            theatre.SetExit("west", villageCenter);
            powerStation.SetExits(null, villageCenter, null, waterTreatmentFacility);
            lab.SetExits(villageCenter, office, null, null);
            office.SetExit("west", lab);


            // Creat quests
            // every quest has to have a sort and a long describtun
            Quest labQuest  = new Quest("Data", "Locate the missing data in the lab.");

            //initialize tasks
            //Task(shortDec, longDec, What quest the task relates to , what room the task has to go in, this is to exicute, if the task need a key to run);
            Task labTask    = new Task("question"   , "awnser a tricky question"                , labQuest  , lab       , QuestionTask  , officeKey );
            Task officeTask = new Task("Key"        , "this is the key for the question task"   , labQuest  , office    , FindMyKey     , null      , officeKey);
            

            // Add quests to the chapter's quest list
            Quests.Add(labQuest);

            // add task to the quest list
            labQuest.AddTask(officeTask);
            labQuest.AddTask(labTask);

            // add task to the room
            lab.AddTask(labTask);
            office.AddTask(officeTask);
                

        
            // adding thigs to rooms
            Rooms.Add(waterTreatmentFacility);
            Rooms.Add(villageCenter);
            Rooms.Add(theatre);
            Rooms.Add(office);
            Rooms.Add(powerStation);
            Rooms.Add(lab);

        }
        public string PlayerScore()
        {
            return $@"your engineer score is : {engineerScore}";
        }



        /****************** down form here are only tasks*********************/




        private int QuestionTask()
        {

            
            string header = "Here's a tricky question for you:\nWhat is the capital of France?";
            string[] option = {"1. Paris","2. London","3. Berlin"};
            
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
            string watertreatment   = "     ";
            string PowerStation     = "     ";
            string villageCenter    = "     ";
            string theatre          = "     ";
            string office           = "     ";
            string lab              = "     ";

            // Mark the current room
            switch (currentRoom.ShortDescription)
            {
                case "somewhere":
                    watertreatment  = "*You*";
                    break;
                case "Powerstation":
                    PowerStation    = "*You*";
                    break;
                case "villageCenter":
                    villageCenter   = "*You*";
                    break;
                case "Theatre":
                    theatre         = "*You*";
                    break;
                case "Office":
                    office          = "*You*";
                    break;
                case "Lab":
                    lab             = "*You*";
                    break;
            }
            string map = $@"
            +---------------+         +---------------+          +---------------+          +---------------+
            |     water     |         |               |          |               |          |               |
            |  Treatmenmt   +---------+  Powerstation +----------+ villagecenter +----------+     Theatre   |
            |     {watertreatment}     |         |     {PowerStation}     |          |     {villageCenter}     |          |     {theatre}     |
            +---------------+         +---------------+          +-------+-------+          +---------------+
                                                                         |
                                                                         |
                                                                         |
                                                                         |
                                                                 +-------+-------+          +---------------+
                                                                 |               |          |               |
                                                                 +      Lab      +----------+     Office    |
                                                                 |     {lab}     |          |     {office}     |
                                                                 +---------------+          +---------------+


                        ";

        Console.WriteLine(map);
        }
    }
}