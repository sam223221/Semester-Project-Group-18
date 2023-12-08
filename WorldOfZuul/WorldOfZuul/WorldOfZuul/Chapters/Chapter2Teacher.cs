using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Formats.Asn1;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using System.Xml;

namespace WorldOfZuul
{
    public class Chapter2Teacher : IChapter
    {
        public List<Room> Rooms { get; private set; }
        public List<Quest> Quests {get; set;}
        public bool IsCompleted { get; set; }
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

            theatre.SetExits(null, outside,office,null);

            pub.SetExits(null, null,class1,outside);

            office.SetExits(theatre,hallway1,canteen,null);

            class1.SetExits(pub,null,workshop,hallway1);
             
            hallway1.SetExits(outside, class1,hallway2,office);

            hallway2.SetExits(hallway1,workshop,null,canteen);

            canteen.SetExits(office,hallway2,null,null);

            workshop.SetExits(class1,null,null,hallway2);
        

            Item pen = new Item("pen" , "A beatiful pen given to you by your collegue","Help your collegue to by teaching he's students");
            Item screwdriver = new Item("screwdriver","A simple screwdriver","Workshop");
            Item hammer = new Item("hammer","A hammer nothing too speacial about it","Theatre");
            Item smartphone= new Item("smartphone","A smartphone that you found","Office");

            // Create Quests
            Quest Repairschool = new Quest("RepairTheSchool.","Repair the school");
            Quest Helpkids = new Quest("Helpkids ","Help the kids around the university");
            Quest Helpcolleagues = new Quest("Helpcolleagues","Help your colleagues");
            Quest LostSomething= new Quest("LostPhone","Someone lost something valuable");

            // Create Tasks and associate them with quests
            Task hallway1task = new Task("Kid","A kid is crying",Helpkids,hallway1,kidCrying,null,null);
            Task hallway2task = new Task("Colleguehelp","A collegue needs help",Helpcolleagues,hallway2,colleaguehelp,null,null);
            Task pubtask = new Task("PubInteraction","A student's parent recognizes you in the pub",Helpkids,pub,parents,null,null);
            Task outsidetask = new Task("Protest","There is a protest!",Helpkids,outside,protest,null,null);
            Task classtask = new Task("Geography","You have to help ypur gegraphy collegue",Helpcolleagues,class1,geography,null,pen);
            Task hallway2task2 = new Task("Lost","A kid is looking for something",Helpkids,hallway2,lostpen,pen,null);
            Task screwdriver1 = new Task("Screwdriver","A screwdriver",Repairschool,workshop,screwdriverfound,null,screwdriver);
            Task Hammer = new Task("Hammer","A hammer",Repairschool,theatre,hammerfound,screwdriver,hammer);
            Task canteenrepair = new Task("Canteenrepair","The canteen needs repairs",Repairschool,canteen,repairs,hammer,null);
            Task foundphone = new Task("Phone","You found a phone",LostSomething,office,lostphone,null,smartphone);
            Task givephone = new Task("GiveOrNot","Will you make the right choice?",LostSomething,pub,GiveOrNot,smartphone,null);
            // Add quest to Chapter list
            Quests.Add(Helpkids);
            Quests.Add(Helpcolleagues);
            Quests.Add(Repairschool);
            Quests.Add(LostSomething);

            // Add tasks to rooms
            hallway1.AddTask(hallway1task);
            hallway2.AddTask(hallway2task);
            hallway2.AddTask(hallway2task2);
            outside.AddTask(outsidetask);
            class1.AddTask(classtask);
            workshop.AddTask(screwdriver1);
            theatre.AddTask(Hammer);
            canteen.AddTask(canteenrepair);
            office.AddTask(foundphone);
            pub.AddTask(givephone);
            pub.AddTask(pubtask);

