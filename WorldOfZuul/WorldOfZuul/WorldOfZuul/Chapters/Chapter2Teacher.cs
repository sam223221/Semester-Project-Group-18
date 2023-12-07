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
        private Room? library;

        public Chapter2Teacher()
        {
            Rooms = new List<Room>();
            Quests = new List<Quest>();

        }

        public string PlayerScore(){
            return "Your score is {} ";
        }

        public Room GetStartRoom() => outside;

                public void ShowIntroduction()
        {

        }

        public void CreateRoomsAndQuests()
        {
            // Create Rooms
    
            class1      = new("Class1"           ,"You enter the classroom, a space of learning and academic engagement. The air is charged with the anticipation of knowledge, and the scent of chalk dust lingers in the room. Rows of desks face the front, each accompanied by a chair and a neatly organized set of course materials.");

            outside     = new("Outside"         ,"You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");

            theatre     = new("Theatre"         ,"You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");

            pub         = new("Pub"             ,"You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");

            hallway1    = new("Hallway1"         ,"You find yourself in a bustling university hallway, a thoroughfare of academic energy.The scent of freshly printed paper and the distant hum of discussions fill the air.Students rush past, notebooks in hand, absorbed in their own worlds of knowledge.");

            office      = new("Office"          ,"You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");

            canteen     = new("Canteen"         ,"You step into the bustling canteen, and the aroma of sizzling meats and hearty soups immediately envelops you.The room is alive with the chatter of fellow adventurers, their laughter mingling with the clinking of mugs and plates.");

            hallway2    = new("Hallway2"         ,"You find yourself in a bustling university hallway, a thoroughfare of academic energy.The scent of freshly printed paper and the distant hum of discussions fill the air.Students rush past, notebooks in hand, absorbed in their own worlds of knowledge.");

            library     = new("Library"         ,"You enter the library, a sanctuary of knowledge and quiet contemplation. The air is hushed, and the scent of aged paper mingles with the subtle aroma of leather-bound books. The vast room is lined with towering shelves, each holding the accumulated wisdom of countless authors and scholars.");
            
            
            outside.SetExits(null, pub, hallway1, theatre); // North, East, South, West

            theatre.SetExits(null, null,office,outside);

            pub.SetExits(null, outside,class1,null);

            office.SetExits(theatre,null,office,null);

            class1.SetExits(pub,null,library,hallway1);
             
            hallway1.SetExits(outside, office,hallway2,class1);

            hallway2.SetExits(hallway1,library,null,null);

            canteen.SetExits(office,null,null,null);

            library.SetExits(class1,null,null,hallway2);
        

            // Add rooms to the chapter's room list
            Rooms.AddRange(new List<Room>() {outside,class1,library,canteen,hallway2,hallway1,office,pub,theatre});

            Item pen = new Item("pen" , "a pen");
            // Create Quests
            Quest Helpkids = new Quest("Helpkids ","Help the kids around the university");
            Quest Helpcolleagues = new Quest("Helpcolleagues","Help your colleagues");
           // Quest solvePuzzleQuest = new Quest("Solve Puzzle", "Solve the puzzle in the lab.");
            
            // Create Tasks and associate them with quests
            //Task findDataTask = new Task("Find Data Task", "Find the hidden data in the room.", findDataQuest,startRoom, FindDataTaskAction);
            //Task solvePuzzleTask = new Task("Solve Puzzle Task", "Solve the tricky puzzle.", solvePuzzleQuest, anotherRoom ,SolvePuzzleTaskAction);
            Task hallway1task = new Task("Kid is crying","A kid is crying",Helpkids,hallway1,kidCrying,null,null);
            Task hallway2task = new Task("Help needed","A collegue needs help",Helpcolleagues,hallway2,colleaguehelp,null,null);

            // Add quest to Chapter list
            //Quests.Add(findDataQuest);
            //Quests.Add(solvePuzzleQuest);
            Quests.Add(Helpkids);
            Quests.Add(Helpcolleagues);
            

            // Add tasks to rooms
            //startRoom.AddTask(findDataTask);
            hallway1.AddTask(kidCrying);
            hallway2.AddTask(colleaguehelp);
            //anotherRoom.AddTask(solvePuzzleTask);

            // Add task to quest
            //findDataQuest.AddTask(findDataTask);
            //solvePuzzleQuest.AddTask(solvePuzzleTask);

        }

        private int kidCrying()
        {
            Console.WriteLine("You encounter a student crying in the hallway.What will you do?");
            Console.WriteLine("help/ignore?");
            string awnser = Console.ReadLine().ToLower();
            if (awnser == "help")
            {
                Console.WriteLine("The student appreciates your support.You gain a positive reputation. ");
                return 5;

            }
            else
            {
                Console.WriteLine("The student feels ignored. Lose a bit of reputation.");
                return -5;
            }
        }
        private int colleaguehelp()
        {
            Console.WriteLine("A colleague asks for your help with a task.Will you help him?");
            Console.WriteLine("assist/ignore?");
            string awnser = Console.ReadLine().ToLower();
            if (awnser == "assist")
            {
                Console.WriteLine("The colleague is a new teacher and he needs advice on how to deal with the students");
                Console.WriteLine("What will you tell him?");
                Console.WriteLine( $@"1.That he is a bad teacher
                                      2.That every teacher has to deal with this problem and that he will get better as the time goes on
                                      3.That he should yell at the kids who don't listen to him
                                      4.That he he should show respect to the kids in order to be respected");
                awnser = Console.ReadLine().ToLower();
                if(awnser =="1")
                    {
                        Console.WriteLine("The colleague is dissapointed,and he's thinking about quiting he's job.");
                        return -20;
                    }
                else
                if(awnser =="2")
                {
                    Console.WriteLine("The colleague is more confident now,thanks to you!");
                    return 5;
                }
                else
                if(awnser == "3")
                {
                    Console.WriteLine("You gave the collegue bad advice");
                    return -10;
                }
                else
                if(awnser == "4")
                {
                    Console.WriteLine("You gave your collegue a great advice");
                    return 10;
                }
                else
                {
                    Console.WriteLine("The collegue is dissapointed that you refused to help");
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("The collegue is dissapointed that you refused to help");
                return -5;
            }
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
       public void showMap(Room currentRoom)
        {
            string class1       = "     ";
            string hallway1     = "     ";
            string hallway2     = "     ";
            string pub          = "     ";
            string outside      = "     ";
            string canteen      = "     ";
            string theatre      = "     ";
            string office       = "     ";
            string library      = "     ";

            // Mark the current room
            switch (currentRoom.ShortDescription)
            {
                case "Class1":
                    class1 = "*You*";
                    break;
                case "Theatre":
                    theatre = "*You*";
                    break;
                case "Outside":
                    outside = "*You*";
                    break;
                case "Pub":
                    pub = "*You*";
                    break;
                case "Hallway1":
                    hallway1 = "*You*";
                    break;
                case "Hallway2":
                    hallway2 = "*You*";
                    break;
                case "Office":
                    office = "*You*";
                    break;
                case "Canteen":
                    canteen = "*You*";
                    break;
                case "Library":
                    library = "*You*";
                    break;
            }

            string map = $@"
          ┌───────────┐   ┌───────────┐  ┌────────────┐
          │           │   │           │  │            │
          │  Theatre  │───│  Outside  ├──┤    Pub     │
          │ {theatre}     │   │{outside}      │  │   {pub}    │
          └───┬───────┘   └───┬───────┘  └───┬────────┘
              │               │              │
          ┌───┴───────┐   ┌───┴────────┐  ┌──┴─────────┐
          │           │   │            │  │            │    
          │  Office   │───┤  Hallway1  ├──┤   Class    │  
          │  {office}    │   │ {hallway1}      │  │  {class1}     │
          └───┬───────┘   └───┬────────┘  └──┬─────────┘
              │               │              │
          ┌───┴───────┐   ┌───┴───────┐  ┌───┴────────┐
          │           │   │           │  │            │        
          │  Canteen  ├───┤  Hallway2 ├──┤  Library   │
          │  {canteen}    │   │ {hallway2}     │  │ {library}      │
          └───────────┘   └───────────┘  └────────────┘
                        ";

            Console.WriteLine(map);
        }
    }
}
