namespace WorldOfZuul
{
    public delegate bool TaskAction();  // Changed to return a bool indicating success or failure

    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; private set; }
        private TaskAction Action { get; set; }
        public Room Room { get; private set; } // The room where the task is located
        public Quest RelatedQuest { get; set; } // Add this property



/// <summary>
/// here is where you make the task
/// </summary>
/// <param name="name">give the task a Name</param>
/// <param name="description">Describe what the task is about</param>
/// <param name="relatedQuest">Add what quest this task is related to</param>
/// <param name="room">add the room where the task is going to be in</param>
/// <param name="action"> here you give the related method to execute</param>
        public Task(string name, string description, Quest relatedQuest , Room room , TaskAction action)
        {
            Name = name;
            Description = description;
            IsCompleted = false;
            Action = action;
            RelatedQuest = relatedQuest;
            Room = room;
        }


        public void Execute()
        {
            if (!IsCompleted)
            {
                bool success = Action?.Invoke() ?? false;  // Execute the action and get the result
                if (success)
                {
                    IsCompleted = true;  // Mark as completed only if successful
                }
                else
                {
                    Console.WriteLine("Task not completed. Try again or check if you're missing something.");
                }
            }
            else
            {
                Console.WriteLine($"Task '{Name}' is already completed.");
            }
        }
    }
}
