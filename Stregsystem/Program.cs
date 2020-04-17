using Stregsystem;
using System;
using System.Text.RegularExpressions;
//20184639_Martin_Opal_Lykkegaard
// in this project is also located a readme file, if curious about my own thought of this project, feel free to read, it isent very long!
namespace Stregsystem
{
    class Program
    {
        static void Main(string[] args)
        {
            PointSystem system = new PointSystem();
            StregsystemCLI ui = new StregsystemCLI(system);
            StregsystemCommandParser control = new StregsystemCommandParser(system,ui);
            ui.Start();
        }
    }
}
