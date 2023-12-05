namespace WorldOfZuul
{
    public class Chapter4Engineer : IChapter // Ensure it implements IChapter
    {
        public List<Room> Rooms { get; private set; }
        public List<Quest> Quests {get; set;}
        private Room? HistoricalArchive;
        private Room? construtionSite;
        private Room? AbandonFactory;
        private Room? villageCenter;
        private Room? ModernHome;
        private Room? TownHall;
        private Room? SunStone;
        private Room? DesignHQ;
        private Room? Library;
        private Room? River;
        private Room? lab;

        private int engineerScore = 0;



        public Chapter4Engineer()
        {

            Rooms = new List<Room>();
            Quests = new List<Quest>();

        }    

        public void ShowIntroduction()
        {
            string[] introSlides = new string[] {"EgnineerIntro","EgineerIntroWater","EgineerIntroHousing","EgineerIntroElectricity"};

            foreach (var slide in introSlides)
            {
                Console.Clear(); // Clear the console for each slide
                Console.WriteLine(TextArtManager.GetTextArt(slide)); // Display the slide
                Console.ReadKey(); // Wait for any key press
            }


        }

        public Room GetStartRoom() => villageCenter;


        public void CreateRoomsAndQuests()
        {

            //creat Items
            Item officeKey = new Item("","");


            // Initialize rooms
            HistoricalArchive       = new Room("HistoricalArchive"  , "A section of the Village Library or a separate room filled with ancient records, blueprints, and artifacts. \nIt's a key resource for uncovering historical technologies and inspirations for the Electricity Quest.");
            construtionSite         = new Room("ConstructionSite"   , "An active building site with scaffolding, construction equipment, and a buzz of activity.\nIt's the physical manifestation of Sam's designs coming to life in the Housing Quest");
            AbandonFactory          = new Room("AbandonFactory"     , "A dilapidated structure with rusted machinery and broken windows, overtaken by nature. \nIt holds clues to the village's industrial past and is key to solving the Water Treatment Quest.");
            villageCenter           = new Room("villageCenter"      , "The bustling heart of the village, with cobblestone streets, quaint shops, and a lively marketplace.\nIt's a central meeting point for villagers, buzzing with gossip and activity, and serves as a starting point for both the Water Treatment and Electricity Quests.");
            ModernHome              = new Room("ModernHome"         , "A newly built eco-friendly house, showcasing innovative design and sustainable living. \nIt serves as a model home, demonstrating the success of your effort in the Housing Quest.");
            TownHall                = new Room("TownCenter"         , "A grand, old building with a clock tower, symbolizing the village's heritage and governance. Inside, it's adorned with historical artifacts and portraits of past leaders.\nThis is where you discusses the Electricity Quest with village officials.");
            DesignHQ                = new Room("DesignHQ"           , "A creative studio with architectural models, blueprints, and a large drafting table. It's a hub of innovation where you'll design sustainable homes for the Housing Quest.");
            SunStone                = new Room("SunStone"           , "An ancient, mystical site located in a clearing outside the village. \nIt features a large, engraved stone rumored to harness solar energy, inspiring Sam's solutions in the Electricity Quest.");
            Library                 = new Room("Library"            , "A quiet, cozy building filled with rows of books, old manuscripts, and a large, detailed map of the village and surrounding areas. \nIt's a treasure trove of information for researching both the Water Treatment and Electricity Quests.");
            River                   = new Room("River"              , "A serene spot by the river, with willow trees, benches, and a small dock. \nThe river's condition, however, shows signs of pollution, setting the stage for the Water Treatment Quest.");
            lab                     = new Room("Lab"                , "A modern contrast to the village's rustic charm, the lab is equipped with various scientific instruments, a workbench, and a chalkboard filled with equations and diagrams.\nIt's your haven for experimenting and problem-solving.");

            // Set exits
            AbandonFactory.SetExit("west", lab);
            villageCenter.SetExits(null, Library, lab, River);
            TownHall.SetExit("east", River);
            Library.SetExit("west", villageCenter);
            River.SetExits(null, villageCenter, null, TownHall);
            lab.SetExits(villageCenter, AbandonFactory, null, null);


            // Creat quests
            // every quest has to have a sort and a long describtun
            Quest labQuest  = new Quest("Data", "Locate the missing data in the lab.");

            //initialize tasks
            //Task(shortDec, longDec, What quest the task relates to , what room the task has to go in, this is to exicute, if the task need a key to run);
            Task labTask    = new Task("question"   , "awnser a tricky question"                , labQuest  , lab       , QuestionTask  , officeKey );
            Task officeTask = new Task("Key"        , "this is the key for the question task"   , labQuest  , AbandonFactory    , FindMyKey     , null      , officeKey);
            
            
            // Add quests to the chapter's quest list
            Quests.Add(labQuest);

            // add task to the quest list
            labQuest.AddTask(officeTask);
            labQuest.AddTask(labTask);

            // add task to the room
            lab.AddTask(labTask);
            AbandonFactory.AddTask(officeTask);
                

        
            // adding thigs to rooms
            Rooms.Add(TownHall);
            Rooms.Add(villageCenter);
            Rooms.Add(Library);
            Rooms.Add(AbandonFactory);
            Rooms.Add(River);
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