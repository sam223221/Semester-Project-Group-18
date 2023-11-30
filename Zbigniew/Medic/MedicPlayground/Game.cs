using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MedicPlayground
{
    class Game
    {
        public void Stop()
        {

        }


        public void Play()
        {

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            TextArts textArts = new TextArts();
            Console.WriteLine(textArts.ChapterTextArt);

            Console.Write
                 ("\nBefore you start playing turn on fullscreen\n"+
                 "\nHello Brave Adventurer! In Chapter III, you're the important Medic character. You're not just playing a game... " +
                 "\nYou're a healer, life saver and you are bringing hope to your friends and whole village." +
                 "\nGood luck, Medic! I hope you will save lots of lives and win many battles!" +
                 "\nFirstly you need to choose your hero name.\n");

            string hero_name = GetHeroName();

            Console.WriteLine("\nHello " + hero_name + "! This name is perfect. Now let's begin journey!");

            Console.WriteLine("\nPress any key to continue...");

            PressKey();

            Console.WriteLine(textArts.MedicTextArt);

            PressKey();

            string prompt = $"{textArts.GameMenu}";
            string[] option = { "New Medic", "Village", "Hospital", "Forest" };

            Menu mainMenu = new Menu(prompt, option);
            mainMenu.Run();
            int SelectedOption = mainMenu.Run();

            switch(SelectedOption)
            {
                case 0:
                    Console.Clear();
                    Play();
                    break;
                case 1:
                    Console.Clear();
                    Village();
                    break;
                case 2:
                    Console.Clear();
                    Hospital();
                    break;
                case 3:
                    Console.Clear();
                    Forest();
                    break;
            }


            Console.ReadKey();
        }

        //Forest chapter
        public static void Forest()
        {
            TextArts textArts = new TextArts();
            Console.WriteLine($"\n{textArts.Forest}\n");

            PrintText("\nWelcome to the forest! It's peaceful here," +
                "\nwith the sound of leaves and the fresh smell of the earth. " +
                "\nSunlight peeks through the trees, creating a calm atmosphere." +
                "\nYou're on a mission to find medicinal herbs. " +
                "\nThey're scattered around. Listen to the birds and feel the breeze as you explore. " +
                "\nThe villagers are counting on you to bring back healing herbs. " +
                "\nEnjoy your journey through the forest, and good luck, caring medic!");

            Console.ReadKey();
            Console.Clear();

            Forest forest = new Forest();
            forest.Main();

        }

        //Hospital chapter
        public static void Hospital()
        {
            TextArts textArts = new TextArts();
            Console.Write($"\n{textArts.Hospital}\n");

            PrintText("Something will be there");
        }        
        
        //Village chapter
        public static void Village()
        {
            TextArts textArts = new TextArts();
            Console.Write($"\n{textArts.Village}\n");
            
            PrintText("\nWelcome Medic, our village lacks health care and basic medicines. " +
                "\nEveryone of the citizens really appreciate your sacrifice and dedication to help us live normal. " +
                "\nWe hope that your work will bring relief to those suffering, and your skills will help us overcome health difficulties." +
                "\r\nThank you hero.");
            
            Console.WriteLine("\nPress any key to continue...\n");
        }

        //Method that setup a hero name
        public static string GetHeroName()
        {
            String hero_name;

            do
            {
                Console.Write
                ("\nName of your hero must consist minimum 3 and a maximum 9 characters...");
                Console.Write
                ("\nNow to begin your journey, type in your hero name: ");

                hero_name = Console.ReadLine();

                if (string.IsNullOrEmpty(hero_name) || hero_name.Length < 3 || hero_name.Length > 9)
                {
                    Console.Write("\nType name correctly, please... \nRemember the requirements are 3-9 characters...\n");
                }
            } while (string.IsNullOrEmpty(hero_name) || hero_name.Length < 3 || hero_name.Length > 9);

            return hero_name;
        }

        //Clear console when any key is pressed.
        public static void PressKey()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            Console.Clear();
        }

        //Printing text animation
        public static void PrintText(string text, int speed = 40)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            Console.WriteLine();
        }


    }
}

