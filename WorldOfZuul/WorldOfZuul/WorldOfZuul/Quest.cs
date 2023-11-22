namespace WorldOfZuul
{

    public class Quest
    {
        public string Name { get; }
        public string Description { get; }
        public bool IsCompleted { get; set; }
        public IChapter Chapter { get; }

        public Quest(string name, string description, IChapter chapter)
        {
            Name = name;
            Description = description;
            Chapter = chapter;
            IsCompleted = false;
        }
    }

    
}
