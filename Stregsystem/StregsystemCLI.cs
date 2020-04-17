using System;
using System.Collections.Generic;


namespace Stregsystem
{
    public class StregsystemCLI : IStregSystemUI
    {
        public StregsystemCLI(PointSystem ps)
        {
            pointsystem = ps;
            pointsystem.UserBalanceWarning += Pointsystem_UserBalanceWarning; // handle for the userbalance warning, when a users balance falls below a threshold 
        }


        bool _running; // the state of the application, if true, UI gets to run, if not UI stops 
        public PointSystem pointsystem;

        string feedbackLine = ""; // First flexible line in the menu, will give userfeedback on commands
        string WarningLine = ""; // second flexible line, will ONLY give warnings

        public event IStregSystemUI.StregsystemEvent CommandEntered;

        public delegate void StregsystemEvent(object source, string arg);

        protected virtual void OnCommandEntered(string command)
        {
            CommandEntered?.Invoke(this, command);
        }

        public void Close() // The menu will close, after _running is set to false
        {
            _running = false;
            Console.Clear();
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            feedbackLine = "AdminCommand " + adminCommand + " was not recognized";
        }

        public void DisplayGeneralError(string errorString)
        {
            feedbackLine = "Error concerning: " + errorString + ", try again";
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            feedbackLine = user.UserName + " Does not have sufficient credit for product " + product.ID;
        }

        public void DisplayProductNotFound(string product)
        {
            feedbackLine = "Product with ID = " + product + " was not found";
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            feedbackLine = "the input \"" + command + "\" holds too many arguments";
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            feedbackLine = transaction.T_User.FirstName + " just bought " + transaction.T_Product.Name;
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            feedbackLine = transaction.T_User.FirstName + " just bought " + count + "x " + transaction.T_Product.Name;
        }

        public void DisplayUserInfo(User user) // displays the userview, will show tostring, balance and recent transactions
        {
            Console.Clear();

            Console.WriteLine("                                           " + user.UserName + "'s page                                                         ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
            Console.WriteLine(user.ToString());
            Console.WriteLine("Balance: " + user.Balance);
            if (user.Transactions.Count == 0) // if user has no transactions, we instead pass this simple writeline
            {
                Console.WriteLine("                                       User has no previous purchases");
            }
            else // user has atleast one transaction, this or these will be displayed, up to a limit of 10
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                     " + user.UserName + "'s last " + user.Transactions.Count % 11 + " transactions");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                List<Transaction> transactions = pointsystem.GetTransactions(user, 10);  // function call to the function returning a list of 10 latest transaction, newest first
                foreach (var item in transactions)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\n\n Press enter to return to the menu.."); // Possible further development could also focus on accepting certain commands(or all) in userview

            Console.ReadLine();
        }

        public void DisplayUserNotFound(string username)
        {
            feedbackLine = "User \"" + username + "\" could not be found, check spelling";
        }

        public void DisplayCreditsAdded(string username, int amount)
        {
            User user = pointsystem.GetUserByUsername(username);
            feedbackLine = amount + " credits was added to " + user.FirstName + "'s account";
        }

        public void Start() // very basic method, will run the UI in a loop until _running is set to false, which is done through Close()
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
            int i = 0;
            Console.Clear();
            Console.Write("\n To quickbuy type \"'username' 'Product number'\" " +
                "\n To multibuy type\"'username' 'amount' 'product number'\" " +
                "\n To access user page type ONLY username " +
                "\n - Spaces between username, number and amount are required, and order is fixed" +
                "\n-----------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("{0,-5} {1,-35:N1} {2,-5} |  {0,-5} {1,-35:N1} {2,-5} \n", "ID", "Product", "Price");
            foreach (var item in pointsystem.ActiveProducts)
            {
                if (i % 2 == 0)
                {
                    Console.Write("{0,-5} {1,-35:N1} {2,-5} ", pointsystem.ActiveProducts[i].ID, pointsystem.ActiveProducts[i].Name, pointsystem.ActiveProducts[i].Price);
                }
                else
                {
                    Console.Write("|  {0,-5} {1,-35:N1} {2,-5}\n", pointsystem.ActiveProducts[i].ID, pointsystem.ActiveProducts[i].Name, pointsystem.ActiveProducts[i].Price);
                }
                i++;
            }
            Console.WriteLine(
                "\n-----------------------------------------------------------------------------------------------------------\n");

            Console.Write("                     " + feedbackLine +
                "\n                     " + WarningLine +
                "\n-----------------------------------------------------------------------------------------------------------\n" +
                "                                         Input: "
            );
            feedbackLine = "";
            WarningLine = "";
        }
        private void Pointsystem_UserBalanceWarning(object source, int balance) // changes the unique warning line, to inform the user that is balance is running low, and the current balance
        {
            WarningLine = "Warning: balance is low!, currently: " + balance;
        }
    }
}
