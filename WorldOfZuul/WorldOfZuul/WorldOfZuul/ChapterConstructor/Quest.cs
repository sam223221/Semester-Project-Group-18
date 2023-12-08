namespace WorldOfZuul
{

    public class Quest
    {
        public string Name { get; }
        public string Description { get; }
        public bool IsCompleted { get; set; }
        public List<Task> Tasks { get; private set; }

        public Quest(string name, string description)
        {
            Name = name;
            Description = description;
            IsCompleted = false;
            Tasks = new List<Task>();
        }
        
        // add a task to the quest
        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        // get the current task
        public Task GetCurrentTask()
        {
            return Tasks.FirstOrDefault(t => !t.IsCompleted);
        }

        // Method to check if all tasks for this quest are completed
        public bool AreAllTasksCompleted()
        {
            return Tasks.All(task => task.IsCompleted);
        }

    }

    
}
