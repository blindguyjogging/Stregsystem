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
            Console.WriteLine("Hello World!");
        }
    }
}
