using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeRising
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> 
        { "north", "east", "south", "west", "look", "back", "menu", "help",
         "quests","quest", "task", "tasks", "complete", "see", "do", "next"
         ,"chapter", "socialscore", "inventory","map"};

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
