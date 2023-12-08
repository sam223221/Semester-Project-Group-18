using System;
using System.Reflection;

namespace WorldOfZuul
{
    public class Chapter1Farmer : IChapter // Ensure it implements IChapter
    {
        public bool IsCompleted { get; set; }
        public List<Room> Rooms { get; private set; }
        public List<Quest> Quests { get; set; }
        private Room? village;
        private Room? house;
        private Room? market;
        private Room? farm;
        private Room? lake;
        private Room? watermill;
        public Chapter1Farmer()
        {
            Rooms = new List<Room>();
            Quests = new List<Quest>();
        }

        // Introduction 

        public void ShowIntroduction()
        {
            string[] introSlides = new string[] { "FarmerIntroI", "FarmerIntroII" };

            foreach (var slide in introSlides)
            {
                Console.Clear();
                Console.WriteLine(TextArtManager.GetTextArt(slide));
                Console.ReadKey();
            }
        }

        // Start room

        public Room GetStartRoom() => village;

        // Creates rooms and quests

        public void CreateRoomsAndQuests()
        {

            // Initialize items                          

            Item villageDialogueItem = new Item("a talk with the Villager", null, "Village");
            Item sustFarmCertificate = new Item("SustainableFarmingCertificate", null, "Watermill");
            Item lostanimalsAnimals = new Item("stray dogs", null, "Lake Area");
            Item lostanimalsSnack = new Item("meat", null, "Farm");
            Item organicMaterials = new Item("organic materials", null, "Market");
            Item lostanimalsInfo = new Item("information about lost animals", null, "Lake");
            Item sustFarmBasics = new Item("SustainableFarmingCourse", null, "Watermill");
            Item sdgCertificate = new Item("SDGCertificate", null, "House");
            Item solarpanelItem = new Item("solar panels", null, "Renewable Energy Initiative Quest");
            Item fertilizer = new Item("fertilizer", null, "Farming Adventure Quest");
            Item sdgBasics = new Item("SDGCourse", null, "House");
            Item investor = new Item("a talk with the Investor", null, "Market");

            // Initialize rooms          

            watermill = new Room("Watermill", "A watermill owned by your friend, a good sustainable farmer... \nWhat could be found there?");
            village = new Room("Village", "You are at the center of your beautiful village!");
            market = new Room("Market", "You are on the village market... Look around!");
            house = new Room("House", "House is your knowledge temple... You better use it!");
            farm = new Room("Farm", "Welcome to the farm... Mr. Farmer!");
            lake = new Room("Lake", "What you see is a lovely lake right in front of you!");


            // Set room's exits       

            market.SetExits(village, watermill, null, null);
            watermill.SetExits(farm, null, null, market);
            village.SetExits(null, farm, market, house);
            house.SetExits(null, village, null, lake);
            farm.SetExits(null, null, watermill, village);
            lake.SetExits(null, house, null, null);


            // Create quests                        

            Quest watermillQuest = new Quest("Sustainable Farming Wisdom", "Gather and test your knowledge about sustainable farming!");
            Quest villageQuest = new Quest("Renewable Energy Initiative", "Work your way to help yourself and the community!");
            Quest houseQuest = new Quest("SDG Wisdom", "Gather and test your knowledge about SDG's!");
            Quest lakeQuest = new Quest("Lost Animals", "Rescue animals that got lost around the lake area!");
            Quest farmQuest = new Quest("FarmingAdventure", "Sustainable farming practises!");


            // Initialize tasks                                                                               

            Task watermillCourseSustFarTask = new Task("SustainableFarmCourse", "Look up sustainable farming techniques!", watermillQuest, watermill, SustFarmCourse, null, sustFarmBasics);
            Task watermillQuizSustFarTask = new Task("SustainableFarmQuiz", "Complete a Quiz about Sustainable Farming.", watermillQuest, watermill, SustFarmQuizPlay, sustFarmBasics, sustFarmCertificate);

            Task houseCourseSDGTask = new Task("SDGCourse", "Might be useful for completing the quiz...", houseQuest, house, SDGCourse, null, sdgBasics);
            Task houseQuizSDGTask = new Task("SDGQuiz", "Complete a Quiz about Sustainable Development Goals.", houseQuest, house, SDGQuizPlay, sdgBasics, sdgCertificate);

            Task solarpanelsVillagerDialogue = new Task("VillagerTalk", "Seems like someone is courious about your work...", villageQuest, village, VillageDialogue, sustFarmBasics, villageDialogueItem);
            Task solarpanelsMrInvestorDialogue = new Task("MrMicheal", "Friend of a friend?", villageQuest, village, MichealDialogue, villageDialogueItem, investor);
            Task solarpanelsGet = new Task("RenewableEnergy", "You need some chats...", villageQuest, farm, SolarPanels, investor, solarpanelItem);
            Task solarpanelsOpenHouse = new Task("OpenHouseDay", "Open the doors of your farm!", villageQuest, farm, OpenHouseDay, solarpanelItem, null);

            Task lostanimalsInformation = new Task("LostAnimals", "Gather information about lost animals next to the lake area.", lakeQuest, lake, LakeInformation, null, lostanimalsInfo);
            Task lostanimalsMeat = new Task("Meat", "Get a snack to pet the dogs...", lakeQuest, farm, MeatCollect, lostanimalsInfo, lostanimalsSnack);
            Task lostanimalsFind = new Task("LookAround", "Look for the lost animals!", lakeQuest, lake, LookForAnimals, lostanimalsSnack, lostanimalsAnimals);
            Task lostanimalsShelters = new Task("AnimalShelter", "Build a shelter for the lost animals", lakeQuest, farm, AnimalShelters, lostanimalsAnimals, null);

            Task fertilizeBuyOrganicMaterials = new Task("Shopping", "Buy organic materials to create organic fertilizers", farmQuest, market, GetOrganicMaterials, null, organicMaterials);
            Task fertilizeMixer = new Task("Fertilizer", "Create the best fertilizer for your farm!", farmQuest, farm, MixerFertilizer, organicMaterials, fertilizer);
            Task fertilizeUse = new Task("UseFertilizer", "Use the fertilizer on your farm!", farmQuest, farm, UseFertilizer, fertilizer, null);


            // Add quests to the chapter's quest list

            Quests.Add(watermillQuest);
            Quests.Add(houseQuest);
            Quests.Add(villageQuest);
            Quests.Add(lakeQuest);
            Quests.Add(farmQuest);

            // Add task to the quest list

            watermillQuest.AddTask(watermillCourseSustFarTask);
            watermillQuest.AddTask(watermillQuizSustFarTask);

            houseQuest.AddTask(houseCourseSDGTask);
            houseQuest.AddTask(houseQuizSDGTask);

            villageQuest.AddTask(solarpanelsVillagerDialogue);
            villageQuest.AddTask(solarpanelsMrInvestorDialogue);
            villageQuest.AddTask(solarpanelsGet);
            villageQuest.AddTask(solarpanelsOpenHouse);

            farmQuest.AddTask(fertilizeBuyOrganicMaterials);
            farmQuest.AddTask(fertilizeMixer);
            farmQuest.AddTask(fertilizeUse);


            lakeQuest.AddTask(lostanimalsInformation);
            lakeQuest.AddTask(lostanimalsMeat);
            lakeQuest.AddTask(lostanimalsFind);
            lakeQuest.AddTask(lostanimalsShelters);

            // Add task to the room

            watermill.AddTask(watermillCourseSustFarTask);
            watermill.AddTask(watermillQuizSustFarTask);

            house.AddTask(houseCourseSDGTask);
            house.AddTask(houseQuizSDGTask);

            village.AddTask(solarpanelsVillagerDialogue);
            market.AddTask(solarpanelsMrInvestorDialogue);
            farm.AddTask(solarpanelsGet);
            farm.AddTask(solarpanelsOpenHouse);

            farm.AddTask(lostanimalsMeat);
            farm.AddTask(lostanimalsShelters);
            lake.AddTask(lostanimalsInformation);
            lake.AddTask(lostanimalsFind);

            market.AddTask(fertilizeBuyOrganicMaterials);
            farm.AddTask(fertilizeMixer);
            farm.AddTask(fertilizeUse);

            // Adding things to rooms

            Rooms.Add(watermill);
            Rooms.Add(house);
            Rooms.Add(village);
            Rooms.Add(market);
            Rooms.Add(lake);
            Rooms.Add(farm);

        }

