namespace HopeRising
{
    public delegate int TaskAction();  // Changed to return a bool indicating success or failure

    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; private set; }
        private TaskAction Action { get; set; }
        public Room Room { get; private set; } // The room where the task is located
        public Quest RelatedQuest { get; set; } // Add this property
        
        // Social score impact (can be positive or negative)
        public int SocialScoreImpact { get; set; }
        private readonly TaskAction action;
        public Item? RewardItem { get; private set; }
        public Item? RequiredItem { get; private set; } // Item required to perform the task


/// <summary>
/// here is where you make the task
/// </summary>
/// <param name="name">give the task a Name</param>
/// <param name="description">Describe what the task is about</param>
/// <param name="relatedQuest">Add what quest this task is related to</param>
/// <param name="room">add the room where the task is going to be in</param>    
/// <param name="action"> here you give the related method to execute</param>
        public Task(string name, string description, Quest relatedQuest , Room room , TaskAction action, Item? requiredItem = null, Item? rewardItem = null)
        {
            Name = name;
            Description = description;
            IsCompleted = false;
            Action = action;
            RelatedQuest = relatedQuest;
            Room = room;
            this.action = action;
            RewardItem = rewardItem;
            RequiredItem = requiredItem;

        }
        public bool CanExecute(List<Item> inventory)
        {
            if (RequiredItem == null)
                return true; // No item required, can execute

            return inventory.Any(item => item.Name == RequiredItem.Name);
        }

        public int Execute(Game game)
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                int scoreChange = action();
                if (RewardItem != null)
                {
                    game.AddItemToInventory(RewardItem);
                    Console.WriteLine($"You received an item: {RewardItem.Name}");
                }
                
                return scoreChange;
            }
            Console.WriteLine($"Task '{Name}' is already completed.");
            return 0;
        }
    }
}
