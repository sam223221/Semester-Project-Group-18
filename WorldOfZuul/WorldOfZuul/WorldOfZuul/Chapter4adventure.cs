namespace WorldOfZuul
{
    public class Chapter5Adventure : IChapter
    {
        private Room startRoom;

        public Chapter5Adventure()
        {
            CreateRooms();
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
    }
}
