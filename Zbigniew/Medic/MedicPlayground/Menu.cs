using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MedicPlayground
{
    class Menu
    {
        private int SelectedOption;
        private string[] Option;
        private string Prompt;
        public Menu(string prompt, string[] option)
        {
            Prompt = prompt;
            Option = option; 
            SelectedOption = 0;

        }

        private void DisplayOptions()
        {
            Console.WriteLine(Prompt);
            for (int i = 0; i < Option.Length; i++)
            {
                string CurrentOption = Option[i];
                string prefix;

                if (i == SelectedOption)
                {
                    prefix = ">";
                }
                else
                {
                    prefix = " ";
                }

                Console.WriteLine($"{prefix} {CurrentOption}");
            }
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                //Update the SelectedOption by clicking the arrow up, or arrow down.

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedOption--;
                    if (SelectedOption == -1)
                    {
                        SelectedOption = Option.Length - 1;
                    }
                }
                else if(keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedOption++;
                    if (SelectedOption == Option.Length)
                    {
                        SelectedOption = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            return SelectedOption;
        }




    }
}
