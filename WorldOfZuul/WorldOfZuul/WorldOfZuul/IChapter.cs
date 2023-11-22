

namespace WorldOfZuul
{
    public interface IChapter
    {
        List<Room> Rooms {get; }
        Room GetStartRoom();
        void CreateRooms();
        void initializeQuest();
    }


}