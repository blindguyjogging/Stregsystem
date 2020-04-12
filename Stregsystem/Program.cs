using Stregsystem;
using System;
using System.Text.RegularExpressions;
//20184639_Martin_Opal_Lykkegaard
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
            //ui.Close();
        }
    }
}
