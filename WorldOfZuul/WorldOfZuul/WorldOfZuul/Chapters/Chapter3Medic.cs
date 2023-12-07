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
        private Room? PublicHospital;
        private Room? VillageCenter;
        private Room? GreenForest;
        private Room? HolyChurch;
        private Room? TownMarket;
        private Room? DeepCave;
        private Room? MedicalLab;

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
            Item BookOfHerbGathering = new Item("Book Of Herb Gathering", "With this book, you will now which herbs to gather...");

            Item Herbs = new Item("Fragrant Herbs", "This herbs will come in handy to brew potions...");

            Item MedicalKnowledge = new Item("Medical Knowledge", "You have proven that you are real medic");

            Item BasicInformations = new Item("Informations", "Now you know what is going in the village");

            Item CardAccessingToLab = new Item("Access to Medical Lab", "This is what you need, new medicines are just waiting to discover");

            Item Medicines = new Item("Freshly Discovered Medicines", "They will come in handy to help the miners in cave");


            // Initialize rooms
            VillageCenter = new Room("Village Center", "\nYou stand in the heart of a charming village. Cobblestone paths lead in different directions. To the east is a bustling market, to the south is a quaint church, and to the west is a peaceful forest.");

            PublicHospital = new Room("Hospital", "\nYou find yourself in a sterile environment filled with the scent of antiseptic. White walls surround you, and the sound of distant footsteps echoes through the corridors.");

            GreenForest = new Room("Forest", "\nYou step into a dense and enchanting forest. Tall trees tower above, creating a canopy of leaves that filters sunlight. Birds chirp in the distance, and the air is filled with the scent of pine.");

            HolyChurch = new Room("Church", "\nAs you enter, the atmosphere becomes serene. Stained glass windows filter colorful light into the church. Wooden pews line the aisles, and silence is broken only by the occasional creaking of old wooden floorboards.");

            TownMarket = new Room("Market", "\nThe hustle and bustle of a vibrant market surround you. Stalls are filled with colorful fruits, vegetables, and various goods. Merchants call out their prices, creating a lively and dynamic atmosphere.");
            
            DeepCave = new Room("Deep Cave", "\nYou find yourself standing at the entrance of a mysterious and foreboding cave. The air is cool, and the only sound is the distant dripping of water echoing through the cavern. The darkness within is absolute, and you can barely make out the rough walls covered in ancient rock formations.");

            MedicalLab = new Room("Medical Laboratory", "\nYou walk into a makeshift medical lab. The air smells a bit old, and the lights are not very bright. The lab tables look worn, and there's not much fancy equipment. Some old tools and containers are scattered around. It's not high-tech, but you sense a kind of determined effort in this humble space");

            // Set exits

            // Set exits -> (north, east, south, west) & Set exit -> ("north/east/south/west", "place")

            VillageCenter.SetExits(HolyChurch, PublicHospital, TownMarket, GreenForest);

            DeepCave.SetExits(null, HolyChurch, GreenForest, null);

            HolyChurch.SetExits(null, MedicalLab, VillageCenter, DeepCave);

            PublicHospital.SetExits(MedicalLab, null, null, VillageCenter);

            TownMarket.SetExit("north", VillageCenter);

            GreenForest.SetExits(DeepCave, VillageCenter, null, null);

            MedicalLab.SetExits(null, null, PublicHospital, HolyChurch);

            /***************** QUEST SECTION ****************/

            //Create quest (short and long description)

            Quest TalkingWithLocals = new Quest("Let's talk about the Village matters", "You came to this, but really you don't know anything about it yes? Go and talk with the locals...");
            
            Quest ExploreTheForest = new Quest("Explore Forest", "Defeat the sickness, You've received information from the mayor of the village " +
                                                                      "that the source of the disease was in the forest. You should nip this in the bud...");

            Quest ChurchQuest = new Quest("Do Medical Quiz", "You need to finish this quiz to prepare for healing citizens!");

            Quest DeepCaveRescue = new Quest("Cave Rescue Mission", "You received information about the collapse of the ceiling in the cave. You have to organize a rescue mission, the miners won't last long, hurry up!");


            //Initialize tasks

            Task GatheringBasicInformations = new Task("Informations", "You're here, but it seems you're not quite in the loop. Best go chat with the folks around here", TalkingWithLocals, VillageCenter, VillageCenterInterview, null, BasicInformations);

            Task MedicalQuiz = new Task("Quiz", "Go on and finish this quiz!", ChurchQuest, HolyChurch, QuizInChurch, BasicInformations, null);

            Task DefeatingSickness = new Task("Sickness", "You need to free villagers from the sickness. Source of the virus comes from the forest", ExploreTheForest, GreenForest, BattleWithSickness, BasicInformations, BookOfHerbGathering);

            Task GatherHerbs = new Task("Herbs", "In forest you can find useful herbs, they will come in handy to discover new medicines!", ExploreTheForest, GreenForest, HerbsGathering, BookOfHerbGathering, Herbs);

            Task InformMayor = new Task("Informing", "You defeated the virus that made the lives of the locals miserable, you have to go and tell the Mayor Campbell about it", TalkingWithLocals, VillageCenter, VillageCenterInformMayor, Herbs, CardAccessingToLab);

            Task DiscoveringNewMedicines = new Task("Medicines", "Inventing new medicines is necessary to help injured miners. Go on fast!", DeepCaveRescue, MedicalLab, DiscoverMedicines, null, null);

            Task RescueMiners = new Task("Mission", "You are in front of cave entrance, you need to find miners and help them!", DeepCaveRescue, DeepCave, MinerRescue, Medicines, null);

            //Add quests to the chapters quests lists

            Quests.Add(ExploreTheForest);
            
            Quests.Add(DeepCaveRescue);

            Quests.Add(TalkingWithLocals);

            Quests.Add(ChurchQuest);

            // Add task to the quest list

            ExploreTheForest.AddTask(DefeatingSickness);
            ExploreTheForest.AddTask(GatherHerbs);

            ChurchQuest.AddTask(MedicalQuiz);

            TalkingWithLocals.AddTask(GatheringBasicInformations);
            TalkingWithLocals.AddTask(InformMayor);

            DeepCaveRescue.AddTask(DiscoveringNewMedicines);
            DeepCaveRescue.AddTask(RescueMiners);

            //Add task to the room

            GreenForest.AddTask(DefeatingSickness);
            GreenForest.AddTask(GatherHerbs);

            VillageCenter.AddTask(GatheringBasicInformations);
            VillageCenter.AddTask(InformMayor);

            HolyChurch.AddTask(MedicalQuiz);

            MedicalLab.AddTask(DiscoveringNewMedicines);
            DeepCave.AddTask(RescueMiners);


            // Adding thigs to rooms
            Rooms.Add(HolyChurch);
            Rooms.Add(PublicHospital);
            Rooms.Add(TownMarket);
            Rooms.Add(GreenForest);
            Rooms.Add(VillageCenter);
            Rooms.Add(DeepCave);
            Rooms.Add(MedicalLab);

        }

        public string PlayerScore()
        {
            return $@"Your Medic Score is : {medicScore}";
        }

        /****************** MINER RESCUE TASK **********************/

        private int MinerRescue()
        {
            Console.WriteLine("Welcome to the Miner Rescue Game!");
            Console.WriteLine("You are a rescuer tasked with finding and healing injured miners in a cave.");
            Console.WriteLine("You have three possible locations to search.");

            int totalMiners = 3;
            int minersHealed = 0;

            while (minersHealed < totalMiners)
            {
                Console.WriteLine("\nChoose a location to search (1, 2, or 3):");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int location) && location >= 1 && location <= 3)
                {
                    // Simulate search results
                    bool minerFound = (new Random()).Next(2) == 1;

                    if (minerFound)
                    {
                        Console.WriteLine("You found an injured miner in location " + location + "!");
                        Console.WriteLine("You provide first aid and heal the miner.");
                        minersHealed++;
                    }
                    else
                    {
                        Console.WriteLine("No injured miners found in location " + location + ".");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                }
            }

            Console.WriteLine("\nCongratulations! You have successfully healed all injured miners.");
            Console.WriteLine("Thanks for playing the Miner Rescue Game!");
            return 15;
        }

        /****************** TALKING WITH LOCALS TASK **********************/
        private int VillageCenterInterview()
        {
            bool gatheredAllInfo = false;
            HashSet<string> infoCollected = new HashSet<string>();

            string header = "\nYou came to the village, go gather some informations? Who you want to talk with?\n";
            string[] option = {
                                "1. Mayor of the Village"
                                ,"2. Old Man laying on the pavement"
                                ,"3. Someone who is looking like Farmer"
                                };

            while (!gatheredAllInfo)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, option);

                switch (selectedIndex)
                {
                    case 0:
                        if (!infoCollected.Contains("Mayor"))
                        {
                            PrintText("\nMayor: \nWelcome, kind medic! Our village is truly grateful for your selfless service. The health of our citizens is in your capable hands",15);
                            PrintText("\nI'm Mayor Campbell, if you will need anything go talk to me! " +
                                        "\nOur biggest problem so far is the virus, which is spreading at a dizzying pace. " +
                                        "\nIt's source is in the middle of the forest, please destroy it, we are powerless...", 15);
                            infoCollected.Add("Mayor");

                        }
                        else
                        {
                            Console.WriteLine("\nYou've already talked with Mayor... Go talk with others!\n");
                        }
                        break;
                    case 1:
                        if (!infoCollected.Contains("OldMan"))
                        {
                            PrintText("\nOld Man: \nHey there, Medic... It's tough around here. No money, empty stomachs, and no one to patch us up when we're sick. " +
                                                "\nFeels like nobody notices us anymore. Glad you're here, though.", 15);

                            PrintText($"\nMedic: \nNo worries in couple weeks everything should be fine!", 15);

                            PrintText("\nOld Man: \nThank you very, very much. Go visit church I think you will gain some useful informations there...", 15);


                            infoCollected.Add("OldMan");
                        }
                        else
                        {
                            Console.WriteLine("\nYou've already talked with Old Man... Go talk with others!\n");
                        }
                        break;
                    case 2:
                        if (!infoCollected.Contains("Farmer"))
                        {
                            PrintText("\nFarmer: \nHey! It was tough with empty fields and growling stomachs, but this Hero Farmer came and fixed things up. " +
                                "\nNow, thanks to him, we've got more food on the table." +
                                "\nI hope that now you, like the previous hero, will save us from a difficult situation with health care.", 15);

                            PrintText($"\nMedic: \nI won't let you down, I'm here to help!", 15);

                            PrintText("\nFarmer: \nOn behalf of the entire village, we are grateful...", 15);

                            infoCollected.Add("Farmer");
                        }
                        else
                        {
                            Console.WriteLine("\nYou've already talked with Farmer... Go talk with others!\n");
                        }
                        break;
                    default:
                        Console.WriteLine("\nYou can't to that! This choice is not valid.\n");
                        break;
                }
                
                Console.ReadKey();

                if (infoCollected.Count >= 3)
                {
                    gatheredAllInfo = true;
                }

            }

            Console.Clear();

            Console.WriteLine("\nYou talked to everyone, with your current knowledge you can go and perform the tasks entrusted to you!\n");
            
            return 5;

        }

        private int VillageCenterInformMayor()
        {
            bool gatheredAllInfo = false;
            HashSet<string> infoCollected = new HashSet<string>();

            string header = "\nYou came to the village, go gather some informations? Who you want to talk with?\n";
            string[] option = {
                               "1. Mayor Campbell"
                              };

            while (!gatheredAllInfo)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, option);

                switch (selectedIndex)
                {
                    case 0:
                        if (!infoCollected.Contains("Mayor"))
                        {
                            PrintText("\nMedic: \nMayor Campbell, good news! I've traced and defeated the virus source in the forest. The threat is contained, and the village can breathe easy again", 15);

                            PrintText("\nMayor Campbell: \nFantastic! You've brought a wave of relief to our village. But now there is another problem...", 15);

                            PrintText("\nMedic: \nOh my god! What happened?", 15);

                            PrintText("\nMayor Campbell: \n There was an accident in the mine, many miners were injured. They need your medical help...", 15);

                            PrintText("\nMedic: \n I'm going to save them, I just need to invent new medicines with the help of new types of herbs that I found in the forest. " +
                                                   "I need card to access the medical lab", 15);

                            PrintText("\nMayor Campbell: \n Here you go! Hurry up, the miners won't wait...", 15);

                            infoCollected.Add("Mayor");

                        }
                        else
                        {
                            Console.WriteLine("\nYou've already talked with Mayor... Go talk with others!\n");
                        }
                        break;
                    
                    default:
                        Console.WriteLine("\nYou can't to that! This choice is not valid.\n");
                        break;
                }

                Console.ReadKey();

                if (infoCollected.Count >= 1)
                {
                    gatheredAllInfo = true;
                }

            }

            Console.Clear();

            Console.WriteLine("\nThe Mayor is very happy, you just arrived in the village and it's condition is already improving." +
                              "\nNow go and invent new potions, you need to help the miners!\n");

            return 5;

        }

        /****************** DISCOVER MEDICINES TASK *********************/

        private int DiscoverMedicines()
        {
            Console.WriteLine("Welcome to the Medicine Discovery Game!");
            Console.WriteLine("Discover two medicines to win the game.");

            int discoveredMedicines = 0;

            // Available ingredients
            List<string> ingredients = new List<string>
            {
                "Lotus",
                "Emberfern",
                "Dreamroot",
                "Featherfern",
                "Emberleaf"
            };

            Random random = new Random();

            while (discoveredMedicines < 2)
            {
                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1. Research");
                Console.WriteLine("2. Experiment");
                Console.WriteLine("3. Quit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("You conducted research.");
                        Console.WriteLine("Progress in understanding the compounds...");
                        Console.WriteLine("No medicine discovered yet.");
                        break;

                    case "2":
                        Console.WriteLine("You conducted an experiment.");
                        List<string> experimentIngredients = GetRandomIngredients(ingredients, random);
                        Console.WriteLine($"Using ingredients: {string.Join(", ", experimentIngredients)}");

                        if (IsExperimentSuccessful(experimentIngredients))
                        {
                            Console.WriteLine("Success! You discovered a new medicine!");
                            discoveredMedicines++;
                        }
                        else
                        {
                            Console.WriteLine("Experiment failed. Try different ingredients.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Quitting the game.");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }

            Console.WriteLine("\nCongratulations! You have discovered two new medicines!");
            Console.WriteLine("Game Over. Returning a score of 15.");

            return 15;
        }
        static List<string> GetRandomIngredients(List<string> ingredients, Random random)
        {
            List<string> experimentIngredients = new List<string>();

            for (int i = 0; i < 2; i++)
            {
                int index = random.Next(ingredients.Count);
                experimentIngredients.Add(ingredients[index]);
            }

            return experimentIngredients;
        }
        static bool IsExperimentSuccessful(List<string> ingredients)
        {
            Random random = new Random();
            return random.Next(2) == 0;
        }

        /****************** BATTLE WITH SICKNESS TASK *********************/
        private int BattleWithSickness()
        {
            // Medic stats
            int medicHealth = 100;
            int medicAttackPower = 15;
            int medicHealingPower = 20;

            // Sickness stats
            int sicknessHealth = 1;
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

                        Thread.Sleep(3000);
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid action.");

                        Thread.Sleep(3000);
                        Console.Clear();
                        continue;
                }

                Console.WriteLine();
                System.Threading.Thread.Sleep(1000);
            }

            if (medicHealth <= 0)
            {
                Console.WriteLine("The sickness has overwhelmed you. You fought valiantly, but it wasn't enough.");
                return 0;
            }
            else
            {
                Console.WriteLine("Congratulations! You have successfully defeated the sickness. The village is now a healthier place!");
                return 15;
            }
        }

        /****************** QUIZ IN CHURCH TASK *********************/
        private int QuizInChurch()
        {
            string[] medicalQuestions = {
            "What is the function of the respiratory system?",
            "Which vitamin is essential for bone health?",
            "What is the normal resting heart rate for adults?",
            "What is the main function of the liver?",
            "Which organ is responsible for filtering blood and removing waste?",
            "What is the medical term for the voice box?",
        };

            string[][] medicalAnswers = {
            new string[] { "a) Oxygenate blood", "b) Digest food", "c) Pump blood", "d) Regulate body temperature" },
            new string[] { "a) Vitamin C", "b) Vitamin D", "c) Vitamin A", "d) Vitamin K" },
            new string[] { "a) 60-80 beats per minute", "b) 100-120 beats per minute", "c) 40-60 beats per minute", "d) 120-140 beats per minute" },
            new string[] { "a) Digestion", "b) Blood circulation", "c) Detoxification", "d) Respiratory control" },
            new string[] { "a) Kidneys", "b) Lungs", "c) Heart", "d) Stomach" },
            new string[] { "a) Trachea", "b) Larynx", "c) Esophagus", "d) Pharynx" },
        };

        int score = 0;

        for (int i = 0; i < medicalQuestions.Length; i++)
        {
            string header = medicalQuestions[i];
            string[] options = medicalAnswers[i];

            int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, options);

            // Check the correctness of the answer
            if (CheckAnswer(selectedIndex))
            {
                Console.WriteLine("Correct answer!");
                score++;
            }
            else
            {
                Console.WriteLine("\nWrong answer. The correct answer is: " + GetCorrectAnswer(options));
            }

            Console.WriteLine();
        }

            Console.WriteLine("\nYour score is: " + score + "/" + medicalQuestions.Length);

        if ((double)score / medicalQuestions.Length > 0.5)
        {
            Console.WriteLine("\nCongratulations! You scored above 50%. Your social score increases 15\n");
            return 15;
        }
        else
        {
            Console.WriteLine("\nYou did not score above 50%. Better luck next time.");
            return 5;
        }

            Console.ReadLine();
        }

        static bool CheckAnswer(int selectedIndex)
        {
            return selectedIndex == 0; 
        }

        static string GetCorrectAnswer(string[] options)
        {
            return options[0].Substring(3); 
        }
    

        /***************** HERBS GATHERING TASK ***********************/

        private int HerbsGathering()
            {
                Console.WriteLine(TextArtManager.GetTextArt("Herbs"));

                Console.WriteLine("After defeating the virus that weakened the health of the villagers. " +
                                  "You can explore the forest in peace. Go on and find some herbs!\n");

                List<string> collectedHerbs = new List<string>();
                List<string> availableHerbs = new List<string>
                {
                    "Lotus",
                    "Emberfern",
                    "Dreamroot",
                    "Featherfern",
                    "Emberleaf"
                };

                while (true)
                {
                    Console.WriteLine(TextArtManager.GetTextArt("Herbs"));

                    Console.WriteLine("After defeating the virus that weakened the health of the villagers. " +
                                      "You can explore the forest in peace. Go on and find some herbs!\n");

                    Console.WriteLine("\n > Explore forest and gather mystical healing herbs");
                    Console.WriteLine("\n > 1. Explore forest further");
                    Console.WriteLine("\n > 2. Show gathered herbs");
                    Console.WriteLine("\n > 3. Exit the game, but keep in mind that you will dissapoint the citizens...\n");
                    Console.Write(" > ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("\nExploring the forest...\n");
                            string foundHerb = ExploreForest(availableHerbs);
                            if (!string.IsNullOrEmpty(foundHerb))
                            {
                                Console.WriteLine($"\nYou found the: {foundHerb}\n");
                                collectedHerbs.Add(foundHerb);
                                Thread.Sleep(2000);
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("\nUnfortunately, you haven't found anything, keep searching\n");
                                Thread.Sleep(2000);
                                Console.Clear();
                            }

                            // Conditional statement
                            if (collectedHerbs.Count == availableHerbs.Count)
                            {
                                Console.WriteLine("\nCongratulations! You found every kind of herb in forest...\n");
                                return 15;
                            }
                            break;
                        case "2":
                            Console.WriteLine("\nCollected kinds of herbs:");
                            foreach (string herb in collectedHerbs)
                            {
                                Console.WriteLine($"- {herb}");
                            }
                            break;
                        case "3":
                            Console.WriteLine("\nGreat, these herbs definitely won't go to waste. \nI think it would be possible to make discover new medicines out of this...");
                            return 15;
                        default:
                            Console.WriteLine("\nThis is not valid command\n");
                            break;
                    }
                }
            }

        static string ExploreForest(List<string> availableHerbs)
        {
            Random random = new Random();
            if (random.Next(0, 2) == 0)
            {
                return availableHerbs[random.Next(availableHerbs.Count)];
            }
            return null;
        }

        /****************** MAP SECTION *********************/

        public void showMap(Room currentRoom)
        {
            string VillageCenter = "     ";
            string church = "     ";
            string forest = "     ";
            string market = "     ";
            string hospital = "     ";
            string DeepCave = "     ";
            string MedicalLab = "     ";


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
                case "Deep Cave":
                    DeepCave = "*You*";
                    break;
                case "Medical Lab":
                    MedicalLab = "*You*";
                    break;
                    

            }

            string map = $@"

                                 +--------+--------+            +----------------+          +-----------------+
                                 |                 |            |                |          |                 |
                                 |       Cave      +------------+      Church    +----------+   Medical Lab   |
                                 |    {DeepCave}        |            |     {church}      |    {MedicalLab} |               |
                                 +--------+--------+            +--------+-------+          +--------+--------+ 
                                          |                              |                           |
                                          |                              |                           |
                                          |                              |                           |
                                          |                              |                           |
                                 +--------+--------+            +--------+-------+          +--------+--------+
                                 |                 |            |                |          |                 |
                                 |   Green Forest  +------------+ Village Center +----------+     Hospital    |
                                 |      {forest}      |            |     {VillageCenter}      |            |   {hospital}       |
                                 +--------+--------+            +--------+-------+          +-----------------+
                                                                         |
                                                                         |
                                                                         |
                                                                         |
                                                                 +-------+--------+         +-----------------+
                                                                 |                |         |                 |
                                                                 +  Town Market   +---------+                 |
                                                                 |    {market}       |         |                 |
                                                                 +----------------+         +-----------------+ 


                        ";

            Console.WriteLine(map);
        }

        /****************** INTRODUCTION SECTION *********************/

        //Encapsulated introduction
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

                Console.WriteLine(TextArtManager.GetTextArt("MedicIntroChapter"));

                PrintText("\nBefore you start playing turn on fullscreen\n"+
                 "\nHello Brave Adventurer! In Chapter III, you're the important Medic character. You're not just playing a game... " +
                 "\nYou're a healer, life saver and you are bringing hope to your friends and whole village." +
                 "\nGood luck, Medic! I hope you will save lots of lives and win many battles!" +
                 "\nFirstly you need to choose your hero name.\n", 15);

                string hero_name = GetHeroName();

                Console.WriteLine("\nHello " + hero_name + "! This name is perfect. Now let's begin journey!");

                Console.WriteLine("\nPress any key to continue...");

                PressKey();

                Console.Clear();

                Console.WriteLine(TextArtManager.GetTextArt("MedicCharacter"));

            }

            void Intro2()
            {
                Console.WriteLine("There will be Text Art");
            }

            void Intro3()
            {
                Console.WriteLine("There will be Text Art");
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

        public static string GetHeroName()
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

