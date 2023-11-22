namespace WorldOfZuul
{
    public class Chapter4Engineer : IChapter // Ensure it implements IChapter
    {
        private Room? somewhere;
        private Room? outside;
        private Room? theatre;
        private Room? pub;
        private Room? lab;
        private Room? office;
        private List<Quest> quest = new List<Quest>();


        public Chapter4Engineer()
        {
            CreateRooms();
            lab.AddQuest(new Quest("Find Data", "Locate the missing data in the lab."));
        }    
            
        private Dictionary<string, bool> quests = new Dictionary<string, bool>
        {
            { "FindKey", false },
            { "UnlockDoor", false },
            // Add other quests here
        };


        public void CompleteQuest(string questName)
        {
            if (quests.ContainsKey(questName))
            {
                quests[questName] = true;
            }
        }



        public bool CanAdvanceToNextChapter()
        {
            // Example: Require all quests to be completed to advance
            return quests.Values.All(completed => completed);
        }





        public bool IsCompleted()
        {
            // Additional logic to determine if the chapter is completed
            // For example, it could be the same as CanAdvanceToNextChapter
            return CanAdvanceToNextChapter();
        }



        public void CreateRooms()
        {
            // Initialize rooms
            somewhere   = new Room("somewhere"  , "you're somewhere, don't know where but somewhere");
            outside     = new Room("Outside"    , "You're standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");
            theatre     = new Room("Theatre"    , "You're finding yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");
            pub         = new Room("Pub"        , "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");
            lab         = new Room("Lab"        , "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.");
            office      = new Room("Office"     , "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");

            // Set exits
            somewhere.SetExit("east", pub);
            outside.SetExits(null, theatre, lab, pub);
            theatre.SetExit("west", outside);
            pub.SetExits(null, outside, null, somewhere);
            lab.SetExits(outside, office, null, null);
            office.SetExit("west", lab);
        }



        public Room GetStartRoom() => outside;

        public bool IsCompleated() => true;
    }
}