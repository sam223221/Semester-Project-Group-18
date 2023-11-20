namespace WorldOfZuul
{
    public class Chapter5Adventure : IChapter
    {
        private Room startRoom;

        public Chapter5Adventure()
        {
            CreateRooms();
        }
            private Dictionary<string, bool> quests = new Dictionary<string, bool>
        {
            { "FindKey", false },
            { "UnlockDoor", false },
            // Add other quests here
        };

        // ... Existing methods ...

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
            // Initialize and set up rooms and exits for Chapter 5
            // Example:
            startRoom = new Room("Start Room", "Description of the start room in Chapter 5.");
            startRoom.SetExits(null,null,null,null);
            // ... other room setups ...
        }

        public Room GetStartRoom()
        {
            return startRoom;
        }

        public bool IsCompleated() => true;
    }
}
