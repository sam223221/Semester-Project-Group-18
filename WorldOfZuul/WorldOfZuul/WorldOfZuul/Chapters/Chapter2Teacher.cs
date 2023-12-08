using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks.Dataflow;

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
        private Room? workshop;

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
            Console.WriteLine(TextArtManager.GetTextArt("TeacheraCharacter"));
            Console.ReadKey();
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

            workshop     = new("Workshop"         ,"You enter the engineering workshop, a realm of creativity and innovation. The air is filled with the scent of metal, the faint whir of machinery, and the occasional sizzling sound of welding. The workshop is a symphony of activity, with engineers and students huddled over workbenches, sketching designs, and fine-tuning intricate machinery.");
            
            // Add rooms to the chapter's room list

            Rooms.AddRange(new List<Room>() {outside,class1,workshop,canteen,hallway2,hallway1,office,pub,theatre});

            outside.SetExits(null, pub, hallway1, theatre); // North, East, South, West

            theatre.SetExits(null, null,office,outside);

            pub.SetExits(null, outside,class1,null);

            office.SetExits(theatre,hallway1,canteen,null);

            class1.SetExits(pub,null,workshop,hallway1);
             
            hallway1.SetExits(outside, office,hallway2,class1);

            hallway2.SetExits(hallway1,workshop,null,canteen);

            canteen.SetExits(office,hallway2,null,null);

            workshop.SetExits(class1,null,null,hallway2);
        

            Item pen = new Item("pen" , "A beatiful pen given to you by your collegue","Help your collegue");
            // Create Quests
            Quest solvePuzzleQuest = new Quest("Solve Puzzle", "Solve the puzzle in the lab.");
            Quest Helpkids = new Quest("Helpkids ","Help the kids around the university");
            Quest Helpcolleagues = new Quest("Helpcolleagues","Help your colleagues");
    
           // Quest solvePuzzleQuest = new Quest("Solve Puzzle", "Solve the puzzle in the lab.");
            
            // Create Tasks and associate them with quests
            //Task findDataTask = new Task("Find Data Task", "Find the hidden data in the room.", findDataQuest,startRoom, FindDataTaskAction);
            //Task solvePuzzleTask = new Task("Solve Puzzle Task", "Solve the tricky puzzle.", solvePuzzleQuest, anotherRoom ,SolvePuzzleTaskAction);
            Task hallway1task = new Task("Kid","A kid is crying",Helpkids,hallway1,kidCrying,null,null);
            Task hallway2task = new Task("Colleguehelp","A collegue needs help",Helpcolleagues,hallway2,colleaguehelp,null,null);
            Task outsidetask = new Task("Protest","There is a protest!",Helpkids,outside,protest,null,null);
            Task classtask = new Task("Geography","You have to help ypur gegraphy collegue",Helpcolleagues,class1,geography,null,pen);
            Task hallway2task2 = new Task("Lost","A kid is looking for something",Helpkids,hallway2,lostpen,pen,null);

            // Add quest to Chapter list
            //Quests.Add(findDataQuest);
            //Quests.Add(solvePuzzleQuest);
            Quests.Add(Helpkids);
            Quests.Add(Helpcolleagues);

            // Add tasks to rooms
            //startRoom.AddTask(findDataTask);
            hallway1.AddTask(hallway1task);
            hallway2.AddTask(hallway2task);
            hallway2.AddTask(hallway2task2);
            outside.AddTask(outsidetask);
            class1.AddTask(classtask);
            //anotherRoom.AddTask(solvePuzzleTask);

            // Add task to quest
            //findDataQuest.AddTask(findDataTask);
            Helpkids.AddTask(hallway1task);
            Helpkids.AddTask(hallway2task);
            Helpcolleagues.AddTask(outsidetask);
            Helpcolleagues.AddTask(classtask);
            Helpkids.AddTask(hallway2task2);

            //solvePuzzleQuest.AddTask(solvePuzzleTask);

        }
                                                //Choice quests
        private int kidCrying()
        {
            Console.WriteLine("You encounter a student crying in the hallway.What will you do?");
            Console.WriteLine($@"
            1.help
            2.ignore?");
            while(true)
            {
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
        }
        private int colleaguehelp()
        {
            Console.WriteLine("A colleague asks for your help with a task.Will you help him?");
            Console.WriteLine($@"
            1.assist
            2.ignore?");
            while(true)
            {
            string awnser = Console.ReadLine().ToLower();
            if (awnser == "1")
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
                    Console.WriteLine("Unknown Command");
                }

            }
            else
            if(awnser== "2")
            {
                Console.WriteLine("The collegue is dissapointed that you refused to help");
                return -5;
            }
            else
            Console.WriteLine("Unknown Command");
            }
        }

        private int protest()
        {
            Console.WriteLine(" Students are organizing a peaceful protest for a cause they believe in. What will you do?");
            Console.WriteLine($@"
            1.Observe
            2.Ignore");
            while(true)
            {
            string awnser = Console.ReadLine().ToLower();
            if(awnser == "1")
            {
                Console.WriteLine(" A fight broke out. What will you do?");
                Console.WriteLine($@"
                                    1.Stop the fight
                                    2.Ignore");
                awnser = Console.ReadLine().ToLower();
                if(awnser == "1") 
                {
                    Console.WriteLine("You stop the fight!");
                    return 10;
                }
                else
                if(awnser == "2")
                {
                    Console.WriteLine("The fight was stopped by other teacher,you lose repect in front of the people!");
                    return -10;
                }
                else
                {
                    Console.WriteLine("Unknown Command");
                    
                }


            }
            else if(awnser=="2")
            {
                Console.WriteLine("You didn't do your duty");
                return -10;
            }
            else 
            {
                Console.WriteLine("Unknown Command");
            
            }
            }
        }
        private int geography()
        {
            Console.WriteLine("Your collegue request your help,he won't be able to attend he's lecture,will you attend the lecture in he's place?");
            Console.WriteLine($@"1.Yes
                                 2.No");
            while (true)
            {
            string awnser = Console.ReadLine().ToLower();
            if(awnser=="1")
            {
                Console.WriteLine("You have to teach the students geography!");
                Console.WriteLine($@"What is the capital of Germany?
                                     1.Berlin
                                     2.Paris
                                     3.Moscow
                                     4.London");
                awnser = Console.ReadLine().ToLower();
                if(awnser == "1")
                {
                    Console.WriteLine("Correct!");
                    Console.WriteLine($@"What is the biggest country on earth?
                1.China
                2.Russia
                3.USA
                4.UK
                ");
                 awnser = Console.ReadLine().ToLower();
                 if(awnser == "2")
                 {
                    Console.WriteLine("Correct!");
                    Console.WriteLine($@"How many continents are there?
                    1.7
                    2.8
                    3.6
                    4.4");
                     awnser = Console.ReadLine().ToLower();
                     if(awnser == "1")
                     {
                        Console.WriteLine("Correct");   
                        Console.WriteLine("In which continent is USA located?");
                        Console.WriteLine($@"
                    1.North America
                    2.South America
                    3.Asia
                    4.Africa");
                    awnser = Console.ReadLine().ToLower();
                    if(awnser=="1")
                    {
                        Console.WriteLine("Good job.You were able to help your collegue.In a show of gratitude your collegue rewards you with he's pen");
                        return 30;
                    }
                    else
                    return -2;

                    }
                     else
                     {
                        Console.WriteLine("Wrong");
                        return -3;
                     }

                 }
                 else
                 {
                    Console.WriteLine("Wrong");
                    return -5;
                 }

                }
                else
                {
                    Console.WriteLine("Wrong!");
                    return -10;
                }            
            }
            else
            {
                Console.WriteLine("Unknown command");
            }
         }
        }

        
        private int lostpen()
        {
            Console.WriteLine("A kid lost hes pen and it's looking for it.Do you you give him a pen?");
            Console.WriteLine($@"
            1.Yes
            2.No");
            while(true)
            {
            string awnser = Console.ReadLine().ToLower();

                if(awnser == "1")
                    {
                        Console.WriteLine("The kid thanks you");
                        return 10;
                    }
                else if (awnser == "2")
                {
                    Console.WriteLine("The kid stars crying");
                    return -5;
                }
                else
                Console.WriteLine("Unknown Command");
            }


        }
        // 
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
            string workshop      = "     ";

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
                case "Workshop":
                    workshop = "*You*";
                    break;
            }
            // The map
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
          │  Canteen  ├───┤  Hallway2 ├──┤  Workshop  │
          │  {canteen}    │   │ {hallway2}     │  │ {workshop}      │
          └───────────┘   └───────────┘  └────────────┘
                        ";

            Console.WriteLine(map);
        }
    }
}