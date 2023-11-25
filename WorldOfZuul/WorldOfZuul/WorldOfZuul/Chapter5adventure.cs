using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldOfZuul
{
    public class ChapterExample : IChapter
    {
        public List<Room> Rooms { get; private set; }
        public List<Quest> Quests {get; set;}
        private Room startRoom;
        private Room anotherRoom;

        public ChapterExample()
        {
            Rooms = new List<Room>();
            Quests = new List<Quest>();
            CreateRoomsAndQuests();
        }

        public Room GetStartRoom() => startRoom;

        public void CreateRoomsAndQuests()
        {
            // Create Rooms
            startRoom = new Room("Start Room", "This is the start room of the chapter.");
            anotherRoom = new Room("Another Room", "This is another room in the chapter.");

            // Set exits for rooms
            startRoom.SetExit("north", anotherRoom);
            anotherRoom.SetExit("south", startRoom);

            // Add rooms to the chapter's room list
            Rooms.Add(startRoom);
            Rooms.Add(anotherRoom);

            // Create Quests
            Quest findDataQuest = new Quest("Find Data", "Locate the missing data.");
            Quest solvePuzzleQuest = new Quest("Solve Puzzle", "Solve the puzzle in the lab.");

            // Create Tasks and associate them with quests
            Task findDataTask = new Task("Find Data Task", "Find the hidden data in the room.", findDataQuest,startRoom, FindDataTaskAction);
            Task solvePuzzleTask = new Task("Solve Puzzle Task", "Solve the tricky puzzle.", solvePuzzleQuest, anotherRoom ,SolvePuzzleTaskAction);

            // Add tasks to rooms
            startRoom.Tasks.Add(findDataTask);
            anotherRoom.Tasks.Add(solvePuzzleTask);

            // Add quests to the global quest list (if you have one)
            // allQuests.Add(findDataQuest);
            // allQuests.Add(solvePuzzleQuest);
        }

        // Define the actions for the tasks
        private bool FindDataTaskAction()
        {
            Console.WriteLine("You found the hidden data!");
            // Additional logic for completing the task
            return true;
        }

        private bool SolvePuzzleTaskAction()
        {
            Console.WriteLine("You solved the puzzle!");
            // Additional logic for completing the task
            return true;
        }
    }
}
