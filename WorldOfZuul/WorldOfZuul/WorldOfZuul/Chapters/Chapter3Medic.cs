using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
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
        public bool IsCompleted { get; set; }
        private Room? PublicHospital;
        private Room? VillageCenter;
        private Room? GreenForest;
        private Room? HolyChurch;
        private Room? TownMarket;
        private Room? DeepCave;
        private Room? MedicalLab;
        private Room? OldRanch;

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
            Item BookOfHerbGathering = new Item("Book Of Herb Gathering", "With this book, you will now which herbs to gather...", "You'll get this after defeating sickness in forest");

            Item Herbs = new Item("Fragrant Herbs", "This herbs will come in handy to brew potions...", "You'll discover new kind of herbs when you'll explore the forest");

            Item MedicalKnowledge = new Item("Medical Knowledge", "You have proven that you are real medic", "You need to finish quest in Church");

            Item BasicInformations = new Item("Basic Village Informations", "Now you know what is going in the village", "You need to talk with villagers");

            Item CardAccessingToLab = new Item("Access to Medical Lab", "This is what you need, new medicines are just waiting to discover", "You'll get this card after mayor would know that the sickness is defeated");

            Item Medicines = new Item("Freshly Discovered Medicines", "They will come in handy to help the miners in cave", "You need to go discover medicines in lab");

            Item GoldBar = new Item("Shiny Gold Bar", "I think you can trade this gold bar for something in Market...", "You'll get this gold bar from grateful miner");

            Item RanchKey = new Item("Old Rusty Key", "Farmer gave you this key, it should open the ranch...", "You'll receive it from Farmer after talk...");

            Item TownMarketTicket = new Item("Slightly Torn Ticket", "With this ticket you can now enter the Town Market...", "You'll receive it from Travelling Merchant after talk");

            Item SDG3Book = new Item("Very Well-made Book", "With help of this book, you can finish the SDG Quiz and prepare for the final quest...", "You'll receive it after trade with Travelling Merchant");

            Item SDG3Knowledge = new Item("Sustainable Development Goal 3 Knowledge", "You have provent that you are worthy to be called SDG Medic", "You'll get it after finishing SDG Quiz in Holy Church");
            
            Item Reputation = new Item("Good reputation among citizens", "Knowing that you are helping people, you sleep better...", "You'll get it after treating patients in Public Hospital");

            Item HospitalDoorCode = new Item("Door to access Public Hospital doors", "This will come in handy to get to Hospital next time...", "You firsly need to guess it...");

            // Initialize rooms
            
            VillageCenter = new Room("Village Center", "\nYou stand in the heart of a charming village. Cobblestone paths lead in different directions. To the east is a bustling market, to the south is a quaint church, and to the west is a peaceful forest.");

            PublicHospital = new Room("Public Hospital", "\nYou find yourself in a sterile environment filled with the scent of antiseptic. White walls surround you, and the sound of distant footsteps echoes through the corridors.");

            GreenForest = new Room("Green Forest", "\nYou step into a dense and enchanting forest. Tall trees tower above, creating a canopy of leaves that filters sunlight. Birds chirp in the distance, and the air is filled with the scent of pine.");

            HolyChurch = new Room("Holy Church", "\nAs you enter, the atmosphere becomes serene. Stained glass windows filter colorful light into the church. Wooden pews line the aisles, and silence is broken only by the occasional creaking of old wooden floorboards.");

            TownMarket = new Room("Town Market", "\nThe hustle and bustle of a vibrant market surround you. Stalls are filled with colorful fruits, vegetables, and various goods. Merchants call out their prices, creating a lively and dynamic atmosphere.");
            
            DeepCave = new Room("Deep Cave", "\nYou find yourself standing at the entrance of a mysterious and foreboding cave. The air is cool, and the only sound is the distant dripping of water echoing through the cavern. The darkness within is absolute, and you can barely make out the rough walls covered in ancient rock formations.");

            MedicalLab = new Room("Medical Laboratory", "\nYou walk into a makeshift medical lab. The air smells a bit old, and the lights are not very bright. The lab tables look worn, and there's not much fancy equipment. Some old tools and containers are scattered around. It's not high-tech, but you sense a kind of determined effort in this humble space");

            OldRanch = new Room("Old Ranch", "\nYou stand in the Old Ranch, surrounded by the scent of aged wood and leather. Weathered barns and vast fields paint a nostalgic scene, accompanied by the gentle sounds of cattle and horses.");

            // Set exits

            // Set exits -> (north, east, south, west) & Set exit -> ("north/east/south/west", "place")

            VillageCenter.SetExits(HolyChurch, PublicHospital, TownMarket, GreenForest);

            DeepCave.SetExits(null, HolyChurch, GreenForest, null);

            HolyChurch.SetExits(null, MedicalLab, VillageCenter, DeepCave);

            PublicHospital.SetExits(MedicalLab, null, null, VillageCenter);

            TownMarket.SetExit("north", VillageCenter);

            GreenForest.SetExits(DeepCave, VillageCenter, null, null);

            MedicalLab.SetExits(null, null, PublicHospital, HolyChurch);

            OldRanch.SetExits(PublicHospital, null, null, TownMarket);

            /***************** QUEST SECTION ****************/

            //Create quest (short and long description)

            Quest TalkingWithLocals = new Quest("Let's talk about the Village matters", "You came to this, but really you don't know anything about it yes? Go and talk with the locals...");
            
            Quest ExploreTheForest = new Quest("Explore Green Forest", "Defeat the sickness, You've received information from the mayor of the village " +
                                                                      "that the source of the disease was in the forest. You should nip this in the bud...");

            Quest ChurchQuizzes = new Quest("Do Medical Quiz", "You need to finish this quiz to prepare for healing citizens!");

            Quest DeepCaveRescue = new Quest("Cave Rescue Mission", "You received information about the collapse of the ceiling in the cave. You have to organize a rescue mission, the miners won't last long, hurry up!");

            Quest OldRanchSickness = new Quest("Sickness Killing Animals", "The Farmer counts on your help to save his animals, go on!");

            Quest TownMarketTrade = new Quest("Town Market Trade", "You will definitely find interesting things at the town market.");

            Quest HospitalHealing = new Quest("Help in Public Hospital", "Citizens in Public Hospital need medical care...");
            
            //Initialize tasks

            Task GatheringBasicInformations = new Task("Informations", "You're here, but it seems you're not quite in the loop. Best go chat with the folks around here", TalkingWithLocals, VillageCenter, VillageCenterInterview, null, BasicInformations);

            Task MedicalQuizInHolyChurch = new Task("Quiz", "Go on and finish this quiz!", ChurchQuizzes, HolyChurch, MedicalQuizInChurch, BasicInformations, null);

            Task DefeatingSickness = new Task("Sickness", "You need to free villagers from the sickness. Source of the virus comes from the forest", ExploreTheForest, GreenForest, BattleWithSickness, BasicInformations, BookOfHerbGathering);

            Task GatherHerbs = new Task("Herbs", "In forest you can find useful herbs, they will come in handy to discover new medicines!", ExploreTheForest, GreenForest, HerbsGathering, BookOfHerbGathering, Herbs);

            Task InformMayor = new Task("Inform", "You defeated the virus that made the lives of the locals miserable, you have to go and tell the Mayor Campbell about it", TalkingWithLocals, VillageCenter, VillageCenterInformMayor, Herbs, CardAccessingToLab);

            Task DiscoveringNewMedicines = new Task("Medicines", "Inventing new medicines is necessary to help injured miners. Go on fast!", DeepCaveRescue, MedicalLab, DiscoverMedicines, null, null);

            Task RescueMiners = new Task("Mission", "You are in front of cave entrance, you need to find miners and help them!", DeepCaveRescue, DeepCave, MinerRescue, Medicines, GoldBar);

            Task InformMayor2 = new Task("Inform", "You did a great job of rescuing the injured miners, don't wait and tell the Mayor Campbell about it!", TalkingWithLocals, VillageCenter, VillageCenterInformMayor2, GoldBar, RanchKey);

            Task HealAnimals = new Task("Animals", "You need to help animals as soon as possible, no one knows what's wrong with them...", OldRanchSickness, OldRanch, RescueAnimals, RanchKey, null);

            Task InformFarmer = new Task("Inform", "Farmer would be very happy that you've helped his animals, go inform him!", TalkingWithLocals, VillageCenter, VillageCenterInformFarmer, RanchKey, TownMarketTicket);

            Task TradeInTownMarket = new Task("Trade", "This Travelling Merchant seems quite shady, but you need to trade with him", TownMarketTrade, TownMarket, TradeWithTravellingMerchant, TownMarketTicket, SDG3Book);

            Task SDGQuizInHolyChurch = new Task("SDG", "This quiz will confirm whether you are worthy of being an SDG Medic", ChurchQuizzes, HolyChurch, SDGQuizInChurch, SDG3Book, SDG3Knowledge);

            Task TreatingPatients = new Task("Patients", "Citizens in Public Hospital need help, you are the only one who can cure them...", HospitalHealing, PublicHospital, HospitalPatients, SDG3Knowledge, Reputation);

            Task GuessTheCode = new Task("Guessing", "You've spent so much time in hospital, that now you can't exit...", HospitalHealing, PublicHospital, DoorNumberGuessingGame, Reputation, HospitalDoorCode);

            Task FinalTalkWithMayor = new Task("Mayor", "This is final talk, your adventure is coming to an end, you have already done your part...", TalkingWithLocals, VillageCenter, FinalTalk, HospitalDoorCode, null);

            //Add quests to the chapters quests lists

            Quests.Add(ExploreTheForest);
            
            Quests.Add(DeepCaveRescue);

            Quests.Add(TalkingWithLocals);

            Quests.Add(ChurchQuizzes);

            Quests.Add(OldRanchSickness);

            Quests.Add(TownMarketTrade);

            Quests.Add(HospitalHealing);

            // Add task to the quest list

            ExploreTheForest.AddTask(DefeatingSickness);
            ExploreTheForest.AddTask(GatherHerbs);

            ChurchQuizzes.AddTask(MedicalQuizInHolyChurch);
            ChurchQuizzes.AddTask(SDGQuizInHolyChurch);


            TalkingWithLocals.AddTask(GatheringBasicInformations);
            TalkingWithLocals.AddTask(InformMayor);
            TalkingWithLocals.AddTask(InformMayor2);
            TalkingWithLocals.AddTask(InformFarmer);
            TalkingWithLocals.AddTask(FinalTalkWithMayor);

            DeepCaveRescue.AddTask(DiscoveringNewMedicines);
            DeepCaveRescue.AddTask(RescueMiners);

            OldRanchSickness.AddTask(HealAnimals);

            TownMarketTrade.AddTask(TradeInTownMarket);

            HospitalHealing.AddTask(TreatingPatients);
            HospitalHealing.AddTask(GuessTheCode);

            //Add task to the room

            GreenForest.AddTask(DefeatingSickness);
            GreenForest.AddTask(GatherHerbs);

            VillageCenter.AddTask(GatheringBasicInformations);
            VillageCenter.AddTask(InformMayor);
            VillageCenter.AddTask(InformMayor2);
            VillageCenter.AddTask(InformFarmer);
            VillageCenter.AddTask(FinalTalkWithMayor);

            HolyChurch.AddTask(MedicalQuizInHolyChurch);
            HolyChurch.AddTask(SDGQuizInHolyChurch);

            MedicalLab.AddTask(DiscoveringNewMedicines);
            
            DeepCave.AddTask(RescueMiners);

            OldRanch.AddTask(HealAnimals);

            TownMarket.AddTask(TradeInTownMarket);

            PublicHospital.AddTask(TreatingPatients);
            PublicHospital.AddTask(GuessTheCode);
            
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
            return $@" > Your Medic Score is : {medicScore}";
        }

        /************************* HOSPITAL HEALING TASK ***************************/

        private int DoorNumberGuessingGame()
        {
            Random random = new Random();

            int min = 1;
            int max = 100;
            int guess;
            int number;
            int guesses;

            PrintText("You worked so hard that I lost track of time. It's late and there's no one with the keys. " +
                      "\nYou have to guess the hospital door code to get out...");

            PrintText("Press any key...");

            PressKey();


            Console.WriteLine("Guess a code number of between " + min + " - " + max + " : ");
            guess = Convert.ToInt32(Console.ReadLine());

            number = random.Next(min, max + 1);
            guesses = 1;

            if (guess == number)
            {
                Console.WriteLine("Number: " + number);
                Console.WriteLine("Guesses: " + guesses);
                Console.WriteLine("Nice! You guessed the code you can now exit the hospital...");
                return 15; 
            }

            while (guess != number)
            {
                Console.WriteLine(TextArtManager.GetTextArt("CodeMedic"));
                
                Console.WriteLine("Guess: " + guess);

                if (guess > number)
                {
                    Console.WriteLine(guess + " is too high!");

                    Thread.Sleep(1500);
                    Console.Clear();
                }
                else if (guess < number)
                {
                    Console.WriteLine(guess + " is too low!");
                    
                    Thread.Sleep(1500);
                    Console.Clear();
                }

                Console.WriteLine("Guess a number between " + min + " - " + max + " : ");
                guess = Convert.ToInt32(Console.ReadLine());

                guesses++;
            }

            Console.WriteLine("Number: " + number);
            Console.WriteLine("Guesses: " + guesses);
            Console.WriteLine("Okay, you guessed ");

            Console.ReadKey();
            return 0; 
        }

        static int patientsTreated = 0;
        static int medicalSupplies = 20;
        static int reputation = 0;
        private int HospitalPatients()
        {

            Console.WriteLine("\nPatients are waiting for you to cure them!");
            Console.WriteLine("You are the medic in charge. " +
                              "\nYour goal is to hit 50 reputation score.");

            while (true)
            {
                DisplayStats();

                Console.WriteLine("\n Choose an action:");
                Console.WriteLine(" > 1. Treat a patient");
                Console.WriteLine(" > 2. Restock medical supplies");
                Console.WriteLine(" > 3. Quit, but you won't get any social score");
                Console.Write(" > ");
                int choice = GetChoice(1, 3);

                switch (choice)
                {
                    case 1:
                        TreatPatient();
                        break;
                    case 2:
                        RestockSupplies();
                        break;
                    case 3:
                        EndGame();
                        return 0;
                }

                Thread.Sleep(2000);
                Console.Clear();

                if (reputation == 50)
                {
                    Console.WriteLine("Nice! Health of the village citizens is increasing...");
                    EndGame();
                    return 15;
                }
            }

            static void DisplayStats()
            {
                Console.WriteLine($"\nPatients Treated: {patientsTreated}");
                Console.WriteLine($"Medical Supplies: {medicalSupplies}");
                Console.WriteLine($"Reputation: {reputation}");
            }

            static int GetChoice(int min, int max)
            {
                int choice;
                while (true)
                {
                    Console.Write("Enter your choice: ");
                    if (int.TryParse(Console.ReadLine(), out choice) && choice >= min && choice <= max)
                    {
                        return choice;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid option.");
                    }
                }
            }
            
            static void TreatPatient()
            {
                if (medicalSupplies > 0)
                {
                    Console.WriteLine("Treating a patient...");

                    Random random = new Random();
                    int treatmentResult = random.Next(1, 11);

                    if (treatmentResult <= 5) // 50% success rate
                    {
                        Console.WriteLine("Patient treated successfully!");
                        patientsTreated++;
                        reputation += 10;
                    }
                    else
                    {
                        Console.WriteLine("Treatment failed. Patient's condition worsened.");
                        reputation -= 5;
                    }

                    medicalSupplies--;
                }
                else
                {
                    Console.WriteLine("Not enough medical supplies. Restock before treating more patients.");
                }
            }

            static void RestockSupplies()
            {
                Console.WriteLine("Restocking medical supplies...");
                medicalSupplies += 10;
                Console.WriteLine("Medical supplies restocked (+10).");
            }

            static void EndGame()
            {
                Console.WriteLine($"Total patients treated: {patientsTreated}");
                Console.WriteLine($"Final reputation: {reputation}");
                Console.WriteLine("Goodbye!");
            }
        }

        /************************* ANIMAL RESCUE TASK ******************/

        public class Animal
        {
            public string Name { get; set; }
            public string Species { get; set; }
            public bool IsHealthy { get; set; }

            public Animal(string name, string species)
            {
                Name = name;
                Species = species;
                IsHealthy = false;
            }
        }
        public class Clinic
        {
            private List<Animal> animals;
            private Random random;

            public Clinic()
            {
                animals = new List<Animal>();
                random = new Random();
            }

            public void AddAnimal(Animal animal)
            {
                animals.Add(animal);
            }

            public void TreatAnimal(Animal animal)
            {
                // Simulate treatment success based on a random chance
                animal.IsHealthy = random.Next(0, 2) == 0;
            }

            public IEnumerable<Animal> Animals => animals;

            public void PrintAnimalStatus()
            {
                Console.WriteLine("\nCurrent Animal Status:");
                foreach (var animal in animals)
                {
                    Console.WriteLine($"{animal.Name} ({animal.Species}): {(animal.IsHealthy ? "Healthy" : "Unhealthy")}");
                }
            }

            public int GetCuredAnimalCount()
            {
                return animals.Count(animal => animal.IsHealthy);
            }
        }
        public int RescueAnimals()
        {
            Clinic clinic = new Clinic();
            int curedAnimalCount = 0;

            // Sample animals
            Animal deer = new Animal("John", "Deer");
            Animal cat = new Animal("Whiskers", "Cat");
            Animal cow = new Animal("Buttercup", "Cow");
            Animal horse = new Animal("Mustang", "Horse");


            clinic.AddAnimal(deer);
            clinic.AddAnimal(cat);
            clinic.AddAnimal(cow);
            clinic.AddAnimal(horse);

            Console.WriteLine("Farmer animals are unhealthy and sick, help them!");

            while (true)
            {
                Console.WriteLine(TextArtManager.GetTextArt("MedicCow"));
                Console.WriteLine("\n > 1. Diagnose and Treat Animals");
                Console.WriteLine(" > 2. Print Animal Status");
                Console.WriteLine(" > 3. Exit");

                Console.Write(" > Select an option: ");
                Console.Write(" > ");
                string choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        foreach (var animal in clinic.Animals)
                        {
                            Console.WriteLine($"\nDiagnosing and treating {animal.Name}...");
                            clinic.TreatAnimal(animal);
                            Console.WriteLine($"{animal.Name} is {(animal.IsHealthy ? "now healthy!" : "still unwell.")}");

                            Thread.Sleep(2000);
                            Console.Clear();
                        }

                        curedAnimalCount = clinic.GetCuredAnimalCount();

                        if (curedAnimalCount >= 3)
                        {
                            Console.WriteLine("Congratulations! You cured 3 animals. You win!");
                                
                            return 15;
                            break;
                        }
                        break;

                    case "2":
                        clinic.PrintAnimalStatus();
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("There is no such option...");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                }

                Thread.Sleep(1000);
            }
        }

        /****************** MINER RESCUE TASK **********************/
        private int MinerRescue()
        {
            Console.WriteLine("\n > Oh no! Miners are stuck down there, hurry up and help them");
            Console.WriteLine(" > You are a rescuer tasked with finding and healing injured miners in a cave.");
            Console.WriteLine(" > You have three possible locations to search.");

            int totalMiners = 3;
            int minersHealed = 0;

            while (minersHealed < totalMiners)
            {
                Console.WriteLine(TextArtManager.GetTextArt("CaveMedic"));
                Console.WriteLine();

                Console.WriteLine("\n > Choose a location to search (1, 2, or 3):");
                Console.Write(" > ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int location) && location >= 1 && location <= 3)
                {
                    // Simulate search results
                    bool minerFound = (new Random()).Next(2) == 1;

                    if (minerFound)
                    {
                        Console.WriteLine("Nice! You found an injured miner in location " + location + "!");
                        Console.WriteLine("You provide first aid and heal the miner.");
                        minersHealed++;

                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("No injured miners found in location " + location + ".");

                        Thread.Sleep(2000);
                        Console.Clear();

                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");

                    Thread.Sleep(2000);
                    Console.Clear();
                }
            }

            Console.WriteLine("\n Congratulations! You have successfully healed all injured miners.");
            Console.WriteLine("\n One miner was so grateful for saving his life, that he gave you heavy gold bar!");
            Console.WriteLine("\n Go and talk with mayor!");

            return 15;
        }

        /****************** TRADE IN TOWN MARKET TASK **********************/

        private int TradeWithTravellingMerchant()
        {
            bool gatheredAllInfo = false;
            HashSet<string> infoCollected = new HashSet<string>();

            string header = "\nIt is certain that this traveling merchant has interesting items\n";
            string[] option = {
                               "1. Travelling Merchant"
                              };

            while (!gatheredAllInfo)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, option);

                switch (selectedIndex)
                {
                    case 0:
                        if (!infoCollected.Contains("Merchant"))
                        {
                            PrintText("\nMedic: \nHello... The thing is that I didn't bring any money with me, but I have a very valuable gold bar.", 15);

                            PrintText("\nTravelling Merchant: \nWonderful! I have something much more valuable than gold to offer you.", 15);

                            PrintText("\nMedic: \nHmmm... That's interesting... What is that?", 15);

                            PrintText("\nTravelling Merchnat: \n I'm sure you're familiar with them, but you can never have too many of them.", 15);

                            PrintText("\nSo far, I have seen that you follow the principles of the SDGs, especially point 3 regarding healthy living and well being. " +
                                      "\nNow you have to prove that you are worthy of being called an SDG Medic.");

                            PrintText("\nHere you have the SDG Book. It will go with you all your life. Now go to Holy Church and finish the SDG Quiz...");

                            PrintText("\nThank you for that! I am sure I am worthy...");

                            infoCollected.Add("Merchant");

                        }
                        else
                        {
                            Console.WriteLine("\nYou've already talked with Merchant... Go talk with others!\n");
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

            Console.WriteLine("\nYou got the SDG book from Traveling Merchant." +
                              "\nNow with its help you have to complete SDG quiz in church. " +
                              "\nDon't wait, do it!");

            return 5;

        }

        /****************** TALKING WITH LOCALS TASK **********************/
        private int VillageCenterInterview()
        {
            bool gatheredAllInfo = false;
            HashSet<string> infoCollected = new HashSet<string>();

            string header = "\nYou came to the village, go gather some informations. Who you want to talk with?\n";
            string[] option = {
                                " > 1. Mayor of the Village"
                                ," > 2. Old Man laying on the pavement"
                                ," > 3. Someone who is looking like Farmer"
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
                            PrintText("I'm Mayor Campbell, if you will need anything go talk to me! " +
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
                                "\n I'm Farmer Ronald..." +
                                "\n I hope that now you, like the previous hero, will save us from a difficult situation with health care.", 15);

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

            string header = "\nYou need to inform mayor about your accomplishment with defeating the sickness.\n";
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
        
        private int VillageCenterInformMayor2()
        {
            bool gatheredAllInfo = false;
            HashSet<string> infoCollected = new HashSet<string>();

            string header = "\nMayor needs to know about healed miners... Maybe talk with the Farmer also?.\n";
            string[] option = {
                                " > 1. Mayor of the Village"
                                ," > 2. Farmer Ronald"
                                };                

            while (!gatheredAllInfo)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, option);

                switch (selectedIndex)
                {
                    case 0:
                        if (!infoCollected.Contains("Mayor"))
                        {
                            PrintText("\nMedic: \nMayor Campbell, good news! I went into the cave and healed all the trapped miners...", 15);
                            PrintText("\nMayor Campbell:  \nOh, that's a relief! You did great, Medic. The whole village is thankful for your courage.", 15);
                            PrintText("\nMedic: \nIt was tough, but I'm happy I could help. The miners are getting medical care, and they'll be okay soon.", 15);
                                     
                            infoCollected.Add("Mayor");

                        }
                        else
                        {
                            Console.WriteLine("\nYou've already talked with Mayor... Go talk with others!\n");
                        }
                        break;
                    case 1:
                        if (!infoCollected.Contains("Farmer Ronald"))
                        {
                            PrintText("\nFarmer: \nHey, you! Come here... ", 15);

                            PrintText($"\nMedic: \nHello Farmer! What's up?", 15);

                            PrintText("\nFarmer: \nThis is terrifying, all my animals have a terrible sickness, I don't what is going on! Please help them...", 15);

                            PrintText($"\nMedic: \nOkay... Where I need to go?", 15);

                            PrintText($"\nFarmer: \nAnimals are on my ranch near the Town Market, here you have the key...", 15);

                            PrintText($"\nMedic: \nOkay! I'm on my way!", 15);

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

                if (infoCollected.Count >= 2)
                {
                    gatheredAllInfo = true;
                }

            }

            Console.Clear();

            Console.WriteLine("\nYou talked to everyone, with your current knowledge you can go and perform the tasks entrusted to you!\n");

            return 5;

        }

        private int VillageCenterInformFarmer()
        {
            bool gatheredAllInfo = false;
            HashSet<string> infoCollected = new HashSet<string>();

            string header = "\nThe farmer will be delighted that you helped him with the health of his animals, let him know!.\n";
            string[] option = {
                                " > 1. Farmer Ronald"
                                ," > 2. Someone who looks like Merchant"
                                };

            while (!gatheredAllInfo)
            {
                int selectedIndex = InteractiveMenu.MultichoiceQuestion(header, option);

                switch (selectedIndex)
                {
                    case 0:
                        if (!infoCollected.Contains("Farmer"))
                        {
                            PrintText("\nMedic: \nHey Farmer! Everything is fine, the animals recovered, I cured them!", 15);
                            PrintText("\nFarmer:  \nOh, that's wonderful! Thank you for this, good man! ", 15);
                            PrintText("\nMedic: \nNo worries, that's why I'm here, if the animals still have problems, tell me...", 15);

                            infoCollected.Add("Farmer");

                        }
                        else
                        {
                            Console.WriteLine("\nYou've already talked with Mayor... Go talk with others!\n");
                        }
                        break;
                    case 1:
                        if (!infoCollected.Contains("Merchant"))
                        {
                            PrintText("\nTravelling Merchant: \nWhat's up mate?", 15);

                            PrintText($"\nMedic: \nHello, you look like some kind of Merchant...", 15);

                            PrintText("\nTravelling Merchant: \nYes, I sell different kinds of items, if you want to buy something we can meet at Town Market...", 15);

                            PrintText("\nTravelling Merchant: \nTo enter Town Market you need to have ticket with you... Here you go!", 15);

                            PrintText($"\nMedic: \nOkay... Thank you... See you then...", 15);

                            PrintText($"\nFarmer: \nSee you, double check that you took money with you ;)", 15);

                            infoCollected.Add("Merchant");
                        }
                        else
                        {
                            Console.WriteLine("\nYou've already talked with Merchant... Go talk with others!\n");
                        }
                        break;
                    default:
                        Console.WriteLine("\nYou can't to that! This choice is not valid.\n");
                        break;
                }

                Console.ReadKey();

                if (infoCollected.Count >= 2)
                {
                    gatheredAllInfo = true;
                }

            }

            Console.Clear();

            Console.WriteLine("\nNow you can go and make trades at the Town Market\n");

            return 5;

        }

        private int FinalTalk()
        {
            bool gatheredAllInfo = false;
            HashSet<string> infoCollected = new HashSet<string>();

            string header = "\nYou need to inform mayor about your accomplishment with defeating the sickness.\n";
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
                            PrintText("\nMedic: \nHello Mayor Campbell, I have great news! I've was in Public Hospital and I've treated many patients...", 15);

                            PrintText("\nMayor Campbell: \nYou are amazing, the village has really come back to life and you can see the positive changes at first glance. " +
                                                         "\nIt's a pity you have to leave the village.", 15);

                            PrintText("\nMedic: \nDon't worry, citizens got a good example of what all this should look like.", 15);

                            PrintText("\nMayor Campbell: \nThank you for everything you have done for the village, I hope to see you again...", 15);

                            PrintText("\nMedic: \nGoodbye... I'm glad I could help...");
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

            Console.WriteLine("\nMayor is really grateful for your help...");

            return 5;

        }

        /****************** DISCOVER MEDICINES TASK *********************/

        private int DiscoverMedicines()
        {
            Console.WriteLine("With herbs you found in forest, you can discover some new medicines. " +
                              "\nThey will come in handy to help miners...");
            Console.WriteLine("\nMiners won't last long so hurry up...");

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
                Console.WriteLine(TextArtManager.GetTextArt("MedicinesMedic"));
                Console.WriteLine("\n > Choose an action:");
                Console.WriteLine(" > 1. Research");
                Console.WriteLine(" > 2. Experiment");
                Console.WriteLine(" > 3. Quit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine(" > You conducted research.");
                        Console.WriteLine(" > Progress in understanding the compounds...");
                        Console.WriteLine(" > No medicine discovered yet.");

                        Thread.Sleep(2000);
                        Console.Clear();
                        break;

                    case "2":
                        Console.WriteLine(" > You conducted an experiment.");
                        List<string> experimentIngredients = GetRandomIngredients(ingredients, random);
                        Console.WriteLine($" > Using ingredients: {string.Join(", ", experimentIngredients)}");

                        if (IsExperimentSuccessful(experimentIngredients))
                        {
                            Console.WriteLine(" > Success! You discovered a new medicine!");
                            discoveredMedicines++;

                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine(" > Experiment failed. Try different ingredients.");

                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }

            Console.WriteLine("\nCongratulations! You have discovered two new medicines!");
            Console.WriteLine("\nNow go and help the miners...");

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

        private int MedicalQuizInChurch()
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
        private int SDGQuizInChurch()
        {
            string[] sdgQuestions = {
                "What is the main aim of SDG 3?",
                "Which target under SDG 3 focuses on reducing deaths of newborns and children under 5?",
                "Why is Target 3.4 important in SDG 3?",
                "What does Target 3.8 aim to achieve?",
                "Which disease is mentioned in SDG 3 as part of the goal to combat communicable diseases?",
                "What does Target 3.3 of SDG 3 focus on?"
            };

                        string[][] sdgAnswers = {
                new string[] { "a) End hunger.", "b) Ensure healthy lives and well-being for everyone.", "c) Promote sustainable agriculture.", "d) Achieve gender equality." },
                new string[] { "a) Target 3.1.", "b) Target 3.2.", "c) Target 3.5.", "d) Target 3.7." },
                new string[] { "a) It addresses the reduction of road traffic injuries.", "b) It focuses on reducing the global burden of non-communicable diseases.", "c) It aims to increase air pollution.", "d) It promotes sustainable agriculture." },
                new string[] { "a) Universal access to sexual and reproductive healthcare.", "b) Universal health coverage, ensuring everyone has access to essential health services without financial hardship.", "c) Promote the use of hazardous chemicals.", "d) Achieve zero emissions." },
                new string[] { "a) Diabetes.", "b) Malaria.", "c) Asthma.", "d) Cancer." },
                new string[] { "a) Ending hunger.", "b) Ending the epidemics of AIDS, tuberculosis, malaria, and neglected tropical diseases.", "c) Promoting sustainable cities.", "d) Achieving universal education." }
            };

            int score = 0;

        for (int i = 0; i < sdgQuestions.Length; i++)
        {
            string header = sdgQuestions[i];
            string[] options = sdgAnswers[i];

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

            Console.WriteLine("\nYour score is: " + score + "/" + sdgQuestions.Length);

        if ((double)score / sdgQuestions.Length > 0.5)
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
            string ranch = "     ";


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
                case "Old Ranch":
                    ranch = "*You*";
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
                                 +--------+--------+            +--------+-------+          +--------+--------+
                                                                         |                           |
                                                                         |                           |
                                                                         |                           |
                                                                         |                           |
                                                                 +-------+--------+         +--------+--------+
                                                                 |                |         |                 |
                                                                 +  Town Market   +---------+    Old Ranch    |
                                                                 |    {market}       |         |   {ranch}              |
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
            Console.Clear();
        }

        public static void TitleAnimation(string titleText)
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

