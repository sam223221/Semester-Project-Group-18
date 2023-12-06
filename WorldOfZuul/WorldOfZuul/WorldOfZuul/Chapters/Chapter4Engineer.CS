using System.Diagnostics.Contracts;
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
        private int ChosenMethod = 0;
        private string[] chosenMethod = {"Sand Filtration and Boiling","Chemical Chlorination","Natural Biofiltration Using Plants"};

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
            Item WaterSample            = new Item("Watersample"            ,"Water sample from the river"                                  ,"You need to go to the river and investigate it then take a sample");
            Item WaterAnalyzis          = new Item("Analyzis paper"         ,"An analyzis from the River sample"                            ,"you need to analyze the river water");
            Item FactoryIvestigation    = new Item("FactoryInfo"            , "Information about the abandon water factory"                 ,"Go into the library and read about the factory");
            Item ResearchPaper          = new Item("Research paper"         ,"Research paper about the riversample"                         ,"you need to do the research before you can start implementing");
            Item FactoryInfo            = new Item("Factory Investigation"  , "knowledge about the factory and evidence of illegal dumping" ,"you need to talk to the people in the village center");

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
            Quest waterTreatmentQuest       = new Quest("Data"              , "Locate the missing data in the lab."                                                                 );
            Quest housingQutest             = new Quest("Housing Quest"     , "in this quest you need to research and chose what material\n and show how to build new modern homes" );
            Quest electricityQuest          = new Quest("Electricity Quest" ,"you need to research and find good solution to new and renewible energy, that is sustanible"          );
            
            //initialize tasks
            Task investigateRiver           = new Task("investigation"      ,"there is some polution in this river"                             ,waterTreatmentQuest,River          ,InvestigateRiver               ,null           ,WaterSample        );
            Task interview                  = new Task("Interview"          ," talk to people around to see what what happend"                  ,waterTreatmentQuest,villageCenter  ,VillageCenterInterview         ,WaterSample    ,FactoryInfo        );
            Task abandonFactory             = new Task("investigation"      ," aboandon factory"                                                ,waterTreatmentQuest,AbandonFactory ,AbandonFactoryVisit            ,FactoryInfo    ,FactoryIvestigation);
            Task Analysis                   = new Task("Analysis"           ,"water analysis of to fix the water factory"                       ,waterTreatmentQuest,lab            ,AnalyzeWaterSample             ,WaterSample    ,WaterAnalyzis      );
            Task ResearchSolution           = new Task("Research"           ,"Research for the best solutin to solve the water problem"         ,waterTreatmentQuest,Library        ,ResearchPurificationMethods    ,WaterAnalyzis  ,ResearchPaper      );
            Task ImplementWaterSolutin      = new Task("WaterSolution"      ,"now all the prework is done you can start implementing the filter",waterTreatmentQuest,AbandonFactory ,ImplementPurificationSolution  ,ResearchPaper  ,null               );

            
            
            // Add quests to the chapter's quest list
            Quests.Add(waterTreatmentQuest);
            Quests.Add(housingQutest);
            Quests.Add(electricityQuest);
            
            // add task to the quest list
            waterTreatmentQuest.AddTask(investigateRiver);
            waterTreatmentQuest.AddTask(interview);
            waterTreatmentQuest.AddTask(abandonFactory);
            waterTreatmentQuest.AddTask(Analysis);
            waterTreatmentQuest.AddTask(ResearchSolution);
            waterTreatmentQuest.AddTask(ImplementWaterSolutin);


            // add task to the room
            AbandonFactory.AddTask(abandonFactory);
            AbandonFactory.AddTask(abandonFactory);
            villageCenter.AddTask(interview);
            Library.AddTask(ResearchSolution);
            River.AddTask(investigateRiver);
            lab.AddTask(Analysis);
        
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

        private int ImplementPurificationSolution()
        {
            Console.WriteLine("With a clear plan and the community's support, you begin the implementation of the water purification system.");

            string header = "With a clear plan and the community's support, you begin the implementation of the water purification system.\n\nSelect an action:";
            string[] options = {
                "1. Organize a community cleanup",
                "2. Start construction of the filtration system",
                "3. Coordinate with local authorities for support"
            };

            bool purificationComplete = false;
            while (!purificationComplete)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, options);

                switch (selectedIndex)
                {
                    case 0:
                        if (CleanupCoordinationMiniGame()) // Returns true if successful
                        {
                            Console.WriteLine("The community cleanup was a success!");
                            purificationComplete = true;
                        }
                        break;
                    case 1:
                        if (ConstructionPuzzleMiniGame()) // Returns true if successful
                        {
                            Console.WriteLine("The construction of the filtration system is complete!");
                            purificationComplete = true;
                        }
                        break;
                    case 2:
                        if (ResourceNegotiationMiniGame()) // Returns true if successful
                        {
                            Console.WriteLine("You have successfully secured support from local authorities!");
                            purificationComplete = true;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid action.");
                        break;
                }
                Console.ReadKey();
            }

            int scoreChange = 10; // Example score change
            Console.WriteLine("The water purification system is now operational.");
            return scoreChange;
        }
        private bool ConstructionPuzzleMiniGame()
        {
            var components      = new List<string> { "Sand Filter", "Chlorination Tank", "UV Light Disinfection", "Reverse Osmosis Membrane" };
            var correctOrder    = new List<string> { "Sand Filter", "Reverse Osmosis Membrane", "Chlorination Tank", "UV Light Disinfection" };
            var playerOrder     = new List<string>();

            Console.WriteLine("You need to assemble the water filtration system. Place the components in the correct order.");

            int attempts = 0;
            while (playerOrder.Count < components.Count)
            {
                Console.WriteLine("\nAvailable components:");
                foreach (var component in components)
                {
                    if (!playerOrder.Contains(component))
                    {
                        Console.WriteLine($"- {component}");
                    }
                }

                Console.Write("Select the next component to add: ");
                string selectedComponent = Console.ReadLine();

                if (components.Contains(selectedComponent) && !playerOrder.Contains(selectedComponent))
                {
                    playerOrder.Add(selectedComponent);
                    Console.WriteLine($"{selectedComponent} added.");
                }
                else
                {
                    Console.WriteLine("Invalid component or already added. Please try again.");
                }

                attempts++;
            }

            if (playerOrder.SequenceEqual(correctOrder))
            {
                Console.WriteLine("\nFiltration system successfully assembled!");
                return true;
            }
            else
            {
                Console.WriteLine("\nThe components are not in the correct order. Please try again.");
                return false;
            }
        }
        private bool ResourceNegotiationMiniGame()
        {
            var questions = new List<string>
            {
                "How will this project impact the local ecosystem?" ,
                "What are the long-term benefits for our community?",
                "How do you plan to manage the project's costs?"
            };

            var responses = new Dictionary<string, List<string>>
            {
                { "How will this project impact the local ecosystem?", new List<string> { "It will improve biodiversity.", "It's not a major concern.", "We'll assess it later." } },
                { "What are the long-term benefits for our community?", new List<string> { "Clean water for decades.", "It's mostly short-term.", "Boost to local tourism." } },
                { "How do you plan to manage the project's costs?", new List<string> { "Through efficient budgeting.", "We'll figure it out as we go.", "Seeking external funding." } }
            };

            int successfulResponses = 0;

            foreach (var question in questions)
            {
                Console.WriteLine($"\nAuthority: {question}");
                for (int i = 0; i < responses[question].Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {responses[question][i]}");
                }

                Console.Write("Your response: ");
                if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= responses[question].Count)
                {
                    // Assuming the first response is always the most convincing
                    if (selectedIndex == 1)
                    {
                        successfulResponses++;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid response. Please try again.");
                }
            }

            if (successfulResponses >= questions.Count)
            {
                Console.WriteLine("\nYou have successfully negotiated support from the local authorities!");
                return true;
            }
            else
            {
                Console.WriteLine("\nYou were unable to secure the necessary support.");
                return false;
            }
        }
        private bool CleanupCoordinationMiniGame()
        {
            var areas = new List<string> { "North Bank", "South Bank", "East Bank", "West Bank" };
            var volunteers = new Dictionary<string, string>
            {
                { "Alice"   , "Collecting"      },
                { "Bob"     , "Heavy Lifting"   },
                { "Charlie" , "Sorting"         },
                { "Diana"   , "Scouting"        }
            };

            int turns = 4; // Each volunteer gets one turn
            int cleanedAreas = 0;

            Console.WriteLine("You need to assign volunteers to clean different areas of the riverbank.");

            for (int i = 0; i < turns; i++)
            {
                Console.WriteLine("\nAreas to clean: " + string.Join(", ", areas));
                Console.WriteLine("Available volunteers and their skills:");
                foreach (var volunteer in volunteers)
                {
                    Console.WriteLine($"- {volunteer.Key} (Skill: {volunteer.Value})");
                }

                Console.Write("Choose a volunteer to assign: ");
                string volunteerName = Console.ReadLine();
                Console.Write("Choose an area to clean: ");
                string area = Console.ReadLine();

                // Simple logic to determine if the choice was effective
                if (volunteers.ContainsKey(volunteerName) && areas.Contains(area))
                {
                    Console.WriteLine($"{volunteerName} is cleaning the {area}.");
                    cleanedAreas++; // Increment for each successful assignment
                    areas.Remove(area); // Remove the cleaned area
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    i--; // Allow the player to make the choice again
                }
            }

            Console.WriteLine($"\nCleanup complete. You successfully cleaned {cleanedAreas} areas of the riverbank.");
            return cleanedAreas >= 3; // Return true if most areas are cleaned
        }
        private int ResearchPurificationMethods()
        {
            int SocialScore = 0;

            string header = "You're in the village library, surrounded by books on environmental science and engineering.\nYour goal is to find the most sustainable method to purify the river.\n\nChoose a water purification method to research:";
            string[] options = {
                "1. Sand Filtration and Boiling",
                "2. Chemical Chlorination",
                "3. Natural Biofiltration Using Plants"
            };

                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, options);

                switch (selectedIndex)
                {
                    case 0:
                        Console.WriteLine("\nYou learn about traditional methods like sand filtration and boiling. While effective, they are less practical for large-scale purification.");
                        ChosenMethod=0;
                        SocialScore += 3; // Less sustainable, moderate score increase
                        break;
                    case 1:
                        Console.WriteLine("\nChemical chlorination is highly effective but has environmental drawbacks due to chemical usage.");
                        ChosenMethod=1;
                        SocialScore += 2; // Least sustainable, lower score increase
                        break;
                    case 2:
                        Console.WriteLine("\nNatural biofiltration uses plants to remove contaminants. It's highly sustainable and eco-friendly.");
                        ChosenMethod=2;
                        SocialScore += 5; // Most sustainable, highest score increase
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid purification method.");
                        break;
                }
                Console.ReadKey();
            

            Console.WriteLine("Research complete. You have identified a sustainable purification method for the river.");
            return SocialScore; // Return the updated social score
        }
        private int AnalyzeWaterSample()
        {

            string header = "You enter the village workshop, equipped with various scientific instruments.\nIt's time to analyze the water samples collected from the river.\n\nChoose an analysis method:";
            string[] options = {
                "1. Chemical Reagent Test",
                "2. Microscopic Examination",
                "3. Spectrometric Analysis"
            };

            int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, options);

            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("\nYou add chemical reagents to the water sample. The solution changes color, indicating the presence of heavy metals.");      
                    break;
                case 1:
                    Console.WriteLine("\nUnder the microscope, you observe abnormal bacterial activity, suggesting contamination.");                  
                    break;
                case 2:
                    Console.WriteLine("\nThe spectrometer reveals unusual organic compounds in the water, likely pollutants.");
                    break;
                default:
                    Console.WriteLine("\nInvalid choice. Please select a valid analysis method.");
                    break;
            }

             Console.WriteLine("Analysis complete. You have identified the contaminants in the water.");
            Console.ReadKey();

            return 5;
        }
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