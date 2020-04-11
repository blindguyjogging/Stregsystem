using Stregsystem.LoadFiles;
using System;
using System.Text.RegularExpressions;
//20184639_Martin_Opal_Lykkegaard
namespace Stregsystem
{
    class Program
    {
        static void Main(string[] args)
        {
            FileLoader loader = new FileLoader();
            loader.ProductLoader();
            loader.UserLoader();
            StregsystemCLI.Runner();
            Console.WriteLine("Hello World!");
        }
    }
}
