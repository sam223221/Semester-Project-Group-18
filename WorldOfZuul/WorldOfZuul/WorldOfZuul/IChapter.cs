namespace WorldOfZuul
{
    public interface IChapter
    {
        Room GetStartRoom();
        void CreateRooms();
        bool IsCompleated();
        void CompleteQuest(string questName); // Mark a quest as completed
        bool CanAdvanceToNextChapter(); // Check if the player can advance to the next chapter
        
    }


}