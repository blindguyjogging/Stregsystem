using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Terminal.Gui;

namespace Stregsystem
{
    public class FileLoader
    {
        string catalogue = Path.Combine(Logger.basefolder, "LoadFiles\\Catalogue.txt");
        string users = Path.Combine(Logger.basefolder, "LoadFiles\\UsersOnStart.txt");
        public void ProductLoader()
        {
            using (StreamReader sr = new StreamReader(catalogue)) // Will search through file, loading 3 comma seperated words per. line
            {
                sr.ReadLine(); // Consume description line

                while (!sr.EndOfStream)
                {
                    string[] product = new string[5];

                    string pattern = "\"*;\"*";
                    product = Regex.Split(sr.ReadLine(), pattern);
                    TrimAndSend(product);// trims date of its excess and illegal symbols, and then produces the product

                }
            }
        }

        public void TrimAndSend( string[] words)
        {

            string name;
            double price;
            bool active = false;

            name = Regex.Replace(words[1], "<.*?>", String.Empty);

            price = double.Parse(words[2]);

            if (words[3] == "0")
            {
                active = false;
            }
            else if(words[3] == "1")
            {
                active = true;
            }
            else
            {
                throw new Exception(words[3]);
            }

            new Product(name, price, active, false);
        }

        public void UserLoader()
        {
            using (StreamReader sr = new StreamReader(users)) // Will search through file, loading 3 comma seperated words per. line
            {
                sr.ReadLine(); // Consume description line

                while (!sr.EndOfStream)
                {
                    string pattern = ",";
                    string[] user = new string[5];
                    user = Regex.Split(sr.ReadLine(),pattern);
                    new User(user[1], user[2], user[3], double.Parse(user[4]), user[5]);
                }
            }
        }
    }
}
