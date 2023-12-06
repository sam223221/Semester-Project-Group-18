using System.Net.Http.Headers;

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
            Item WaterSample = new Item("Watersample","Water sample from the river","You need to go to the river and investigate it then take a sample");
            Item WaterAnalyzis = new Item("Analyzis paper","An analyzis from the River sample", "you need to analyze the river water");
            Item FactoryIvestigation = new Item("FactoryInfo", "Information about the abandon water factory", "Go into the library and read about the factory");
            Item ResearchPaper = new Item("Research paper","Research paper about the riversample","you need to do the research before you can start implementing");
            Item FactoryInfo = new Item("Factory Investigation", "knowledge about the factory and evidence of illegal dumping","you need to talk to the people in the village center");

            // Initialize rooms
            HistoricalArchive       = new Room("HistoricalArchive"  , "A section of the Village Library or a separate room filled with ancient records, blueprints, and artifacts. \nIt's a key resource for uncovering historical technologies and inspirations for the Electricity Quest.");
            construtionSite         = new Room("ConstructionSite"   , "An active building site with scaffolding, construction equipment, and a buzz of activity.\nIt's the physical manifestation of Sam's designs coming to life in the Housing Quest");
            AbandonFactory          = new Room("AbandonFactory"     , "A dilapidated structure with rusted machinery and broken windows, overtaken by nature. \nIt holds clues to the village's industrial past and is key to solving the Water Treatment Quest.");
            villageCenter           = new Room("VillageCenter"      , "The bustling heart of the village, with cobblestone streets, quaint shops, and a lively marketplace.\nIt's a central meeting point for villagers, buzzing with gossip and activity, and serves as a starting point for both the Water Treatment and Electricity Quests.");
            ModernHome              = new Room("ModernHome"         , "A newly built eco-friendly house, showcasing innovative design and sustainable living. \nIt serves as a model home, demonstrating the success of your effort in the Housing Quest.");
            TownHall                = new Room("TownHall"           , "A grand, old building with a clock tower, symbolizing the village's heritage and governance. Inside, it's adorned with historical artifacts and portraits of past leaders.\nThis is where you discusses the Electricity Quest with village officials.");
            DesignHQ                = new Room("DesignHQ"           , "A creative studio with architectural models, blueprints, and a large drafting table. It's a hub of innovation where you'll design sustainable homes for the Housing Quest.");
            SunStone                = new Room("SunStone"           , "An ancient, mystical site located in a clearing outside the village. \nIt features a large, engraved stone rumored to harness solar energy, inspiring Sam's solutions in the Electricity Quest.");
            Library                 = new Room("Library"            , "A quiet, cozy building filled with rows of books, old manuscripts, and a large, detailed map of the village and surrounding areas. \nIt's a treasure trove of information for researching both the Water Treatment and Electricity Quests.");
            River                   = new Room("River"              , "A serene spot by the river, with willow trees, benches, and a small dock. \nThe river's condition, however, shows signs of pollution, setting the stage for the Water Treatment Quest.");
            lab                     = new Room("Lab"                , "A modern contrast to the village's rustic charm, the lab is equipped with various scientific instruments, a workbench, and a chalkboard filled with equations and diagrams.\nIt's your haven for experimenting and problem-solving.");

            // Set exits
            HistoricalArchive.SetExit("north",Library);
            construtionSite.SetExits(null,DesignHQ,ModernHome,null);
            AbandonFactory.SetExits(null,null,SunStone,River);
            villageCenter.SetExits(TownHall,River,Library,DesignHQ);
            ModernHome.SetExit("north",construtionSite);
            TownHall.SetExit("south", villageCenter);
            DesignHQ.SetExits(null,villageCenter,null,construtionSite);
            SunStone.SetExit("north",AbandonFactory);
            Library.SetExits(villageCenter,lab,HistoricalArchive,null);
            River.SetExits(null,AbandonFactory,null,villageCenter);
            lab.SetExit("west",Library);


            // Creat quests
            // every quest has to have a sort and a long describtun
            Quest waterTreatmentQuest   = new Quest("Data", "Locate the missing data in the lab.");
            Quest housingQutest         = new Quest("Housing Quest", "in this quest you need to research and chose what material\n and show how to build new modern homes");
            Quest electricityQuest      = new Quest("Electricity Quest","you need to research and find good solution to new and renewible energy, that is sustanible");
            
            //initialize tasks
            Task investigateRiver   = new Task("investigation","there is some polution in this river",waterTreatmentQuest,River,InvestigateRiver,null,WaterSample);
            Task interview          = new Task("Interview"," talk to people around to see what what happend", waterTreatmentQuest,villageCenter,VillageCenterInterview,WaterSample,FactoryInfo);
            Task abandonFactory     = new Task("investigation"," aboandon factory",waterTreatmentQuest,AbandonFactory,AbandonFactoryVisit,FactoryInfo,FactoryIvestigation);
            
            
            
            // Add quests to the chapter's quest list
            Quests.Add(waterTreatmentQuest);
            Quests.Add(housingQutest);
            Quests.Add(electricityQuest);
            
            // add task to the quest list
            waterTreatmentQuest.AddTask(investigateRiver);
            waterTreatmentQuest.AddTask(interview);
            waterTreatmentQuest.AddTask(abandonFactory);


            // add task to the room
            AbandonFactory.AddTask(abandonFactory);
            villageCenter.AddTask(interview);
            River.AddTask(investigateRiver);
        
            // adding thigs to rooms
            Rooms.Add(HistoricalArchive);
            Rooms.Add(construtionSite);
            Rooms.Add(AbandonFactory);
            Rooms.Add(villageCenter);
            Rooms.Add(ModernHome);
            Rooms.Add(TownHall);
            Rooms.Add(DesignHQ);
            Rooms.Add(SunStone);
            Rooms.Add(Library);
            Rooms.Add(River);
            Rooms.Add(lab);

        }
        public string PlayerScore()
        {
            return $@"your engineer score is : {engineerScore}";
        }



        /****************** down form here are only tasks*********************/


        private int AbandonFactoryVisit()
                {
            bool foundEvidence = false;

            string header = "As you step into the abandoned factory, the air grows heavy with the scent of rust and decay.\nThe once-busy production halls now stand silent, with machinery covered in dust and cobwebs.\n\nWhere do you want to look?";
            string[] options = {
                "1. The old office",
                "2. The production floor",
                "3. The storage area",
            };

            while (!foundEvidence)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, options);

                switch (selectedIndex)
                {
                    case 0:
                        string header1 = "The office is cluttered with old papers and broken furniture. Amidst the chaos, you find a locked filing cabinet.\nDo you want to try to open it?";
                        string[] options1 = {"Yes","No"};
                        Console.WriteLine("Do you want to try to open it? (yes/no)");
                        int selectedIndex1 = InteractiveMenu.MultichoiceQuestion(header1, options1);
                        if (selectedIndex1 == 0)
                        {
                            Console.WriteLine("After some effort, the cabinet creaks open, revealing documents about illegal waste disposal practices.");
                            foundEvidence = true;
                        }else
                        {
                            Console.WriteLine("Since the room didn't entail anything else, you went back down to the main floor");
                        }
                        break;
                    case 1:
                        Console.WriteLine("The production floor is littered with remnants of the factory's past operations. You notice a trail of dried chemical residues leading towards the back.");
                        break;
                    case 2:
                        Console.WriteLine("In the storage area, you find barrels labeled with hazardous material warnings, some of them leaking a strange substance.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
                Console.ReadKey();
            }

            // Update social score and mark task as completed
            Console.WriteLine("\nYou have found crucial evidence linking the factory to the river's pollution.\n");            
            return 5;
        }
        private int VillageCenterInterview()
        {

            bool gatheredAllInfo = false;
            HashSet<string> infoCollected = new HashSet<string>();

            string header = "You approach the villagers to ask about the river. Who do you want to talk to?";
            string[] option = {
                               "1. The old fisherman"
                              ,"2. The local shopkeeper"
                              ,"3. A group of playing children"
                              };

            while (!gatheredAllInfo)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, option);
                
                switch (selectedIndex)
                {
                case 0:
                    if (!infoCollected.Contains("fisherman"))
                    {
                        Console.WriteLine("\nFisherman: \n'The fish are gone, and the water smells strange. It's not like it used to be.'");
                        Console.WriteLine("'ever sice that factory shut down'");
                        infoCollected.Add("fisherman");

                    }
                    else
                    {
                        Console.WriteLine("You have already talked to the fisherman.");
                    }
                    break;
                case 1:
                    if (!infoCollected.Contains("shopkeeper"))
                    {
                        Console.WriteLine("Shopkeeper: 'People have been complaining about the water quality. It's affecting my business too.'");
                        infoCollected.Add("shopkeeper");
                    }
                    else
                    {
                        Console.WriteLine("You have already talked to the shopkeeper.");
                    }
                    break;
                case 2:
                    if (!infoCollected.Contains("children"))
                    {
                        Console.WriteLine("Children: 'We don't play near the river anymore, it's yucky and smells bad!'");
                        infoCollected.Add("children");
                    }
                    else
                    {
                        Console.WriteLine("You have already talked to the children.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
                }
                Console.ReadKey();

                if (infoCollected.Count >= 3)
                {
                    gatheredAllInfo = true;
                }

            }

            Console.WriteLine("\nYou have gathered enough information from the villagers");
            return 5;

        }

        private int InvestigateRiver()
        {
            Console.WriteLine("You observe the river's murky waters and notice a decrease in wildlife.");
            Console.WriteLine("You collect water samples from different parts of the river.\n");

            return 5;
        }


        private int QuestionTask()
        {

                
            string header = "Here's a tricky question for you:\nWhat is the capital of France?";
            string[] option = {"1. Paris","2. London","3. Berlin"};
            
            while(true){
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
        }

        public int FindMyKey()
        {
            Console.WriteLine("you found my key thanks\n");
            return 5;
        }

        public void showMap(Room currentRoom)
        {
            string HistoricalArchive    = "     ";
            string construtionSite      = "     ";
            string AbandonFactory       = "     ";
            string villageCenter        = "     ";
            string ModernHome           = "     ";
            string TownHall             = "     ";
            string DesignHQ             = "     ";
            string SunStone             = "     ";
            string Library              = "     ";
            string River                = "     ";
            string lab                  = "     ";

            // Mark the current room
            switch (currentRoom.ShortDescription)
            {
                case "HistoricalArchive":
                    HistoricalArchive   = "*You*";
                    break;
                case "ConstructionSite":
                    construtionSite     = "*You*";
                    break;
                case "AbandonFactory":
                    AbandonFactory      = "*You*";
                    break;
                case "VillageCenter":
                    villageCenter       = "*You*";
                    break;
                case "ModernHome":
                    ModernHome          = "*You*";
                    break;
                case "TownHall":
                    TownHall            = "*You*";
                    break;
                case "DesignHQ":
                    DesignHQ            = "*You*";
                    break;
                case "SunStone":
                    SunStone            = "*You*";
                    break;
                case "Library":
                    Library             = "*You*";
                    break;
                case "River":
                    River               = "*You*";
                    break;
                case "Lab":
                    lab                 = "*You*";
                    break;
            }

            string map = $@"
                                                           +---------------+                                                            
                                                           |               |                                                            
                                                           |   Town Hall   |                                                            
                                                           |     {TownHall}     |                                                            
                                                           +-------+-------+                                                            
                                                                   |                                                                    
                                                                   |                                                                    
                                                                   |                                                                    
     +---------------+          +---------------+          +-------+-------+          +---------------+         +---------------+       
     | construction  |          |               |          |    Village    |          |    Village    |         |    Abandon    |       
     |     site      +----------+   Design HQ   +----------+    Center     +----------+   Riverside   +---------+    Factory    |       
     |     {construtionSite}     |          |     {DesignHQ}     |          |     {villageCenter}     |          |     {River}     |         |     {AbandonFactory}     |       
     +-------+-------+          +---------------+          +-------+-------+          +---------------+         +-------+-------+       
             |                                                     |                                                    |               
             |                                                     |                                                    |               
             |                                                     |                                                    |               
             |                                                     |                                                    |               
     +-------+-------+                                     +-------+-------+          +---------------+         +-------+-------+       
     |    Modern     |                                     |    Village    |          |    Village    |         |               |       
     |     Home      |                                     |    Library    +----------+      Lab      |         |   Sun Stone   |       
     |     {ModernHome}     |                                     |     {Library}     |          |     {lab}     |         |     {SunStone}     |       
     +---------------+                                     +-------+-------+          +---------------+         +---------------+       
                                                                   |                                                                    
                                                                   |                                                                    
                                                                   |                                                                    
                                                                   |                                                                    
                                                           +-------+-------+                                                            
                                                           |   Historical  |                                                            
                                                           |    Archive    |                                                            
                                                           |     {HistoricalArchive}     |                                                            
                                                           +---------------+                                                            
                                                                                                                                        

                        ";

        Console.WriteLine(map);
        }


    }
}