

namespace WorldOfZuul
{
    public interface IChapter
    {
        List<Room> Rooms {get; }
        List<Quest> Quests {get; set;}
        Room GetStartRoom();
        void CreateRoomsAndQuests();
        void ShowIntroduction();
    }


}