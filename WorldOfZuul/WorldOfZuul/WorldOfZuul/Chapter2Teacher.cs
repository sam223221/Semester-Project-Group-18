using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldOfZuul
{
    public class Chapter2Teacher : IChapter
    {
        public List<Room> Rooms { get; private set; }
        public List<Quest> Quests {get; set;}

        private Room? class1;
        private Room? outside;
        private Room? theatre;
        private Room? pub;
        private Room? hallway1;
        private Room? office;
        private Room? canteen;
        private Room? hallway2;
        private Room? hallway3;
        private Room? workshop;
        private Room? lab;
        private Room? library;
        private Room? park;


        public Chapter2Teacher()
        {
            Rooms = new List<Room>();
            Quests = new List<Quest>();
            CreateRoomsAndQuests();
            ShowIntroduction();
        }

        public Room GetStartRoom() => outside;

                public void ShowIntroduction()
        {

        }

        public void CreateRoomsAndQuests()
        {
            // Create Rooms
    
            class1 = new("Class", "You enter the classroom, a space of learning and academic engagement. The air is charged with the anticipation of knowledge, and the scent of chalk dust lingers in the room. Rows of desks face the front, each accompanied by a chair and a neatly organized set of course materials.");

            outside = new("Outside", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");

            theatre = new("Theatre", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");

            pub = new("Pub", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");

            hallway1 = new("Hallway", "You find yourself in a bustling university hallway, a thoroughfare of academic energy.The scent of freshly printed paper and the distant hum of discussions fill the air.Students rush past, notebooks in hand, absorbed in their own worlds of knowledge.");

            office = new("Office", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");

            canteen = new("Canteen","You step into the bustling canteen, and the aroma of sizzling meats and hearty soups immediately envelops you.The room is alive with the chatter of fellow adventurers, their laughter mingling with the clinking of mugs and plates.");

            hallway2 = new("Hallway","You find yourself in a bustling university hallway, a thoroughfare of academic energy.The scent of freshly printed paper and the distant hum of discussions fill the air.Students rush past, notebooks in hand, absorbed in their own worlds of knowledge.");

            hallway3 = new("Hallway","You find yourself in a bustling university hallway, a thoroughfare of academic energy.The scent of freshly printed paper and the distant hum of discussions fill the air.Students rush past, notebooks in hand, absorbed in their own worlds of knowledge.");

            workshop = new("Workshop","You enter the engineering workshop, a realm of creativity and innovation. The air is filled with the scent of metal, the faint whir of machinery, and the occasional sizzling sound of welding. The workshop is a symphony of activity, with engineers and students huddled over workbenches, sketching designs, and fine-tuning intricate machinery.");

            lab = new("Laboratory", "You step into the laboratory, a realm of scientific exploration and discovery. The air is crisp, carrying the faint scent of chemicals and the sterile environment of controlled experimentation. Fluorescent lights overhead illuminate rows of lab benches, each adorned with glassware, instruments, and the promise of groundbreaking research.");

            library = new("Library","You enter the library, a sanctuary of knowledge and quiet contemplation. The air is hushed, and the scent of aged paper mingles with the subtle aroma of leather-bound books. The vast room is lined with towering shelves, each holding the accumulated wisdom of countless authors and scholars.");

            park = new("University Park", "You venture into the university park, a serene oasis nestled within the academic hustle and bustle. The air is filled with the soothing rustle of leaves and the distant melody of birdsong. Tall trees stand sentinel, their branches creating a natural canopy that provides shade for those seeking refuge from the sun.");
            
            
            outside.SetExits(park, pub, hallway1, theatre); // North, East, South, West

            theatre.SetExits(null, null,office,outside);

            pub.SetExits(null, outside,class1,null);

            lab.SetExits(library, hallway3, null, null);

            office.SetExits(theatre,null,office,null);

            class1.SetExits(pub,null,library,hallway1);
             
            hallway1.SetExits(outside, office,hallway2,class1);

            hallway2.SetExits(hallway1,library,hallway3,workshop);

            hallway3.SetExits(hallway2,lab,null,workshop);

            workshop.SetExits(canteen,null,null,hallway3);

            canteen.SetExits(office,null,workshop,null);

            lab.SetExits(library,hallway3,null,null);

            library.SetExits(class1,null,lab,hallway2);
        

            // Add rooms to the chapter's room list
            Rooms.AddRange(new List<Room>() {outside,class1,lab,library,canteen,hallway3,hallway2,hallway1,workshop,office,pub,theatre});


            // Create Quests
            Quest findDataQuest = new Quest("Find Data", "Locate the missing data.");
            Quest solvePuzzleQuest = new Quest("Solve Puzzle", "Solve the puzzle in the lab.");

            // Create Tasks and associate them with quests
            //Task findDataTask = new Task("Find Data Task", "Find the hidden data in the room.", findDataQuest,startRoom, FindDataTaskAction);
            //Task solvePuzzleTask = new Task("Solve Puzzle Task", "Solve the tricky puzzle.", solvePuzzleQuest, anotherRoom ,SolvePuzzleTaskAction);


            // Add quest to Chpter list
            //Quests.Add(findDataQuest);
            //Quests.Add(solvePuzzleQuest);

            // Add tasks to rooms
            //startRoom.AddTask(findDataTask);
            //anotherRoom.AddTask(solvePuzzleTask);

            // Add task to quest
            //findDataQuest.AddTask(findDataTask);
            //solvePuzzleQuest.AddTask(solvePuzzleTask);

        }

        // 
        private int FindDataTaskAction()
        {
            Console.WriteLine("You found the hidden data!");
            // Additional logic for completing the task
            return 5;
        }

        private int SolvePuzzleTaskAction()
        {
            Console.WriteLine("You solved the puzzle!");
            // Additional logic for completing the task
            return 10;
        }
    }
}
