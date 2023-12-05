
namespace WorldOfZuul
{
    public class Room
    {

        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set;}
        public Dictionary<string, Room> Exits { get; private set; } = new();
        public List<Task> Tasks { get; private set; }
        public string TextArt { get; set; }
        public Item? RequiredItem { get; set; } // Item required to enter the room


        public Room(string shortDesc, string longDesc , Item? requiredItem = null)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
            Tasks = new List<Task>();
            RequiredItem = requiredItem;
        }

        public void SetExits(Room? north, Room? east, Room? south, Room? west)
        {
            SetExit("north", north);
            SetExit("east", east);
            SetExit("south", south);
            SetExit("west", west);
        }

        public void SetExit(string direction, Room? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        public bool CanEnter(List<Item> inventory)
        {
            if (RequiredItem == null)
                return true; // No item required, can enter

            return inventory.Any(item => item.Name == RequiredItem.Name);
        }

        public static implicit operator Room(string v)
        {
            throw new NotImplementedException();
        }
    }
}
