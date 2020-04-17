//20184639_Martin_Opal_Lykkegaard
// in this project is also located a readme file, if curious about my own thought of this project, feel free to read, it isent very long!
namespace Stregsystem
{
    class Program
    {
        static void Main(string[] args)
        {
            PointSystem pointsystem = new PointSystem();
            StregsystemCLI ui = new StregsystemCLI(pointsystem);
            StregsystemCommandParser control = new StregsystemCommandParser(pointsystem, ui);
            ui.Start();
        }
    }
}
