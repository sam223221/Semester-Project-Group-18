namespace WorldOfZuul
{
    public class Chapter4Engineer : IChapter // Ensure it implements IChapter
    {
        public List<Room> Rooms { get; private set; }
        public List<Quest> Quests {get; set;}
        private Room? somewhere;
        private Room? outside;
        private Room? theatre;
        private Room? office;
        private Room? pub;
        private Room? lab;



        public Chapter4Engineer()
        {
            Rooms = new List<Room>();
            Quests = new List<Quest>();
            CreateRoomsAndQuests();
            

        }    

        public Room? GetStartRoom() => outside;



        public void CreateRoomsAndQuests()
        {
            // Initialize rooms
            somewhere   = new Room("somewhere"  , "you're somewhere, don't know where but somewhere");
            outside     = new Room("Outside"    , "You're standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");
            theatre     = new Room("Theatre"    , "You're finding yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");
            office      = new Room("Office"     , "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");
            pub         = new Room("Pub"        , "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");
            lab         = new Room("Lab"        , "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.");


            // Set exits
            somewhere.SetExit("east", pub);
            outside.SetExits(null, theatre, lab, pub);
            theatre.SetExit("west", outside);
            pub.SetExits(null, outside, null, somewhere);
            lab.SetExits(outside, office, null, null);
            office.SetExit("west", lab);
        

            // Creat quests
            Quest labQuest  = new Quest("Data", "Locate the missing data in the lab.");
            Quest OfficeQuest = new Quest("FindKey", " you need to find the key for my office");

            //initialize tasks
            Task labTask = new Task("question", "awnser a tricky question", labQuest , lab, QuestionTask);
            Task officeTask = new Task("Key" , "need to find the key for the office lock", OfficeQuest, office , FindMyKey);
            

            // Add quests to the chapter's quest list
            Quests.Add(labQuest);
            Quests.Add(OfficeQuest);

            // add task to the quest list
            labQuest.AddTask(labTask);
            OfficeQuest.AddTask(officeTask);

            // add task to the room
            lab.AddTask(labTask);
            office.AddTask(officeTask);
                

        
            // adding thigs to rooms
            Rooms.Add(somewhere);
            Rooms.Add(outside);
            Rooms.Add(theatre);
            Rooms.Add(office);
            Rooms.Add(pub);
            Rooms.Add(lab);

        }



        /****************** down form here are only tasks*********************/




        private bool QuestionTask()
        {
            Console.WriteLine("Here's a tricky question for you:");
            Console.WriteLine("What is the capital of France?");
            Console.WriteLine("1. Paris");
            Console.WriteLine("2. London");
            Console.WriteLine("3. Berlin");

            string? answer = Console.ReadLine();

            switch (answer)
            {
                case "1":
                    Console.WriteLine("Correct! Paris is the capital of France.");
                    return true;
                
                case "2":
                case "3":
                    Console.WriteLine("That's not correct.");
                    return false;

                default:
                    Console.WriteLine("Invalid choice.");
                    return false;
            }
        }

        public bool FindMyKey()
        {
            Console.WriteLine("you found my key thanks");
            return true;
        }
    }
}