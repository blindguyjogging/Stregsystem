using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Terminal.Gui;

namespace Stregsystem.LoadFiles
{
    public class FileLoader
    {
        public void ProductLoader()
        {
            string path = Path.Combine( Logger.basefolder, "LoadFiles\\Catalogue.txt");
            
            using (StreamReader sr = new StreamReader(path)) // Will search through file, loading 3 comma seperated words per. line
            {
                sr.ReadLine(); // Consume description line

                while (!sr.EndOfStream) 
                {
                    string[] product = new string[5];

                    string pattern = "\"*;\"*";
                    product = Regex.Split(sr.ReadLine(),pattern);
                    TrimAndSend(product);// trims date of its excess and illegal symbols, and then produces the product

                }
            }
        }

        public void TrimAndSend( string[] words)
        {

            string name;
            double price;
            bool active;

            name = Regex.Replace(words[1], "<.*?>", String.Empty);

            price = double.Parse(words[2]);

            if (words[2] == "0")
            {
                active = false;
            }
            else
            {
                active = true;
            }

            new Product(name, price, active, false);
        }

        public void UserLoader()
        {
            string path = Path.Combine(Logger.basefolder, "LoadFiles\\UsersOnStart.txt");
            using (StreamReader sr = new StreamReader(path)) // Will search through file, loading 3 comma seperated words per. line
            {
                sr.ReadLine(); // Consume description line

                while (!sr.EndOfStream)
                {
                    string[] user = new string[5];

                    string pattern = "\"*;\"*";
                    user = Regex.Split(sr.ReadLine(), pattern);
                    new User(user[1], user[2], user[3], Double.Parse(user[4]), user[5]);
                }
            }
        }
    }
}