        //Task section

        private int SDGCourse()
        {
            string header = $"{TextArtManager.GetTextArt("FarmerBooks")} \nHere is a little course on Sustainable Development Goals!\nPick a number to learn more!";
            string[] option =
                { "SDG 1: No Poverty", "SDG 2: Zero Hunger London", "SDG 3: Good Health and Well-being", "SDG 4: Quality Education", "SDG 5: Gender Equality",
                  "SDG 6: Clean Water and Sanitation", "SDG 7: Affordable and Clean Energy", "SDG 8: Decent Work and Economic Growth", "SDG 9: Industry, Innovation, and Infrastructure",
                  "SDG 10: Reduced Inequality", "SDG 11: Sustainable Cities and Communities", "SDG 12: Responsible Consumption and Production", "SDG 13: Climate Action",
                  "SDG 14: Life Below Water", "SDG 15: Life on Land",  "SDG 16: Peace, Justice, and Strong Institutions", "SDG 17: Partnerships for the Goals", "SDG: Exit"};

            while (true)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, option);

                switch (selectedIndex)
                {
                    case 0:
                        Console.WriteLine();
                        Console.WriteLine("\nEnd poverty in all its forms everywhere by \npromoting equal access to resources, \nsocial protection systems, \nand sustainable economic growth.");
                        Console.ReadKey();
                        break;

                    case 1:
                        Console.WriteLine("\nSeek to end hunger, achieve food security, \nimprove nutrition, and promote sustainable \nagriculture by addressing issues such \nas food distribution or agricultural practices.");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("\nFocus on ensuring healthy lives and \npromoting well-being for all, with \ntargets including combating communicable \ndiseases or strengthening health systems.");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("\nEnsure that every child, regardless of gender, \nsocioeconomic background, or location, has access to inclusive \nand high-quality education");
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.WriteLine("\nStrive for gender equality and the empowerment \nof all women and girls by addressing issues \nsuch as discrimination, violence, and unequal \nopportunities in various aspects of life.");
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.WriteLine("\nFocus on ensuring availability and \nsustainable management of water and sanitation \nfor all, addressing issues like water scarcity, \nwater quality, and access to basic sanitation facilities.");
                        Console.ReadKey();
                        break;

                    case 6:
                        Console.WriteLine("\nEnsure access to affordable, reliable, \nsustainable, and modern energy for all by promoting \nrenewable energy sources and improving energy efficiency.");
                        Console.ReadKey();
                        break;

                    case 7:
                        Console.WriteLine("\nPromote sustained, inclusive, and sustainable economic \ngrowth, full and productive employment, and decent work for all, \naddressing issues like job creation, labor rights, and fair wages.");
                        Console.ReadKey();
                        break;

                    case 8:
                        Console.WriteLine("\nFocus on building resilient infrastructure, \npromoting inclusive and sustainable industrialization, \nand fostering innovation to support economic development.");
                        Console.ReadKey();
                        break;
                    case 9:
                        Console.WriteLine("\nAim to reduce inequality within and among \ncountries by addressing issues such as social, economic, and \npolitical disparities through policies that empower marginalized groups.");
                        Console.ReadKey();
                        break;

                    case 10:
                        Console.WriteLine("\nSeek to make cities and human settlements \ninclusive, safe, resilient, and sustainable, addressing \nchallenges related to urbanization, housing, and infrastructure.");
                        Console.ReadKey();
                        break;

                    case 11:
                        Console.WriteLine("\nPromote sustainable consumption and production \npatterns by encouraging efficient resource use, reducing \nwaste, and minimizing the environmental impact of goods and services.");
                        Console.ReadKey();
                        break;

                    case 12:
                        Console.WriteLine("\nFocus on taking urgent action to combat climate change \nand its impacts by implementing measures to reduce greenhouse \ngas emissions, enhance climate resilience, and promote sustainable practices.");
                        Console.ReadKey();
                        break;

                    case 13:
                        Console.WriteLine("\nAim to conserve and sustainably use the oceans, \nseas, and marine resources to prevent marine pollution, \nprotect biodiversity, and ensure the health of marine ecosystems.");
                        Console.ReadKey();
                        break;

                    case 14:
                        Console.WriteLine("\nFocus on protecting, restoring, and \npromoting sustainable use of terrestrial ecosystems, \ncombating desertification, and halting biodiversity loss.");
                        Console.ReadKey();
                        break;

                    case 15:
                        Console.WriteLine("\nSeek to promote peaceful and inclusive \nsocieties, provide access to justice, and build \neffective, accountable, and inclusive institutions at all levels.");
                        Console.ReadKey();
                        break;

                    case 16:
                        Console.WriteLine("\nEmphasize the importance of global collaboration \nto achieve the other goals, encouraging partnerships \nbetween governments, the private sector, and civil society \nto mobilize resources and share knowledge for sustainable development.");
                        Console.ReadKey();
                        break;

                    case 17:
                        Console.WriteLine();
                        return 5;

                    default:
                        Console.WriteLine("An error occured...");
                        return 0;
                }
            }
        } 

        private int SDGQuizPlay()
        {
            SDGQuiz sdgquiz = new();

            while (sdgquiz.correctAnswersCount != 5)
            {
                sdgquiz.StartSDGQuiz();
                return 5;
            }
            return 0;
        } 

        private int SustFarmCourse()
        {
            string header = $"{TextArtManager.GetTextArt("FarmerBooks")} \nFriendly Farmer: Ohh old friend... \nAsk about anything you want! I know everything about sustainable farming!";
            string[] option =
            { "Crop rotation", "Cover crops", "Drip irrigation", "Companion planting", "Composting", "Fallow", "Rotational grazing", "No-till farming",
            "Rainwater harvesting", "Organic farming", "Exit" };

            while (true)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, option);

                switch (selectedIndex)
                {
                    case 0:
                        Console.WriteLine("Crop Rotation enhances soil health and crop yield \nby planting different crops in sequence, reducing the \nneed for synthetic fertilizers and promoting nutrient diversity.");
                        Console.ReadKey();
                        break;

                    case 1:
                        Console.WriteLine("Cover crops capture and store carbon \nin the soil, improving soil structure and fertility, \nproviding a sustainable solution for carbon sequestration in agriculture.");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Drip irrigation minimizes water usage in farming \nby delivering water directly to plant roots, ensuring \nefficient water use and reducing water wastage in agricultural practices.");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("Companion planting involves planting different crops \ntogether, maximizing space utilization, enhancing biodiversity, \nand reducing pests while promoting nutrient uptake among plants.");
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.WriteLine("Composting is the reuse of organic waste in farming, \ncreating nutrient - rich compost as a sustainable alternative to chemical fertilizers.");
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.WriteLine("Fallow, the practice of letting a field rest, \nrestores fertility, controls pests, and promotes \noverall soil health and productivity in a sustainable manner.");
                        Console.ReadKey();
                        break;

                    case 6:
                        Console.WriteLine("Rotational grazing optimizes livestock movement \nacross various pastures, promoting sustainable land \nuse, preventing overgrazing, and enhancing pasture health.");
                        Console.ReadKey();
                        break;

                    case 7:
                        Console.WriteLine("No-till farming, using minimal tillage, \nretains soil structure, reduces erosion, and enhances \nwater retention, contributing to sustainable and conservation-focused agriculture.");
                        Console.ReadKey();
                        break;

                    case 8:
                        Console.WriteLine("Rainwater harvesting collects and reuses rainwater, \naddressing water scarcity concerns in \nagriculture and providing an alternative water source for irrigation.");
                        Console.ReadKey();
                        break;
                    case 9:
                        Console.WriteLine("Organic farming, the production of crops \nwithout synthetic chemicals, promotes soil health, biodiversity, \nand sustainable farming practices as an eco - friendly alternative.");
                        Console.ReadKey();
                        break;

                    case 10:
                        Console.WriteLine();
                        return 5;

                    default:
                        Console.WriteLine("An error occured...");
                        return 0;

                }
            }
        } 

        private int SustFarmQuizPlay()
        {
            SFQuiz sfquiz = new();

            while (sfquiz.correctAnswersCount != 5)
            {
                sfquiz.StartSFQuiz();
                return 5;
            }
            return 0;
        }

        private int VillageDialogue()
        {
            VillagerTalk villagerTalk = new VillagerTalk();
            villagerTalk.villagerTalk();
            return 5;
        } 

        private int MichealDialogue()
        {
            Console.WriteLine(TextArtManager.GetTextArt("MrMicheal");
            Console.ReadKey();
            Console.Clear();
            Printer.PrintLine("Farmer: Good afternoon... I had a little chat with your friend. He told me to visit your office.", 25);
            Printer.PrintLine("Micheal: Yes! Finally! I have an excellent idea. I am a producent of solar panels and I would be glad to install my new product on your farm.", 25);
            Printer.PrintLine("In exchange I will need in exchange is you hosting a Open House on your farm where I would be able to explain the whole installation to our guest.", 25);
            Printer.PrintLine("What are your thoughts on it?", 25);
            Printer.PrintLine("\n1. Agree to the proposal.", 25);
            Printer.PrintLine("2. Agree to the proposal and offer farm goods to Micheal.", 25);

            string? userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Printer.PrintLine("Farmer: Sounds like a great idea! Let's work together for a sustainable future of our community!", 25);
                        Printer.PrintLine("Micheal: Okay, let's get to work then!", 25);
                        return 5;
                    case 2:
                        Printer.PrintLine("Farmer: That is a stunning idea! I will also provide you with some goods my farm creates. I hope you'll be happy.", 25);
                        Printer.PrintLine("Micheal: Thank you Farmer! Time to work together.\n", 25);
                        return 10;
                    default:
                        Printer.PrintLine("Invalid choice. Please enter 1 or 2.", 25);
                        return MichealDialogue();
                }
            }

            return 0;
        }  

        private int SolarPanels()
        {
            Console.WriteLine(TextArtManager.GetTextArt("solarPanels");
            Printer.PrintLine("Micheal arrives at your farm with a team of experts to install the solar panels.", 25);
            Printer.PrintLine("The installation process begins and you simply can't wait to see the final effect!", 25);

            for (int i = 0; i <= 100; i += 5)
            {
                Console.Write($"Installing: {i}% ");
                System.Threading.Thread.Sleep(200);
                Console.SetCursorPosition(0, Console.CursorTop);
            }

            Console.WriteLine("\nInstallation complete!\n");

            Printer.PrintLine("Now it is time to do the Farm Open House with Micheal.", 25);

            return 5;

        } 

        private int OpenHouseDay()
        {
            Console.WriteLine(TextArtManager.GetTextArt("openhouseday"));
            Printer.PrintLine("The day of the Open House has arrived, and your farm is buzzing with excitement!", 25);
            Printer.PrintLine("You have a chance to showcase your commitment to sustainability and inspire others.", 25);
            Console.ReadKey();
            Console.Clear();

            Printer.PrintLine("Micheal gives tours all around your new solar panel installation gathering new customers,", 25);
            Printer.PrintLine("the whole community is having fun and your job is playing a EcoSort mini-game with villagers.", 25);

            Console.ReadKey();
            Console.Clear();

            Printer.PrintLine("*EcoSort mini-game*\n", 25);
            Printer.PrintLine("Use numbers to choose an option for each waste item.", 25);

            Console.ReadKey();
            Console.Clear();

            Dictionary<string, string> wasteItems = new Dictionary<string, string>
    {
        {"Plastic Bottle", "1. Recycling"},
        {"Banana Peel", "2. Composting"},
        {"Newspaper", "1. Recycling"},
        {"Aluminum Can", "1. Recycling"},
        {"Eggshells", "2. Composting"},
        {"Glass Bottle", "1. Recycling"},
        {"Coffee Grounds", "2. Composting"},
        {"Cardboard Box", "1. Recycling"},
        {"Apple Core", "2. Composting"},
        {"Plastic Bag", "3. Landfill"},
        {"Paper Towel", "3. Landfill"},
        {"Orange Peel", "2. Composting"},
        {"Tin Foil", "1. Recycling"},
        {"Cereal Box", "1. Recycling"},
        {"Pizza Box", "2. Composting"},
        {"Styrofoam Cup", "3. Landfill"},
        {"Milk Carton", "1. Recycling"},
        {"Soda Can", "1. Recycling"},
        {"Tea Bags", "2. Composting"},
        {"Plastic Utensils", "3. Landfill"}
    };
            int score = 0;
            int correctToFinish = 4;

            var random = new Random();
            wasteItems = wasteItems.OrderBy(x => random.Next()).ToDictionary(item => item.Key, item => item.Value);

            foreach (var wasteItem in wasteItems)
            {
                Console.Clear();
                Console.WriteLine($"\n{wasteItem.Key}?\n", 25);
                Console.WriteLine("Choose a bin: ");
                Console.WriteLine("1. Recycling");
                Console.WriteLine("2. Composting");
                Console.WriteLine("3. Landfill");

                string? userInput = Console.ReadLine();

                if (userInput == wasteItem.Value.Substring(0, 1))
                {
                    Printer.PrintLine("Correct! This item goes in the right place!\n", 25);
                    score++;
                }
                else
                {
                    Printer.PrintLine("Oops! Wrong!", 25);
                }

                System.Threading.Thread.Sleep(1000);

                if (score >= correctToFinish)
                {
                    break;
                }
            }

            Printer.PrintLine($"Challenge complete!", 25);

            return 15;
        } 

        private int LakeInformation()
        {
            Console.WriteLine(TextArtManager.GetTextArt("lake"));
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Seems like they are some stray dogs running around the lake area...");
            Console.WriteLine("Help your community and find them before it's too late!");
            Console.ReadKey();
            Console.Clear();

            string header = "You walk around the lake and ask different people about the lost animals...";
            string[] option =
                { "John: Lake's Timberman", "Alex: Local Fisherman", "Elizabeth: Hotel owner", "Exit"};

            while (true)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, option);

                switch (selectedIndex)
                {
                    case 0:
                        Printer.PrintLine("You: Hey John, how's it going?", 25);
                        Printer.PrintLine("John: Oh, hey there! Just trying to get these logs ready for the upcoming construction project. What brings you to the lake today?", 25);
                        Printer.PrintLine("You: I'm actually looking for information about some lost animals around here. Have you seen anything unusual lately?", 25);
                        Printer.PrintLine("John: Animals, you say? Well, I did notice a couple of stray dogs near the timber yard a few days ago. Seemed like they were lost or something.", 25);
                        Printer.PrintLine("You: Thanks, John. I'll check that out. Take care with those logs!", 25);
                        Console.ReadKey();
                        break;

                    case 1:
                        Printer.PrintLine("You: Alex, how's the fishing today?", 25);
                        Printer.PrintLine("Alex: Could be better, my friend. The fish seem to be playing hard to get. What brings you to the shore?", 25);
                        Printer.PrintLine("You: I'm on a mission to find some lost animals. Have you noticed anything unusual while out on the water?", 25);
                        Printer.PrintLine("Alex: Lost animals, huh? Well, there was a strange noise the other night, kind of like a distressed animal. I heard it while walking near the lakeside.", 25);
                        Printer.PrintLine("You: Interesting. I'll keep an eye out. If you hear or see anything, let me know.", 25);
                        Console.ReadKey();
                        break;
                    case 2:
                        Printer.PrintLine("You: Hi Elizabeth, everything looks lovely as usual.", 25);
                        Printer.PrintLine("Elizabeth: Thank you! What can I do for you today?", 25);
                        Printer.PrintLine("You: I'm trying to gather information about lost animals in the area. Have you or your guests noticed anything?", 25);
                        Printer.PrintLine("Elizabeth: Lost animals? Now that you mention it, a couple of our guests mentioned hearing strange sounds at night. Maybe some distressed animals? It could be around the Crystal Lake Resort or the Park...", 25);
                        Printer.PrintLine("You: That's helpful. If they notice anything specific, let me know. I'll see if I can find and help those animals.", 25);
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("Looks like I will need some meat...\n");
                        Console.ReadKey();
                        return 5;

                    default:
                        Console.WriteLine("An error occured...");
                        return 0;
                }
            }
        } 

        private int MeatCollect()
        {

            Printer.PrintLine("Getting meat from the fridge...", 25);
            Console.ReadKey();
            Console.Clear();
            return 5;
        } 

        delegate bool SearchAnimalsDelegate();

        private int LookForAnimals()

        {
            Console.WriteLine(TextArtManager.GetTextArt("dogs"));
            string[] locations = { "Timber Yard", "Lakeside", "Crystal Lake Resort", "Park" };
            int score = 0;
            int totalDogs = locations.Length * 3;

            Console.WriteLine("Time to find lost animals in the lake area.");
            Console.WriteLine("Explore each location carefully and try to find all the stray dogs.");
            Console.WriteLine("Good luck!\n");

            Console.ReadKey();
            Console.Clear();

            while (score < totalDogs)
            {
                foreach (var location in locations)
                {
                    Console.WriteLine($"{location}: You search near {location} for any signs of lost dogs.");
                    SearchAnimalsDelegate searchGame = () =>
                    {
                        Console.WriteLine($"You have to decide where to search at {location}. Choose a number between 1 and 4:");
                        Console.WriteLine("1. Check the bushes");
                        Console.WriteLine("2. Look behind the trees");
                        Console.WriteLine("3. Investigate the nearby fields");
                        Console.WriteLine("4. Behind you");

                        int playerChoice;
                        while (true)
                        {
                            Console.Write("Enter your choice: ");
                            if (int.TryParse(Console.ReadLine(), out playerChoice) && playerChoice >= 1 && playerChoice <= 5)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                            }
                        }

                        Random random = new Random();
                        bool foundDogs = random.Next(1, 5) == 1; //25% chance

                        Console.Clear();

                        if (foundDogs)
                        {
                            Console.WriteLine($"You found a stray dog at {location}! Great job!");
                            Console.WriteLine("A little snack and he likes you!");
                        }
                        else
                        {
                            Console.WriteLine($"No luck this time at {location}... Keep exploring!");
                        }

                        Console.WriteLine();

                        return foundDogs;
                    };

                    bool foundDogs = searchGame();

                    if (foundDogs)
                    {
                        score += 5;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            Console.WriteLine($"\nCongratulations! You've found all the stray dogs and completed the mission with a score of {score}!");
            Console.WriteLine("Now it is time to build some animal shelters for the new citizens!");
            return score;
        } 
       
        private int AnimalShelters()
        {
            Console.WriteLine(TextArtManager.GetTextArt(["dogShelters"));
            Printer.PrintLine("Congratulations again on finding the stray dogs.", 25);
            Printer.PrintLine("Now you need to build homes for the lost animals.", 25);
            Console.ReadKey();

            string header = "Build some shelters for your guests!";
            string[] options =
                { "1: Build a shelter for Hugo", "2: Build a shelter for Sky", "3: Build a shelter for Goldie", "Complete the quest." };

            bool[] sheltersBuilt = { false, false, false };

            while (true)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, options);

                if (selectedIndex >= 0 && selectedIndex < 3 && !sheltersBuilt[selectedIndex])
                {
                    Console.WriteLine($"Shelter {selectedIndex + 1} built! {(selectedIndex == 0 ? "Hugo" : (selectedIndex == 1 ? "Sky" : "Goldie"))} is happy!");
                    sheltersBuilt[selectedIndex] = true;
                    Console.ReadKey();
                }
                else if (selectedIndex == 3 && sheltersBuilt.All(built => built))
                {
                    Console.ReadKey();
                    return 10;
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            }
        } 
       
        private int GetOrganicMaterials()
        {
            Console.WriteLine(TextArtManager.GetTextArt("organicmaterials"));
            Console.ReadKey();
            Console.Clear();
            Printer.PrintLine("Farmer: Hello Sara! I need some organic materials to mix into a fertilizer... Here is my list: \n- Compost \n- Manure \n- Alfalfa Meal \n- Fish Emulsion \n- Kelp Meal \n- Green Manure \n- Worm Castings", 25);
            Printer.PrintLine("Sara: Sure! I got all of it... Here is it!", 25);
            Console.ReadKey();
            return 5;
        } 
       
        private int MixerFertilizer()
        {
            int result = RunFertilizerMixerGame();

            if (result == 1 || result == 2)
            {
                Console.WriteLine($"You scored {result * 10} points!");
                return result * 10;
            }
            else
            {
                Console.WriteLine("Exiting the game.");
                return 0;
            }
        } 
       
        private int UseFertilizer()
        {
            {
                Console.WriteLine(TextArtManager.GetTextArt("usefertilizer"));
                string header = "Use the fertilizer on all of your plants!";
                string[] option =
                    { "Carrot", "Tomato", "Corn", "Pumpkin", "Sunflower", "Finished"};

                while (true)
                {
                    int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, option);

                    switch (selectedIndex)
                    {
                        case 0:
                            Printer.PrintLine($"Fertilizer used on {option[0]}.", 25);
                            Console.ReadKey();
                            break;

                        case 1:
                            Printer.PrintLine($"Fertilizer used on {option[1]}.", 25);
                            Console.ReadKey();
                            break;
                        case 2:
                            Printer.PrintLine($"Fertilizer used on {option[2]}.", 25);
                            Console.ReadKey();
                            break;
                        case 3:
                            Printer.PrintLine($"Fertilizer used on {option[3]}.", 25);
                            Console.ReadKey();
                            break;
                        case 4:
                            Printer.PrintLine($"Fertilizer used on {option[4]}.", 25);
                            Console.ReadKey();
                            break;
                        case 5:
                            Printer.PrintLine("All of the plants got their fertilizer!", 25);
                            Printer.PrintLine("Good job!", 25);
                            return 5;
                        default:
                            Console.WriteLine("An error occured...");
                            return 0;
                    }
                }
            }
        }

        //Method for the Map

        public void showMap(Room currentRoom)
        {
            string lake = "     ";
            string house = "     ";
            string village = "     ";
            string farm = "     ";
            string market = "     ";
            string watermill = "     ";

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

        //Different Classes/Methods

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
                Console.WriteLine(TextArtManager.GetTextArt("sdgquizTextArt"));
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

        public class SFQuiz
        {
            public interface IQuizQuestionSF
            {
                string Question { get; }
                string CorrectAnswer { get; }
                bool CheckAnswer(string userAnswer);
            }

            public class SFQuizQuestion : IQuizQuestionSF
            {
                public string Question { get; }
                public string CorrectAnswer { get; }

                public SFQuizQuestion(string question, string correctAnswer)
                {
                    Question = question;
                    CorrectAnswer = correctAnswer;
                }

                public bool CheckAnswer(string userAnswer)
                {
                    return userAnswer.Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase);
                }
            }


            private List<IQuizQuestionSF> quizQuestions;
            public int correctAnswersCount;

            public SFQuiz()
            {
                quizQuestions = new List<IQuizQuestionSF>
        {
    new SFQuizQuestion("1. What reduces the need for synthetic fertilizers?\n \n- Genetic modification \n- Crop Rotation \n- Chemical spraying", "Crop rotation"),

    new SFQuizQuestion("2. What captures and stores carbon in the soil?\n\n- Deep plowing\n- Cover crops\n- Concrete foundations", "Cover crops"),

    new SFQuizQuestion("5. What reduces water usage in farming?\n\n- Flood irrigation\n- Drip irrigation\n- Open hose watering", "Drip irrigation"),

    new SFQuizQuestion("6. What involves planting different crops together?\n\n- Single cropping\n- Intercropping\n- Random planting", "Companion planting"),

    new SFQuizQuestion("7. What is the reuse of organic waste in farming?\n\n- Trash disposal\n- Composting\n- Incineration", "Composting"),

    new SFQuizQuestion("9. What is the practice of letting a field rest to restore fertility?\n\n- Intensive farming\n- Fallow\n- Desertification", "Fallow"),

    new SFQuizQuestion("10. What involves rotating livestock through different pastures?\n\n- Static grazing\n- Rotational grazing\n- Free-range grazing", "Rotational grazing"),

    new SFQuizQuestion("13. What farming method uses minimal tillage?\n\n- Intensive tillage\n- No-till farming\n- Deep plowing", "No-till farming"),

    new SFQuizQuestion("15. What is the process of collecting and reusing rainwater?\n\n- Wastewater treatment\n- Rainwater harvesting\n- Stormwater runoff", "Rainwater harvesting"),

    new SFQuizQuestion("16. What is the production of crops without the use of synthetic chemicals?\n\n- Traditional farming\n- Organic farming\n- Chemical farming", "Organic farming"),

        };
                correctAnswersCount = 0;
            }



            public void PrintQuizTxt()
            {
                Console.WriteLine(TextArtManager.GetTextArt("sfquizTextArt"));
                Console.WriteLine("It's time to test your knowledge about Sustainable Farming!");
                Console.WriteLine();
            }
            public void StartSFQuiz()
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

        public class VillagerTalk
        {
            private bool cropRotationCompleted = false;
            private bool chemicalReductionCompleted = false;
            private bool waterConservationCompleted = false;

            public void villagerTalk()

            {
                do
                {
                    Console.WriteLine(TextArtManager.GetTextArt("Houses");
                    Console.WriteLine();
                    Console.WriteLine("Villager: Hi there! I heard you are a new farming master around here!");
                    Console.WriteLine("People say you are specialized in sustainable farming. Could you tell me something about it?");
                    Console.WriteLine();

                    if (!cropRotationCompleted)
                        Console.WriteLine("1. Explain crop rotation");

                    if (!chemicalReductionCompleted)
                        Console.WriteLine("2. Discuss reducing of chemicals");

                    if (!waterConservationCompleted)
                        Console.WriteLine("3. Talk about water conservation");

                    Console.WriteLine();

                    string? input = Console.ReadLine();

                    if (int.TryParse(input, out int choice))
                    {
                        if (choice >= 1 && choice <= 3)
                        {
                            if (choice == 1 && !cropRotationCompleted)
                            {
                                ExplainCropRotation();
                                cropRotationCompleted = true;
                            }
                            else if (choice == 2 && !chemicalReductionCompleted)
                            {
                                ExplainChemicalReduction();
                                chemicalReductionCompleted = true;
                            }
                            else if (choice == 3 && !waterConservationCompleted)
                            {
                                ExplainWaterConservation();
                                waterConservationCompleted = true;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("You have already completed that task. Choose a different option.");
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input. Choose a number between 1 and 3.");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press Enter to return to the main dialogue...");
                    Console.ReadLine();
                    Console.Clear();

                } while (!cropRotationCompleted || !chemicalReductionCompleted || !waterConservationCompleted);

                Console.WriteLine();
                Printer.PrintLine("Villager: Thank you for letting me know all of these things. I can't wait to share this knowledge.", 25);
                Printer.PrintLine("I might know somebody that could help you with sustainable farming.", 25);
                Printer.PrintLine("Talk to Micheal, you can find his office right next to the church.");
                Printer.PrintLine("Farmer: Thank you very much, see you around!", 25);
                Console.WriteLine();
            }


            private void ExplainCropRotation()
            {

                Console.WriteLine();
                Printer.PrintLine("Farmer: Crop rotation is like a dance for my fields. Instead of planting the same crop in", 25);
                Printer.PrintLine("the same spot every year, I mix it up!", 25);
                Printer.PrintLine("So, one year, it's corn taking center stage, then the next sunflower steal the show.", 25);
                Printer.PrintLine("It helps to keep my soil nutrient-rich and prevents pests", 25);
                Printer.PrintLine("from getting too comfortable. Imagine it as a natural way of giving the land a breather!", 25);
                Console.WriteLine();
                Printer.PrintLine("Villager: Oh, that is fascinating! So it is like a land dance choreography?", 25);
                Console.WriteLine();
                Printer.PrintLine("Farmer: Exactly! Each crop has it's own moves and by switching them around, I ensure my field stays healthy!", 25);
            }

            private void ExplainChemicalReduction()
            {
                Console.WriteLine();
                Printer.PrintLine("Farmer: Sustainability is all about the green harmony on the farm.", 25);
                Printer.PrintLine("Reducing chemical usage is like creating a peaceful garden where", 25);
                Printer.PrintLine("plants and animals coexist. I minimize the impact on the environment", 25);
                Printer.PrintLine("by opting for greener alternatives, ensuring my fields are in harmony with nature.", 25);
                Console.WriteLine();
                Printer.PrintLine("Villager: Green harmony, that sounds stunning! How do you achieve it?", 25);
                Console.WriteLine();
                Printer.PrintLine("Farmer: I focus on soil health, embrace organic farming practices", 25);
                Printer.PrintLine("and utilize natural fertilizers to keep a healthy land.", 25);
            }

            private void ExplainWaterConservation()
            {
                Console.WriteLine();
                Printer.PrintLine("Farmer: Ever solved a puzzle where every piece matters?", 25);
                Printer.PrintLine("Water conservation is like putting together an aqua puzzle for the farm.", 25);
                Printer.PrintLine("I need to use each drop efficiently, fitting them into the puzzle", 25);
                Printer.PrintLine("to create a picture of water-smart landscape.", 25);
                Console.WriteLine();
                Printer.PrintLine("Villager: An aqua puzzle! How interesting! How do you do it?", 25);
                Console.WriteLine();
                Printer.PrintLine("Farmer: I'm glad you've asked! I recycle water and adopt technologies that optimize water use.", 25);
                Printer.PrintLine("That is the secret to find the perfect fit for every drop to sustain my crops.", 25);
            }
        } 

        public int RunFertilizerMixerGame()
        {
            Console.Clear();
            Console.WriteLine(TextArtManager.GetTextArt("fertilizerMixer"));
            Console.WriteLine("Mix different organic materials to create fertilizer.");
            Console.WriteLine();

            Dictionary<string, string> organicMaterials = new Dictionary<string, string>
        {
            { "1", "Compost" },
            { "2", "Manure" },
            { "3", "Alfalfa Meal" },
            { "4", "Fish Emulsion" },
            { "5", "Kelp Meal" },
            { "6", "Green Manure" },
            { "7", "Worm Castings" }
        };

            List<string> bestCombination = new List<string> { "2", "4", "7" };
            List<string> midCombination1 = new List<string> { "1", "3", "5" };
            List<string> midCombination2 = new List<string> { "1", "2", "6" };
            List<string> midCombination3 = new List<string> { "2", "5", "1" };

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Choose three organic materials to mix:");
                foreach (var material in organicMaterials)
                {
                    Console.WriteLine($"{material.Key}. {material.Value}");
                }
                Console.WriteLine("8. Back to the menu");

                List<string> playerMix = new List<string>();
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"Enter choice {i + 1}: ");
                    string? choice = Console.ReadLine();

                    if (choice == "8")
                    {
                        return 0;
                    }

                    if (organicMaterials.ContainsKey(choice))
                    {
                        playerMix.Add(choice);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Ooops! Your mix didn't work well... Try again!");
                        Console.WriteLine();
                        Console.WriteLine("Organic Fertilizer Mixing Game!");
                        Console.WriteLine("Mix different organic materials to create fertilizer.");
                        Console.WriteLine("Choose three organic materials to mix:");
                        Console.WriteLine("1. Compost");
                        Console.WriteLine("2. Manure");
                        Console.WriteLine("3. Alfalfa Meal");
                        Console.WriteLine("4. Fish Emulsion");
                        Console.WriteLine("5. Kelp Meal");
                        Console.WriteLine("6. Green Manure");
                        Console.WriteLine("7. Worm Castings");
                        Console.WriteLine("8. Back to the menu");
                        i = -1;
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                        continue;
                    }
                }

                if (IsCorrectCombination(playerMix, bestCombination))
                {
                    Console.Clear();
                    Console.WriteLine("Congratulations! You created the best fertilizer. Your crops will grow healthier!");
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return 2;
                }
                else if (IsCorrectCombination(playerMix, midCombination1) || IsCorrectCombination(playerMix, midCombination2) || IsCorrectCombination(playerMix, midCombination3))
                {
                    Console.Clear();
                    Console.WriteLine("Good job! You created a decent fertilizer. Crops will grow, but not as well as the best one.");
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return 1;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ooops! Your mix didn't work well... Try again!");
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
            bool IsCorrectCombination(List<string> playerMix, List<string> correctCombination)
            {
                playerMix.Sort();
                correctCombination.Sort();

                return playerMix.SequenceEqual(correctCombination);
            }
        }
    }
}