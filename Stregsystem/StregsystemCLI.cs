using System;
using System.Collections.Generic;
using System.Text;


namespace Stregsystem
{
    public class StregsystemCLI : IStregSystemUI
    {
        bool _running;
        public StregsystemCLI(PointSystem ps)
        {
            pointsystem = ps;
        }

        public PointSystem pointsystem;

        string feedbackLine = "";

        public event IStregSystemUI.StregsystemEvent CommandEntered;

        public delegate void StregsystemEvent(object source, string arg);

        protected virtual void OnCommandEntered(string command)
        {
            if (CommandEntered!= null)
            {
                CommandEntered(this, command);
            }
        }

        public void Close()
        {
            _running = false;
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            feedbackLine = "AdminCommand " + adminCommand + " was not recognized";
        }

        public void DisplayGeneralError(string errorString)
        {
            feedbackLine = "Error concerning "+errorString+", try again";
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            feedbackLine = user.UserName + "Does not have sufficient credit for product " + product.ID;
        }

        public void DisplayProductNotFound(string product)
        {
            feedbackLine = "Product with ID = " + product + " was not found";
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            feedbackLine = "the input \""+command+ "\" holds too many arguments";
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            feedbackLine = "User just bought " +transaction.T_Product.Name;
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            feedbackLine = "User just bought " +count+"x " + transaction.T_Product.Name;
        }

        public void DisplayUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserNotFound(string username)
        {
            feedbackLine = "User \""+username + "\" could not be found, check spelling";
        }

        public void Start()
        {
            _running = true;
            do
            {
                DrawMenu();
                HandleInput();
            } while (_running);
        }

        public void HandleInput()
        {
            string command = Console.ReadLine();
            OnCommandEntered(command);
        }
        public void DrawMenu()
        {
            Console.Write("\n To quickbuy type \"'username' 'Product number'\" " +
                "\n To multibuy type\"'username' 'amount' 'product number'\" " +
                "\n To access user page type ONLY username " +
                "\n - Spaces between username, number and amount are required, and order is fixed" +
                "\n-----------------------------------------------------------------------------------------------------------\n");
            for (int i = 0; i + 1 < pointsystem.ActiveProducts.Count; i++)
            {
                Console.WriteLine("{0,-5} {1,-35:N1} {2,-5} | {3,-5} {4,-35} {5,0}", pointsystem.ActiveProducts[i].ID, pointsystem.ActiveProducts[i].Name, pointsystem.ActiveProducts[i].Price, pointsystem.ActiveProducts[i + 1].ID, pointsystem.ActiveProducts[i + 1].Name, pointsystem.ActiveProducts[i + 1].Price);
                i++;
            }
            Console.WriteLine(
                "\n-----------------------------------------------------------------------------------------------------------\n");
                 
                Console.Write("                     "+feedbackLine+"\n-----------------------------------------------------------------------------------------------------------\n" +
                "                                         Input: "
                ) ;
        }
        public void DrawMenu(string message)
        {
            Console.Write("\n To quickbuy type \"'username' 'Product number'\" " +
                "\n To multibuy type\"'username' 'amount' 'product number'\" " +
                "\n To access user page type ONLY username " +
                "\n - Spaces between username, number and amount are required, and order is fixed" +
                "\n-----------------------------------------------------------------------------------------------------------\n");
            for (int i = 0; i + 1 < pointsystem.ActiveProducts.Count; i++)
            {
                Console.WriteLine("{0,-5} {1,-35:N1} {2,-5} | {3,-5} {4,-35} {5,0}", pointsystem.ActiveProducts[i].ID, pointsystem.ActiveProducts[i].Name, pointsystem.ActiveProducts[i].Price, pointsystem.ActiveProducts[i + 1].ID, pointsystem.ActiveProducts[i + 1].Name, pointsystem.ActiveProducts[i + 1].Price);
                i++;
            }
            Console.Write(
                "\n-----------------------------------------------------------------------------------------------------------\n"
                 + "                                                                                                      "
               + "\n-----------------------------------------------------------------------------------------------------------\n" +
                "                                         Input: "
                );
        }
    }
}
