using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace HopeRising
{
    public class Chapter4Engineer : IChapter // Ensure it implements IChapter
    {
        public List<Room> Rooms { get; private set; }
        public List<Quest> Quests {get; set;}
        public bool IsCompleted { get; set; }
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

            Item EnergyMind             = new Item("Energy quiz master"     ,"you are now the master of energy"                             ,"you need togo to the Library to do the PowerQuiz to unluck this task");
            Item AreaMaster             = new Item("All knowing Area master","Now we know where the solarpanals are going to stand"         ,"you need togo to Sun Stone and do the SunStoneExplor task to get acess to this one");
            Item Prototype              = new Item("SolarPanel Prototype"   ,"this is the prototype of the solarPanel"                      ,"you need togo to the Lab and do the PrototypeSolar task to get acess");
            Item FundingPaper           = new Item("Funding Paper"          ,"The funding for the SolarPanels"                              ,"you need togo to TownHall to ask for funding");
            Item Instalation            = new Item("Instalation"            ,"The instalation is now done and this is a proof of that"      ,"you need togo to Sun Stone and do the instalation task");

            Item BluePrint              = new Item("BluePrint"              ,"this is the BLuePrint for the new homes"                      ,"you need togo to DesignHQ and desing the new homes");
            Item TrainingCir            = new Item("Training Certify"       ,"you have qualifyed the workers to start bulding homes"        ,"you need togo to ConstructionSite and do Training");
            // Initialize rooms
            HistoricalArchive       = new Room("HistoricalArchive"  , "A section of the Village Library or a separate room filled with ancient records, blueprints, and artifacts. \nIt's a key resource for uncovering historical technologies.");
            construtionSite         = new Room("ConstructionSite"   , "An active building site with scaffolding, construction equipment, and a buzz of activity.\nIt's the physical manifestation of the engineers designs coming to life in the Housing Quest");
            AbandonFactory          = new Room("AbandonFactory"     , "A dilapidated structure with rusted machinery and broken windows, overtaken by nature. \nIt holds clues to the village's industrial past and is key to solving the Water Treatment Quest.");
            villageCenter           = new Room("VillageCenter"      , "The bustling heart of the village, with cobblestone streets, quaint shops, and a lively marketplace.\nIt's a central meeting point for villagers, buzzing with gossip and activity.");
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
            Quest waterTreatmentQuest       = new Quest("Water treatment quest  "   ,"Locate the missing data in the lab."                                                                 );
            Quest electricityQuest          = new Quest("Electricity Quest      "   ,"you need to research and find good solution to new and renewible energy, that is sustanible"         );
            Quest housingQutest             = new Quest("Housing Quest          "   ,"in this quest you need to research and chose what material\n and show how to build new modern homes" );
            
            //initialize tasks

            // water treatment quest
            Task investigateRiver           = new Task("investigation"      ,"Water :   there is some polution in this river"                               ,waterTreatmentQuest    ,River          ,InvestigateRiver                   ,null           ,WaterSample        );
            Task interview                  = new Task("Interview"          ,"Water :   talk to people around to see what what happend"                     ,waterTreatmentQuest    ,villageCenter  ,VillageCenterInterview             ,WaterSample    ,FactoryInfo        );
            Task abandonFactory             = new Task("Investigation"      ,"Water :   aboandon factory"                                                   ,waterTreatmentQuest    ,AbandonFactory ,AbandonFactoryVisit                ,FactoryInfo    ,FactoryIvestigation);
            Task Analysis                   = new Task("Analysis"           ,"Water :   water analysis of to fix the water factory"                         ,waterTreatmentQuest    ,lab            ,AnalyzeWaterSample                 ,WaterSample    ,WaterAnalyzis      );
            Task ResearchSolution           = new Task("Research"           ,"Water :   Research for the best solutin to solve the water problem"           ,waterTreatmentQuest    ,Library        ,ResearchPurificationMethods        ,WaterAnalyzis  ,ResearchPaper      );
            Task ImplementWaterSolutin      = new Task("WaterSolution"      ,"Water :   now all the prework is done you can start implementing the filter"  ,waterTreatmentQuest    ,AbandonFactory ,ImplementPurificationSolution      ,ResearchPaper  ,null               );

            // Electricity quest
            Task colectKnowlage             = new Task("PowerQuiz"          ,"EL    :   You need to know learn some thing about Power"                      , electricityQuest      ,Library        ,EnergyQuizMiniGame                 ,null           ,EnergyMind         );
            Task SunStoneExplor             = new Task("SunStoneExplore"     ,"EL    :   Exploring is good, knowing the place and story"                     , electricityQuest      ,SunStone       ,SolarPanelSiteSelectionMiniGame    ,EnergyMind     ,AreaMaster         );
            Task PrototypeSolar             = new Task("SolarTest"          ,"EL    :   Bulding the solar Panels that are going to be used"                 , electricityQuest      ,lab            ,PrototypeSolarPanelsPuzzle         ,AreaMaster     ,Prototype          );
            Task funding                    = new Task("Funding"            ,"EL    :   Securing the fund for the solarpanels"                              , electricityQuest      ,TownHall       ,SecureFundingMiniGame              ,Prototype      ,FundingPaper       );
            Task InstalingSolarPanel        = new Task("InstallSolarPanel"  ,"EL    :   Instaling the solarPanels"                                          , electricityQuest      ,SunStone       ,ManageInstallationResources        ,FundingPaper   ,Instalation        );
            Task LightShow                  = new Task("LightShow"          ,"EL    :   Light show for the first time EL goes trough the village"           , electricityQuest      ,villageCenter  ,LightUpVillageEvent                ,Instalation    ,null               );

            // The Eco-Friendly Housing quest
            Task designHouses               = new Task("Design"             ,"House :   Designing and chosing how the new homes shall look like"            , housingQutest         , DesignHQ      , DesignAndPlanSustainableHouses    ,null           ,BluePrint          );
            Task Train                      = new Task("Training"           ,"House :   Training and teaching the workers"                                  , housingQutest         ,construtionSite,SourceAndTrainTask                 ,BluePrint      ,TrainingCir        );
            Task modernHome                 = new Task("Home"               ,"House :   Construct the new home"                                             , housingQutest         ,ModernHome     ,ConstructModelHouse                ,TrainingCir    ,null               );


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

            electricityQuest.AddTask(colectKnowlage);
            electricityQuest.AddTask(SunStoneExplor);
            electricityQuest.AddTask(PrototypeSolar);
            electricityQuest.AddTask(funding);
            electricityQuest.AddTask(InstalingSolarPanel);
            electricityQuest.AddTask(LightShow);

            housingQutest.AddTask(designHouses);
            housingQutest.AddTask(Train);
            housingQutest.AddTask(modernHome);


            // add task to the room
            AbandonFactory.AddTask(abandonFactory);
            AbandonFactory.AddTask(ImplementWaterSolutin);

            construtionSite.AddTask(Train);

            villageCenter.AddTask(interview);
            villageCenter.AddTask(LightShow);

            Library.AddTask(ResearchSolution);
            Library.AddTask(colectKnowlage);

            DesignHQ.AddTask(designHouses);

            SunStone.AddTask(SunStoneExplor);
            SunStone.AddTask(InstalingSolarPanel);

            ModernHome.AddTask(modernHome);

            TownHall.AddTask(funding);

            River.AddTask(investigateRiver);

            lab.AddTask(Analysis);
            lab.AddTask(PrototypeSolar);
        
            
            
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
            return $@"";
        }



        /****************** down form here are only tasks*********************/

        private int ConstructModelHouse()
        {
            Printer.PrintLine("Construct a Model House Task");
            Printer.PrintLine("------------------------------------------------");
            Printer.PrintLine("You are tasked with overseeing the construction of a model eco-friendly house. This project will demonstrate sustainable building techniques and use eco-friendly materials.");

            int totalScore = 0;

            // Decision: Foundation Construction
            Printer.PrintLine("Choose the type of foundation : ");
            Printer.PrintLine("1 - Traditional Concrete");
            Printer.PrintLine("2 - Eco-Friendly Concrete");
            Printer.Print("Your choice (Number): ");
            string foundationChoice = Console.ReadLine();
            Console.Clear();
            totalScore += foundationChoice == "2" ? 4 : 1; // Eco-friendly option scores higher

            // Decision: Framing and Insulation
            Printer.PrintLine("Select insulation material: ");
            Printer.PrintLine("1 - Fiberglass");
            Printer.PrintLine("2 - Recycled Material");
            Printer.Print("Your choice (Number): ");
            string insulationChoice = Console.ReadLine();
            Console.Clear();
            totalScore += insulationChoice == "2" ? 4 : 1; // Recycled material scores higher

            // Decision: Finishing
            Printer.PrintLine("Choose exterior materials : ");
            Printer.PrintLine("1 - Standard Paint");
            Printer.PrintLine("2 - Eco-Friendly Paint");
            Printer.Print("Your choice (Number): ");
            string exteriorChoice = Console.ReadLine();
            Console.Clear();
            totalScore += exteriorChoice == "2" ? 4 : 1; // Eco-friendly paint scores higher

            // Quality and Sustainability Check
            Printer.PrintLine("Performing a final check on sustainability features and build quality...");
            Printer.PrintLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            totalScore += 2; // Additional points for successful completion

            Printer.PrintLine($"Construction complete.");
            return totalScore;
        }



        private int SourceAndTrainTask()
        {
            Printer.PrintLine("Source and Train Task");
            Printer.PrintLine("------------------------------------------------");
            Printer.PrintLine("As the morning sun casts its first light over the village, you find yourself at the crossroads of tradition and innovation. Today, you shoulder a pivotal responsibility in the village's journey towards sustainable living.");
            Printer.PrintLine("Your first challenge lies in navigating the bustling local market and the surrounding areas to source materials that will set the foundation for eco-friendly housing.\nThe air is filled with the potential of new beginnings as you ponder choices that balance environmental responsibility with practical needs.");
            Printer.PrintLine("Later, under the shade of an old oak tree, you'll gather with the local construction crew.\nThese skilled artisans, familiar with traditional methods, are eager yet apprehensive about learning modern, sustainable building techniques.\nYour task is to bridge the gap between age-old practices and the new wave of eco-conscious construction.");
            Printer.PrintLine("Every decision you make today will ripple through the future of the village. It's time to blend the wisdom of the past with the innovations of the present.");
            Printer.PrintLine("Press any key to begin your journey...");
            Console.ReadKey();
            Console.Clear();

            int totalScore = 0;

            // Sourcing sustainable materials
            Printer.PrintLine("\nSelect sustainable building materials for the project:");
            Printer.PrintLine("1 - Recycled Steel");
            Printer.PrintLine("2 - Sustainably Sourced Wood");
            Printer.PrintLine("3 - Energy-Efficient Insulation");
            Printer.Print("Your choice (Number): ");
            string materialChoice = Console.ReadLine();
            totalScore += EvaluateMaterialChoice(materialChoice);
            Console.Clear();

            // Training construction crew
            Printer.PrintLine("\nOrganize training sessions on sustainable building techniques:");
            Printer.PrintLine("1 - Efficient Use of Materials");
            Printer.PrintLine("2 - Minimizing Construction Waste");
            Printer.PrintLine("3 - Implementing Green Technologies");
            Printer.Print("Choose a training focus (Number): ");
            string trainingChoice = Console.ReadLine();
            totalScore += EvaluateTrainingChoice(trainingChoice);
            Console.Clear();

            Printer.PrintLine($"Task complete. Total score: {totalScore}");
            return totalScore;
        }


        private int EvaluateMaterialChoice(string choice)
        {
            switch (choice)
            {
                case "1": return 3; // Recycled Steel
                case "2": return 2; // Sustainably Sourced Wood
                case "3": return 3; // Energy-Efficient Insulation
                default: return 0;
            }
        }


        private int EvaluateTrainingChoice(string choice)
        {
            switch (choice)
            {
                case "1": return 3; // Efficient Use of Materials
                case "2": return 2; // Minimizing Construction Waste
                case "3": return 3; // Implementing Green Technologies
                default: return 0;
            }
        }


        private int DesignAndPlanSustainableHouses()
        {
        Printer.PrintLine("Design and Plan Sustainable Houses Task");
        Printer.PrintLine("------------------------------------------------");
        Printer.PrintLine("As the appointed lead of the village's new eco-friendly housing initiative, you step into the local architect's office.\nThe walls are adorned with blueprints and sketches of innovative designs. Today, you embark on a journey to blend the village's traditional charm with the efficiencies of modern sustainability.");
        Printer.PrintLine("Your task: Work hand-in-hand with the renowned architect, Alex Green, to design houses that are not only environmentally friendly but also resonate with the village's aesthetic.\nTogether, you'll create a comprehensive plan for a housing development that sets a new standard for sustainable living.");
        Printer.PrintLine("The decisions you make today will shape the future of the village. It's time to turn your eco-friendly vision into a reality.");
        Printer.PrintLine("Press any key to start the consultation...");
        Console.ReadKey();
        Console.Clear();
            int totalScore = 0;

            // Design decisions
            totalScore += MakeDesignDecisions();

            // Planning the development
            totalScore += PlanHousingDevelopment();

            Printer.PrintLine($"Design and planning complete.");
            return totalScore;
        }


        private int MakeDesignDecisions()
        {
            int score = 0;
            Printer.PrintLine("You are now making key design decisions for the eco-friendly houses.");

            // Architectural Style Decision
            Printer.PrintLine("Choose an architectural style for the houses:");
            Printer.PrintLine("1 - Traditional: Emphasizes local heritage and classic designs.");
            Printer.PrintLine("2 - Modern: Focuses on minimalism and contemporary aesthetics.");
            Printer.PrintLine("3 - Hybrid: A blend of traditional and modern elements.");
            Printer.PrintLine("Your choice (Number): ");
            string styleChoice = Console.ReadLine();
            score += EvaluateDesignChoice(styleChoice, "Architectural Style");
            Console.Clear();

            // Material Selection Decision
            Printer.PrintLine("\nSelect the primary building material:");
            Printer.PrintLine("1 - Locally Sourced Wood: Sustainable and blends with the environment.");
            Printer.PrintLine("2 - Recycled Steel: Durable and modern.");
            Printer.PrintLine("3 - Natural Stone: Traditional and long-lasting.");
            Printer.PrintLine("Your choice (Number): ");
            string materialChoice = Console.ReadLine();
            score += EvaluateDesignChoice(materialChoice, "Material");
            Console.Clear();

            // Energy Efficiency Decision
            Printer.PrintLine("\nDecide on the energy efficiency approach:");
            Printer.PrintLine("1 - Solar Panels: Harness solar energy for electricity.");
            Printer.PrintLine("2 - Geothermal Heating: Utilize underground heat for warming homes.");
            Printer.PrintLine("3 - Passive Design: Maximize natural light and insulation.");
            Printer.PrintLine("Your choice (Number): ");
            string energyChoice = Console.ReadLine();
            score += EvaluateDesignChoice(energyChoice, "Energy Efficiency");
            Console.Clear();

            return score;
        }



        private int PlanHousingDevelopment()
        {
            int score = 0;
            Printer.PrintLine("You are now planning the layout and features of the housing development.");

            // Layout Decision
            Printer.PrintLine("Decide on the overall layout of the housing development:");
            Printer.PrintLine("1 - Clustered: Houses are close together, fostering community interaction.");
            Printer.PrintLine("2 - Spread Out: Houses are more spaced apart, providing privacy.");
            Printer.PrintLine("Your choice (1, 2): ");
            string layoutChoice = Console.ReadLine();
            score += EvaluateDesignChoice(layoutChoice,"Layout");
            Console.Clear();

            // Green Space Decision
            Printer.PrintLine("\nPlan the green spaces within the development:");
            Printer.PrintLine("1 - Central Park: A large communal green space in the center.");
            Printer.PrintLine("2 - Scattered Gardens: Multiple small green spaces throughout.");
            Printer.PrintLine("Your choice (1, 2): ");
            string greenSpaceChoice = Console.ReadLine();
            score += EvaluateDesignChoice(greenSpaceChoice,"GreenSpace");
            Console.Clear();

            // Infrastructure Decision
            Printer.PrintLine("\nDetermine the infrastructure approach:");
            Printer.PrintLine("1 - Eco-Friendly Public Transport: Emphasize shared transportation options.");
            Printer.PrintLine("2 - Pedestrian-Friendly Design: Prioritize walking and cycling paths.");
            Printer.PrintLine("Your choice (1, 2): ");
            string infrastructureChoice = Console.ReadLine();
            score += EvaluateDesignChoice(infrastructureChoice,"Infrastructure");
            Console.Clear();

            return score;
        }


        private int EvaluateDesignChoice(string choice, string category)
        {
            switch (category)
            {
                case "Architectural Style":
                    return choice == "3" ? 3 : 2;
                case "Material":
                    return choice == "1" ? 3 : 2; 
                case "Energy Efficiency":
                    return choice == "1" ? 3 : 2; 
                case "Layout":
                    return choice == "1" ? 3 : 2;
                case "GreenSpace":
                    return choice == "1" ? 3 : 2;
                case "Infrastructure":
                    return choice == "2" ? 3 : 2;
                default:
                    return 0;
            }
        }


        private int LightUpVillageEvent()
        {
            string[] villageBefore = new string[]
            {
                "    ___     ",
                "   /   \\   ",
                "  /     \\  ",
                " /_______\\ ",
                " |       | ",
                " |[     ]| ",
                " |       | "
            };

            string[] villageAfter = new string[]
            {
                "    ___     ",
                "   /   \\   ",
                "  /     \\  ",
                " /_______\\ ",
                " |       | ",
                " |[Light]| ",
                " |       | "
            };

            Printer.PrintLine("Lighting Up the Village Event...");
            Printer.PrintLine("The solar panels are now powering the village!");

            // Display the village before lighting up
            foreach (var line in villageBefore)
            {
                Console.WriteLine(line);
                Thread.Sleep(500); // Delay for effect
            }

            Thread.Sleep(1000); // Pause before the lights turn on

            Console.Clear();

            // Display the village after lighting up
            foreach (var line in villageAfter)
            {
                Console.WriteLine(line);
                Thread.Sleep(500); // Delay for effect
            }

            Printer.PrintLine("\nThe village is lit up! The event is a success!");
            return 10;
        }


        private int PresentEnvironmentalBenefits()
        {
            Printer.PrintLine("\nPresenting Environmental Benefits:");
            Printer.PrintLine("You begin to explain the environmental benefits of the solar power project.");
            
            int points = 0;
            string[] benefits = {
                "Reduces carbon emissions by replacing fossil fuels",
                "Promotes biodiversity by minimizing habitat disruption",
                "Supports sustainable community development",
                "Decreases air and water pollution"
            };

            foreach (var benefit in benefits)
            {
                Printer.Print($"Do you want to mention this benefit?\n'{benefit}'\n (yes/no): ");
                string response = Console.ReadLine().ToLower();

                if (response == "yes")
                {
                    points++;
                    Printer.PrintLine($"You highlight: '{benefit}'");
                }
                Printer.PrintLine("Press any key togo to next question...");
                Console.ReadKey();
                Console.Clear();              
            }

            if (points > 0)
            {
                Printer.PrintLine("Your points about environmental benefits are well-received.");
            }
            else
            {
                Printer.PrintLine("You missed discussing the key environmental benefits.");
            }
            Printer.PrintLine("Press any key togo to next question...");
            Console.ReadKey();
            Console.Clear();
            return points;
        }


        private int ManageInstallationResources()
        {
            Printer.PrintLine("\nManage the Installation Resources:");
            Printer.PrintLine("You need to allocate workers to various tasks and guide them through the installation steps.");

            int totalScore = 0;
            var tasks = new Dictionary<string, string>
            {
                { "Site Preparation"    , "Clearing the area and setting up the base for panels."       },
                { "Panel Assembly"      , "Assembling the solar panels and wiring."                     },
                { "Panel Installation"  , "Mounting the panels and connecting them to the power grid."  }
            };

            foreach (var task in tasks)
            {
                Printer.PrintLine($"\nTask: {task.Key}");
                Printer.PrintLine($"Description: {task.Value}");
                Printer.PrintLine("Allocate workers (1 - Few, 2 - Many): \n");
                Printer.Print("Awnser (Number) : ");
                string workerAllocation = Console.ReadLine();

                Printer.PrintLine("\nChoose installation step (1 - Start, 2 - Middle, 3 - Finalize): ");
                Printer.Print("Awnser (Number) : ");
                string installationStep = Console.ReadLine();

                int taskScore = EvaluateTaskManagement(task.Key, workerAllocation, installationStep);
                totalScore += taskScore;
                
                Printer.PrintLine("Press any key togo to next question...");
                Console.ReadKey(); 
                Console.Clear();       
            }

            Printer.PrintLine($"Installation management complete.");
            return totalScore;
        }


        private int EvaluateTaskManagement(string task, string workerAllocation, string installationStep)
        {
            // Scoring logic based on the effectiveness of decisions
            int score = 0;

            // Example scoring for worker allocation
            score += workerAllocation == "2" ? 1 : 0;

            // Example scoring for installation steps
            switch (installationStep)
            {
                case "1": // Start
                    score += task == "Site Preparation" ? 2 : 1;
                    break;
                case "2": // Middle
                    score += task == "Panel Assembly" ? 2 : 1;
                    break;
                case "3": // Finalize
                    score += task == "Panel Installation" ? 2 : 1;
                    break;
                default:
                    score += 0; // No score for invalid choices
                    break;
            }

            return score;
        }


        private int SecureFundingMiniGame()
        {
            Printer.PrintLine("Secure Funding Task");
            Printer.PrintLine("------------------------------------------------");
            Printer.PrintLine("You are in the village hall, ready to present the solar power project to the village council and local business owners.");
            Printer.PrintLine("Your goal is to persuade them to invest in the project. You will need to highlight the benefits and address any concerns they might have.");
            Printer.PrintLine("Make your case compelling to secure the necessary funding.");
            Printer.PrintLine("Press any key to begin your presentation...");
            Console.ReadKey();
            Console.Clear();
            int totalScore = 0;

            // Presentation and negotiation
            totalScore += PresentEnvironmentalBenefits();
            totalScore += PresentEconomicAdvantages();
            totalScore += AddressConcernsAboutCost();
            totalScore += AddressConcernsAboutMaintenance();

            // Evaluate the outcome based on the total score
            Printer.PrintLine("\nYour presentation is complete.");
            if (totalScore >= 12) // Threshold for successful funding
            {
                Printer.PrintLine("Congratulations! The council and business owners are impressed and agree to fund the project.");
            }
            else
            {
                Printer.PrintLine("The stakeholders are hesitant. It seems you need to refine your proposal and address their concerns more effectively.");
            }

            return totalScore;
        }    


        private int PresentEconomicAdvantages()
        {
            Printer.PrintLine("\nPresenting Economic Advantages:");
            Printer.PrintLine("You are ready to discuss the economic benefits of the solar power project.");

            int points = 0;
            string[] economicAdvantages = {
                "Long-term cost savings due to reduced energy bills",
                "Job creation in solar panel installation and maintenance",
                "Boost to local businesses through increased energy independence",
                "Attracting new investments and eco-tourism to the village"
            };

            foreach (var advantage in economicAdvantages)
            {
                Printer.Print($"Do you want to emphasize this advantage? '{advantage}'\n (yes/no): ");
                string response = Console.ReadLine().ToLower();

                if (response == "yes")
                {
                    points++;
                    Printer.PrintLine($"You emphasize: '{advantage}'");
                }
                Printer.PrintLine("Press any key togo to next question...");
                Console.ReadKey(); 
                Console.Clear();               
            }

            if (points > 0)
            {
                Printer.PrintLine("Your presentation on economic advantages resonates with the audience.");
            }
            else
            {
                Printer.PrintLine("You missed discussing the key economic advantages.");
            }
            Printer.PrintLine("Press any key togo to next question...");
            Console.ReadKey();
            Console.Clear();
            return points; 
        }


        private int AddressConcernsAboutCost()
        {
            Printer.PrintLine("\nAddressing Concerns About Cost:");
            Printer.PrintLine("A council member raises a concern: 'This solar power project sounds promising, but what about the initial investment? How do we manage the costs?'");

            string[] responses = {
                "We plan to utilize government grants and renewable energy incentives to offset initial costs.",
                "We'll start small and expand gradually, managing costs as we go.",
                "The community will fund the entire project through donations."
            };

            Printer.PrintLine("How do you respond? (Choose 1, 2, or 3)");
            for (int i = 0; i < responses.Length; i++)
            {
                Printer.PrintLine($"{i + 1}: {responses[i]}");
            }

            string chosenResponse = Console.ReadLine();
            switch (chosenResponse)
            {
                case "1":
                    Printer.PrintLine("You explain: 'We plan to utilize government grants and renewable energy incentives to offset initial costs.'");
                    Printer.PrintLine("\nPress any key togo to next question...");
                    Console.ReadKey();  
                    Console.Clear();                 
                    return 3; // Most effective response
                case "2":
                    Printer.PrintLine("You suggest: 'We'll start small and expand gradually, managing costs as we go.'");
                    Printer.PrintLine("\nPress any key togo to next question...");
                    Console.ReadKey();  
                    Console.Clear();                 
                    return 2; // Moderately effective response
                case "3":
                    Printer.PrintLine("You propose: 'The community will fund the entire project through donations.'");
                    Printer.PrintLine("\nPress any key togo to next question...");
                    Console.ReadKey();      
                    Console.Clear();              
                    return 1; // Least effective response
                default:
                    Printer.PrintLine("Invalid response. The council looks confused.");
                    Printer.PrintLine("\nPress any key togo to next question...");
                    Console.ReadKey();
                    Console.Clear();
                    return 0; // No points for invalid response
            }
            
        }


        private int AddressConcernsAboutMaintenance()
        {
            Printer.PrintLine("\nAddressing Concerns About Maintenance:");
            Printer.PrintLine("A local business owner raises a concern: 'Solar panels are great, but what about the long-term maintenance costs?'");

            string[] responses = {
                "The maintenance costs are relatively low, and we'll train local technicians to handle it, creating more jobs.",
                "We'll set up a maintenance fund contributed by all beneficiaries of the project.",
                "Maintenance will be managed by a specialized company at a fixed annual fee."
            };

            Printer.PrintLine("How do you respond? (Choose 1, 2, or 3)");
            for (int i = 0; i < responses.Length; i++)
            {
                Printer.PrintLine($"{i + 1}: {responses[i]}");
            }

            string chosenResponse = Console.ReadLine();
            switch (chosenResponse)
            {
                case "1":
                    Printer.PrintLine("You explain: 'The maintenance costs are relatively low, and we'll train local technicians to handle it, creating more jobs.'");
                    Printer.PrintLine("\nPress any key togo to next question...");
                    Console.ReadKey();              
                    Console.Clear();      
                    return 3; // Most effective response
                case "2":
                    Printer.PrintLine("You suggest: 'We'll set up a maintenance fund contributed by all beneficiaries of the project.'");
                    Printer.PrintLine("\nPress any key togo to next question...");
                    Console.ReadKey();    
                    Console.Clear();                
                    return 2; // Moderately effective response
                case "3":
                    Printer.PrintLine("You propose: 'Maintenance will be managed by a specialized company at a fixed annual fee.'");
                    Printer.PrintLine("\nPress any key togo to next question...");
                    Console.ReadKey();     
                    Console.Clear();               
                    return 1; // Least effective response
                default:
                    Printer.PrintLine("Invalid response. The business owner looks unsatisfied.");
                    Printer.PrintLine("\nPress any key togo to next question...");
                    Console.ReadKey();       
                    Console.Clear();             
                    return 0; // No points for invalid response
            }
        }


        private int PrototypeSolarPanelsPuzzle()
        {
            Printer.PrintLine("Prototype Solar Panels Task");
            Printer.PrintLine("------------------------------------------------");
            Printer.PrintLine("Welcome to the village workshop! Your task is to prototype an efficient solar panel using available components.");
            Printer.PrintLine("You'll need to choose the right type of photovoltaic cells, inverter, battery, and frame material.");
            Printer.PrintLine("Each choice affects the overall efficiency of the solar panel. Aim for the highest efficiency possible.");
            Printer.PrintLine("Let's get started!");
            Printer.PrintLine("Press any key to begin...");
            Console.ReadKey();

            var components = new Dictionary<string, string>
            {
                { "Photovoltaic Cells", "Choose type: Monocrystalline, Polycrystalline, Thin-Film" },
                { "Inverter", "Choose type: String, Central, Micro" },
                { "Battery", "Choose capacity: High, Medium, Low" },
                { "Frame", "Choose material: Aluminum, Steel, Composite" }
            };

            int efficiencyScore = 0;

            foreach (var component in components)
            {
                Console.Clear();
                Printer.PrintLine($"Component: {component.Key}");
                Printer.PrintLine($"Options: {component.Value}");
                Printer.Print("Your choice: ");
                string choice = Console.ReadLine().ToLower();

                efficiencyScore += EvaluateComponentChoice(component.Key, choice);
            }

            Printer.PrintLine("\nTesting the solar panel prototype...");
            if (efficiencyScore > 10) // Example threshold for a successful prototype
            {
                Printer.PrintLine("The prototype is highly efficient! Good job!");
            }
            else
            {
                Printer.PrintLine("The prototype could be improved. Try adjusting the components.");
            }

            return efficiencyScore;
        }

       
        private int EvaluateComponentChoice(string component, string choice)
        {
            // Example scoring logic
            switch (component)
            {
                case "Photovoltaic Cells":
                    return (choice == "monocrystalline") ? 5 : 2;
                case "Inverter":
                    return (choice == "micro") ? 4 : 2;
                case "Battery":
                    return (choice == "high") ? 3 : 1;
                case "Frame":
                    return (choice == "aluminum") ? 3 : 1;
                default:
                    return 0;
            }
        }
      
      
        private int SolarPanelSiteSelectionMiniGame()
        {
                Console.Clear();
            Printer.PrintLine("Solar Panel Site Selection Task");
            Console.WriteLine("------------------------------------------------");
            Printer.PrintLine("You are about to scout the Sun Stone area for potential solar panel installation sites.");
            Printer.PrintLine("There are three locations to evaluate, but be cautious: not all of them are optimal.");
            Printer.PrintLine("Your choices will impact the village's solar energy project and your social score.");
            Printer.PrintLine("Choose wisely!");
            Printer.PrintLine("Press any key to begin...");
            Console.ReadKey();
            var locations = new Dictionary<string, (string Description, int Score)>
            {
                { "Near the Ancient Tree"   , ("Good sunlight, but roots may interfere with installation."      , -4 )  },
                { "Atop the Hill"           , ("Excellent sunlight, but the ground is uneven."                  , 10 )  },
                { "Beside the Old Ruins"    , ("Moderate sunlight, stable ground, but close to historical site.", -2 )  }
            };

            int totalScore = 0;

            foreach (var location in locations)
            {
                Console.Clear();
                Printer.PrintLine($"Location: {location.Key}");
                Printer.PrintLine($"Assessment: {location.Value.Description}");
                Printer.Print("Do you approve this location for solar panels? (yes/no): ");
                string approval = Console.ReadLine().ToLower();

                if (approval == "yes")
                {
                    if (location.Key == "Atop the Hill")
                    {
                        Printer.PrintLine("you've made a wise choise, this is the best sutible area");
                    }else
                    {
                        Printer.PrintLine("ufff you will bee interfering with sorunding history which will not go well with the locals");
                    }
                    totalScore += location.Value.Score;
                    Printer.PrintLine($"Location '{location.Key}' approved.");
                    break;
                }
                else
                {
                    Printer.PrintLine($"Location '{location.Key}' rejected.");
                }
                Printer.PrintLine("Press any key to continue...");
                Console.ReadKey();
            }

            Printer.PrintLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Printer.PrintLine($"Total score for site selection: {totalScore}");
            return totalScore; // The total score reflects the suitability of chosen locations
        }
      
      
        private int EnergyQuizMiniGame()
        {
            var questions = new Dictionary<string, List<string>>
            {
                { "What is the main benefit of using solar energy?", new List<string> { "It's renewable", "It's cheap", "It's unlimited", "It's colorful" } },
                { "How do solar panels generate electricity?", new List<string> { "Using wind", "Using water", "Converting sunlight into electricity", "Burning fossil fuels" } },
                { "What can affect the efficiency of a solar panel?", new List<string> { "The color of the panel", "The time of day", "The panel's size", "The weather and placement" } }
            };

            var correctAnswers = new List<int> { 0, 2, 3 }; // Indices of correct answers
            int score = 0;

            foreach (var question in questions)
            {

                Printer.PrintLine($"\nQuestion: {question.Key}",25);
                    

                for (int i = 0; i < question.Value.Count; i++)
                {
                    Printer.PrintLine($"{i + 1}. {question.Value[i]}",35);
                }

                Printer.Print("Your answer (number): ",25);
                if (int.TryParse(Console.ReadLine(), out int answerIndex) && answerIndex > 0 && answerIndex <= question.Value.Count)
                {
                    if (answerIndex - 1 == correctAnswers[questions.Keys.ToList().IndexOf(question.Key)])
                    {
                        Printer.PrintLine("Correct!");
                        score++;
                    }
                    else
                    {
                        Printer.PrintLine("Incorrect.");
                    }
                }
                else
                {
                    Printer.PrintLine("Invalid answer. please use your brain next time");
                }
                Console.WriteLine("Press any key to advance to next question...");
                Console.ReadKey();
                Console.Clear();


            }

            Console.WriteLine($"\nQuiz complete. Your score: {score}/{questions.Count}");
            return score;
        }
   
   
        private int ImplementPurificationSolution()
        {
            Printer.PrintLine("With a clear plan and the community's support, you begin the implementation of the water purification system.");

            string header = "With a clear plan and the community's support, you begin the implementation of the water purification system.\nYou can only chose one way of action\n\nSelect an action:";
            string[] options = {
                "Organize a community cleanup",
                "Start construction of the filtration system",
                "Coordinate with local authorities for support"
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
                            Printer.PrintLine("The community cleanup was a success!");
                            purificationComplete = true;
                        }
                        break;
                    case 1:
                        if (ConstructionPuzzleMiniGame()) // Returns true if successful
                        {
                            Printer.PrintLine("The construction of the filtration system is complete!");
                            purificationComplete = true;
                        }
                        break;
                    case 2:
                        if (ResourceNegotiationMiniGame()) // Returns true if successful
                        {
                            Printer.PrintLine("You have successfully secured support from local authorities!");
                            purificationComplete = true;
                        }
                        break;
                    default:
                        Printer.PrintLine("Invalid choice. Please select a valid action.");
                        break;
                }
                Console.ReadKey();
            }

            int scoreChange = 10; // Example score change
            Printer.PrintLine("The water purification system is now operational.");
            return scoreChange;
        }
      
      
        private bool ConstructionPuzzleMiniGame()
        {
            while (true)
            {
                    
                var components      = new List<string> { "Sand Filter", "Chlorination Tank", "UV Light Disinfection", "Reverse Osmosis Membrane" };
                var correctOrder    = new List<string> { "Sand Filter", "Reverse Osmosis Membrane", "Chlorination Tank", "UV Light Disinfection" };
                var playerOrder     = new List<string>();
                string[] options ={};

                int attempts = 0;
                    
                while (playerOrder.Count < components.Count)
                {

                    var availableComponents = components.Where(c => !playerOrder.Contains(c)).ToList();

                    // Convert the available components to an array
                    options = availableComponents.ToArray();
            
                    string header = "You need to assemble the water filtration system. Place the components in the correct order.\nThere is an ideal way to asemble it try until you sucess\nAvailable components:";
                    int indexNumber = InteractiveMenu.MultichoiceQuestion(header,options);


                    Console.Write($"you have added {options[indexNumber]} to the list");
                    

                    playerOrder.Add(options[indexNumber]);

                    Console.ReadKey();
                    attempts++;
                }

                if (playerOrder.SequenceEqual(correctOrder))
                {
                    Printer.PrintLine("\nFiltration system successfully assembled!");
                    return true;
                }
                else
                {
                    Printer.PrintLine("\nThe components are not in the correct order. Please try again.");
                }
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
                Printer.PrintLine($"\nAuthority: {question}");
                for (int i = 0; i < responses[question].Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {responses[question][i]}");
                }

                Printer.Print("Your response: ");
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
                    Printer.PrintLine("Invalid response. Please try again.");
                }
            }

            if (successfulResponses >= questions.Count)
            {
                Printer.PrintLine("\nYou have successfully negotiated support from the local authorities!");
                return true;
            }
            else
            {
                Printer.PrintLine("\nYou were unable to secure the necessary support.");
                return false;
            }
        }
    
    
        private bool CleanupCoordinationMiniGame()
        {
            var areas = new List<string> { "north bank", "south bank", "east bank", "west bank" };
            var volunteers = new Dictionary<string, string>
            {
                { "Alice"   , "Collecting"      },
                { "Bob"     , "Heavy Lifting"   },
                { "Charlie" , "Sorting"         },
                { "Diana"   , "Scouting"        }
            };

            int turns = 4; // Each volunteer gets one turn
            int cleanedAreas = 0;

            Printer.PrintLine("You need to assign volunteers to clean different areas of the riverbank.");

            

            for (int i = 0; i < turns; i++)
            {
                Console.WriteLine("\nAreas to clean: " + string.Join(", ", areas));
                Console.WriteLine("Available volunteers and their skills:");
                foreach (var volunteer in volunteers)
                {
                    Console.WriteLine($"- {volunteer.Key} (Skill: {volunteer.Value})");
                }

                Console.Write("Choose a volunteer to assign: ");
                string? volunteerName = Console.ReadLine();
                Console.Write("Choose an area to clean: ");
                string? area = Console.ReadLine();

                // Simple logic to determine if the choice was effective
                if (volunteers.ContainsKey(volunteerName) && areas.Contains(area))
                {
                    Printer.PrintLine($"{volunteerName} is cleaning the {area}.");
                    cleanedAreas++; // Increment for each successful assignment
                    areas.Remove(area); // Remove the cleaned area
                }
                else
                {
                    Printer.PrintLine("Invalid choice. Please try again.");
                    i--; // Allow the player to make the choice again
                }
                Console.ReadKey();
                Console.Clear();
            }

            Printer.PrintLine($"\nCleanup complete. You successfully cleaned {cleanedAreas} areas of the riverbank.");
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
                        Printer.PrintLine("\nYou learn about traditional methods like sand filtration and boiling. While effective, they are less practical for large-scale purification.");
                        Printer.PrintLine("On how to build one the ideal way to asemble is Sand Filter, Reverse Osmosis Membrane, Chlorination Tank, UV Light Disinfection");
                        SocialScore += 5; // Less sustainable, moderate score increase
                        break;
                    case 1:
                        Printer.PrintLine("\nChemical chlorination is highly effective but has environmental drawbacks due to chemical usage.");
                        SocialScore += 2; // Least sustainable, lower score increase
                        break;
                    case 2:
                        Printer.PrintLine("\nNatural biofiltration uses plants to remove contaminants. It's highly sustainable and eco-friendly.");
                        SocialScore += 10; // Most sustainable, highest score increase
                        break;
                    default:
                        Printer.PrintLine("Invalid choice. Please select a valid purification method.");
                        break;
                }
                Console.ReadKey();
            

            Printer.PrintLine("Research complete. You have identified a sustainable purification method for the river.");
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
                    Printer.PrintLine("\nYou add chemical reagents to the water sample. The solution changes color, indicating the presence of heavy metals.");      
                    break;
                case 1:
                    Printer.PrintLine("\nUnder the microscope, you observe abnormal bacterial activity, suggesting contamination.");                  
                    break;
                case 2:
                    Printer.PrintLine("\nThe spectrometer reveals unusual organic compounds in the water, likely pollutants.");
                    break;
                default:
                    Printer.PrintLine("\nInvalid choice. Please select a valid analysis method.");
                    break;
            }

             Printer.PrintLine("Analysis complete. You have identified the contaminants in the water.");
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

                        int selectedIndex1 = InteractiveMenu.MultichoiceQuestion(header1, options1);
                        if (selectedIndex1 == 0)
                        {
                            Printer.PrintLine("After some effort, the cabinet creaks open, revealing documents about illegal waste disposal practices.");
                            foundEvidence = true;
                        }else
                        {
                            Printer.PrintLine("Since the room didn't entail anything else, you went back down to the main floor");
                        }
                        break;
                    case 1:
                        Printer.PrintLine("The production floor is littered with remnants of the factory's past operations. You notice a trail of dried chemical residues leading towards the back.");
                        break;
                    case 2:
                        Printer.PrintLine("In the storage area, you find barrels labeled with hazardous material warnings, some of them leaking a strange substance.");
                        break;
                    default:
                        Printer.PrintLine("Invalid choice. Please select a valid option.");
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
                        Printer.PrintLine("\nFisherman: \n'The fish are gone, and the water smells strange. It's not like it used to be.'");
                        Printer.PrintLine("'ever sice that factory shut down'");
                        infoCollected.Add("fisherman");

                    }
                    else
                    {
                        Printer.PrintLine("You have already talked to the fisherman.");
                    }
                    break;
                case 1:
                    if (!infoCollected.Contains("shopkeeper"))
                    {
                        Printer.PrintLine("Shopkeeper: 'People have been complaining about the water quality. It's affecting my business too.'");
                        infoCollected.Add("shopkeeper");
                    }
                    else
                    {
                        Printer.PrintLine("You have already talked to the shopkeeper.");
                    }
                    break;
                case 2:
                    if (!infoCollected.Contains("children"))
                    {
                        Printer.PrintLine("Children: 'We don't play near the river anymore, it's yucky and smells bad!'");
                        infoCollected.Add("children");
                    }
                    else
                    {
                        Printer.PrintLine("You have already talked to the children.");
                    }
                    break;
                default:
                    Printer.PrintLine("Invalid choice. Please select a valid option.");
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
            Printer.PrintLine("You observe the river's murky waters and notice a decrease in wildlife.");
            Printer.PrintLine("You collect water samples from different parts of the river.\n");

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