            // Add task to quest
            Helpkids.AddTask(hallway1task);
            Helpkids.AddTask(hallway2task);
            Helpkids.AddTask(pubtask);
            Helpkids.AddTask(hallway2task2);
            Helpkids.AddTask(foundphone);
            Helpcolleagues.AddTask(outsidetask);
            Helpcolleagues.AddTask(classtask);
            Repairschool.AddTask(Hammer);
            Repairschool.AddTask(screwdriver1);
            Repairschool.AddTask(canteenrepair);
            LostSomething.AddTask(foundphone);
            LostSomething.AddTask(givephone);
            
        }
            //Tasks
        private int kidCrying()
        {
            Printer.PrintLine("You encounter a student crying in the hallway.What will you do?");
             Printer.PrintLine($@"
            1.help
            2.ignore?");
            while(true)
            {
            string awnser = Console.ReadLine().ToLower();
            if (awnser == "1")
            {
                 Printer.PrintLine("The student appreciates your support.You gain a positive reputation. ");
                return 5;

            }
            else
            if(awnser=="2")
            {
                 Printer.PrintLine("The student feels ignored. Lose a bit of reputation.");
                return -5;
            }
            else
             Printer.PrintLine("Unknown command");
            }
        }
        private int colleaguehelp()
        {
            Printer.PrintLine("A colleague asks for your help with a task.Will you help him?");
            Printer.PrintLine($@"
            1.assist
            2.ignore?");
            while(true)
            {
            string awnser = Console.ReadLine().ToLower();
            if (awnser == "1")
            {
                Printer.PrintLine("The colleague is a new teacher and he needs advice on how to deal with the students");
                Printer.PrintLine("What will you tell him?");
                Printer.PrintLine( $@"
            1.That he is a bad teacher
            2.That every teacher has to deal with this problem and that he will get better as the time goes on
            3.That he should yell at the kids who don't listen to him
            4.That he he should show respect to the kids in order to be respected");
                awnser = Console.ReadLine().ToLower();
                if(awnser =="1")
                    {
                         Printer.PrintLine("The colleague is dissapointed,and he's thinking about quiting the job.");
                        return -20;
                    }
                else
                if(awnser =="2")
                {
                    Printer.PrintLine("The colleague is more confident now,thanks to you!");
                    return 5;
                }
                else
                if(awnser == "3")
                {
                     Printer.PrintLine("You gave the collegue bad advice");
                    return -10;
                }
                else
                if(awnser == "4")
                {
                     Printer.PrintLine("You gave your collegue a great advice");
                    return 10;
                }
                else
                {
                    Printer.PrintLine("Unknown Command");
                }

            }
            else
            if(awnser== "2")
            {
                 Printer.PrintLine("The collegue is dissapointed that you refused to help");
                return -5;
            }
            else
             Printer.PrintLine("Unknown Command");
            }
        }

        private int protest()
        {
            Printer.PrintLine(" Students are organizing a peaceful protest for a cause they believe in. What will you do?");
            Printer.PrintLine($@"
            1.Observe
            2.Ignore");
            while(true)
            {
            string awnser = Console.ReadLine().ToLower();
            if(awnser == "1")
            {
                Printer.PrintLine(" A fight broke out. What will you do?");
                Printer.PrintLine($@"
            1.Stop the fight
            2.Ignore");
                while(true)
                {
                awnser = Console.ReadLine().ToLower();
                if(awnser == "1") 
                {
                    Printer.PrintLine("You stop the fight!");
                    return 10;
                }
                else
                if(awnser == "2")
                {
                    Printer.PrintLine("The fight was stopped by other teacher,you lose repect in front of the people!");
                    return -10;
                }
                else
                {
                    Printer.PrintLine("Unknown Command");
                    
                }
                }

            }
            else if(awnser=="2")
            {
                Printer.PrintLine("You didn't do your duty...");
                return -10;
            }
            else 
            {
                Printer.PrintLine("Unknown Command");
            
            }
            }
        }
        private int geography()
        {
             Printer.PrintLine("Your collegue request your help,he won't be able to attend he's lecture,will you attend the lecture in he's place?");
             Printer.PrintLine($@"
            1.Yes
            2.No");
            while (true)
            {
            string awnser = Console.ReadLine().ToLower();
            if(awnser=="1")
            {
                Printer.PrintLine("You have to teach the students geography!");
                Printer.PrintLine($@"What is the capital of Germany?
            1.Berlin
            2.Paris
            3.Moscow
            4.London");
                awnser = Console.ReadLine().ToLower();
                if(awnser == "1")
                {
                     Printer.PrintLine("Correct!");
                     Printer.PrintLine($@"What is the biggest country on earth?
            1.China
            2.Russia
            3.USA
            4.UK
                ");
                 awnser = Console.ReadLine().ToLower();
                 if(awnser == "2")
                 {
                     Printer.PrintLine("Correct!");
                     Printer.PrintLine($@"How many continents are there?
            1.7
            2.8
            3.6
            4.4");
                     awnser = Console.ReadLine().ToLower();
                     if(awnser == "1")
                     {
                         Printer.PrintLine("Correct");   
                         Printer.PrintLine("In which continent is USA located?");
                         Printer.PrintLine($@"
            1.North America
            2.South America
            3.Asia
            4.Africa");
                    awnser = Console.ReadLine().ToLower();
                    if(awnser=="1")
                    {
                        Printer.PrintLine("Good job.You were able to help your collegue.In a show of gratitude your collegue rewards you with he's pen");
                        return 30;
                    }
                    else
                    return -2;

                    }
                     else
                     {
                        Printer.PrintLine("Wrong");
                        return -3;
                     }

                 }
                 else
                 {
                     Printer.PrintLine("Wrong");
                    return -5;
                 }

                }
                else
                {
                     Printer.PrintLine("Wrong!");
                    return -10;
                }            
            }
            else
            {
                 Printer.PrintLine("Unknown command");
            }
         }
        }

        
        private int lostpen()
        {
            Printer.PrintLine("A kid lost his pen and it's looking for it,He needs a pen to fill in some papers.Do you let him borrow your pen?");
             Printer.PrintLine($@"
        1.Yes
        2.No");
            while(true)
            {
            string awnser = Console.ReadLine().ToLower();

                if(awnser == "1")
                    {
                         Printer.PrintLine("The kid thanks you.You gain the respect of the child , and teach him that you should always help the ones in need");
                        return 10;
                    }
                else if (awnser == "2")
                {
                     Printer.PrintLine("The kid stars crying.You could have helped him...");
                    return -10;
                }
                else
                 Printer.PrintLine("Unknown Command");
            }


        }

        private int hammerfound()
        {
             Printer.PrintLine("A worker needs a screwdriver for 5 minutes.Do you give him your screwdriver?");
             Printer.PrintLine(@$"
        1.Yes
        2.No
            ");
            string awnser = Console.ReadLine().ToLower();
            while(true)
            {
                if(awnser=="1")
                {
                     Printer.PrintLine("He thanks you and rewards you with a hammer");
                    return 10;
                }
                else
                if(awnser=="2")
                return -10;
                else
                 Printer.PrintLine("Unknown Command");
            }

        }
        private int screwdriverfound()
        {
            Printer.PrintLine("You see a screwdriver");
             Printer.PrintLine(@$"
        1.Take it
            ");
            while(true)
            {
                string awnser = Console.ReadLine().ToLower();
                if(awnser=="1")
                return 0;
                else
                 Printer.PrintLine("Unknown Command");
            }
        }
        private int repairs()
        {
             Printer.PrintLine("The canteen needs repairs,a table is broken, and there is no one that knows how to fix it.You have worked as a construction worker before becomind a teacher,will you help?");
             Printer.PrintLine(@$"
        1.Yes
        2.No
             ");
            while(true)
            {
                 string awnser = Console.ReadLine().ToLower();
                 if(awnser== "1")
                 {
                    Printer.PrintLine("You were able to fix it,the kids thank you for doing something that wasn't even supposed to be done by you!");
                    return 20;
                 }
                 else
                if(awnser=="2")
                {
                    Printer.PrintLine("You just ignore the table.While fixing the table wasn't your job, you could have helped!");
                    return -5;
                }

            }
        }
        private int parents()
        {
            Printer.PrintLine("A student's parent recognizes you in the pub.");
             Printer.PrintLine(@$"
        Approach the parent or avoid interaction?
        1.Approach
        2.Avoid
             ");
             while(true)
             {
                string awnser = Console.ReadLine().ToLower();
                if(awnser=="1")
                {
                    Printer.PrintLine("Approach the parent, exchange pleasantries, and potentially enhance your relationship with parents.");
                    Printer.PrintLine("The parents ask you how is their child doing in school");
                    Printer.PrintLine(@$"
        Tell them that:
        1.The child is doing great
        2.The child is doing bad");
                while(true)
                {
                    awnser = Console.ReadLine().ToLower();
                    if(awnser=="1")
                    {
                        Printer.PrintLine("The child is very happy that you told he's parents that he's a good student");
                        return 10;
                    }
                    else
                    if(awnser=="2")
                    {
                        Printer.PrintLine("The child is very sad and you lose respect in front of he's eyes");
                        return -10;
                    }
                    else
                    {
                        Printer.PrintLine("Unknown Command");
                    }
                }
                }
                else
                if(awnser=="2")
                {
                    Printer.PrintLine("Avoid interaction to maintain privacy. Preserve personal space but potentially miss an opportunity to strengthen parent-teacher relations.");
                    return 0;
                }
                else
                {
                    Printer.PrintLine("Unknown command");
                }

             }
        }
        private int lostphone()
        {
            Printer.PrintLine("You found a smartphone some kid might have lost it.You should take it and find the kid that lost it");
            Printer.PrintLine(@$"
        1.Take it
            ");
            while(true)
            {
            string awnser = Console.ReadLine().ToLower();
            if(awnser=="1")
            {
                Printer.PrintLine("You took the phone");
                return 0;
            }
            else 
                Printer.PrintLine("Unknown command");
            }

        }
        private int GiveOrNot()
        {
            Printer.PrintLine("A kid comes to you,and says that the phone that you found in the office is he's phone");
            Printer.PrintLine(@$"
        What will you do?
        1.Give he's phone back
        2.Ask where he lost he's phone
            "); 
        while(true)
        {
         string awnser = Console.ReadLine().ToLower();
         if(awnser=="1")
         {
            Printer.PrintLine("Immediately after you gave him the phone a kid comes to you and tells you that he lost his phone in the office.You made the wrong decision...");
            return -10;
         }
         if(awnser=="2")
         {
            Printer.PrintLine("He said the he lost his phone in the workshop");
            Printer.PrintLine(@$"
        What will you do?
        1.Give him the phone
        2.Tell him that thats not he's phone and that you won't give it to him
            ");
        while(true)
        {
         awnser = Console.ReadLine().ToLower();
         if(awnser=="1")
         {
            Printer.PrintLine("It wasn't his phone, you should have paid more attention...");
            return -10;
         }
         else
         if(awnser=="2")
         {
            Printer.PrintLine("You did the right choice the phone is in good hands until the kid that lost it will ask for back for it");
            return 10;
         }
         else
        Printer.PrintLine("Unknown command");

        }

         }
         else
         Printer.PrintLine("Unknown command");
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
            string workshop     = "     ";

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
          │   {theatre}   │   │  {outside}    │  │     {pub}  │
          └───┬───────┘   └───┬───────┘  └───┬────────┘
              │               │              │
          ┌───┴───────┐   ┌───┴────────┐  ┌──┴─────────┐
          │           │   │            │  │            │    
          │  Office   │───┤  Hallway1  ├──┤   Class    │  
          │    {office}  │   │   {hallway1}    │  │      {class1} │
          └───┬───────┘   └───┬────────┘  └──┬─────────┘
              │               │              │
          ┌───┴───────┐   ┌───┴───────┐  ┌───┴────────┐
          │           │   │           │  │            │        
          │  Canteen  ├───┤  Hallway2 ├──┤  Workshop  │
          │    {canteen}  │   │   {hallway2}   │  │    {workshop}   │
          └───────────┘   └───────────┘  └────────────┘
                        ";

            Console.WriteLine(map);
        }
    }
}