using System;
using System.Text.RegularExpressions;

namespace Stregsystem
{
    class StregsystemCommandParser
    {
        public StregsystemCommandParser(IStregsystem ps, IStregSystemUI ui)
        {
            pointsystem = ps;
            UI = ui;
            UI.CommandEntered += Ui_CommandEntered;
        }

        User user;
        Product product;
        IStregsystem pointsystem;
        IStregSystemUI UI;

        public void ParseCommand(string command)
        {
            command = command.Trim();
            string[] commandlist = Regex.Split(command, " ");

            if (commandlist.Length > 3) // no command takes over 3 arguments
            {
                UI.DisplayTooManyArgumentsError(command);
            }
            else if (command.Contains(':')) // If command contains ':' it can ONLY be admin command, has its own 
            {

                AdminstrationCommand(Regex.Split(command, " "));
            }
            else // If command does not contain ':' we know that it should be a buy or user command
            {

                if (pointsystem.GetUserByUsername(commandlist[0]) == null) // if user is null, we can disregard the command, as all commands at this point needs a user
                {
                    UI.DisplayUserNotFound(commandlist[0]);
                }
                else // we now now that first element is user, and that it is valid
                {
                    user = pointsystem.GetUserByUsername(commandlist[0]);
                    if (commandlist.Length == 1) // if lenght is 1, then it should be a User command
                    {
                        UI.DisplayUserInfo(user);
                    }
                    else // the remaining commands can either be quickbuy or multibuy
                    {
                        if (commandlist.Length == 2) // if lenght is 2 it should be a quickbuy command
                        {
                            if (pointsystem.GetProductByID(Int32.Parse(commandlist[1])) == null)
                            {
                                UI.DisplayProductNotFound(commandlist[1]);
                            }
                            else if (pointsystem.GetProductByID(Int32.Parse(commandlist[1])).Active)
                            {
                                product = pointsystem.GetProductByID(Int32.Parse(commandlist[1]));
                                BuyTransaction transaction = pointsystem.BuyProduct(user, product); // returns null if invalid funds
                                if (transaction == null)
                                {

                                    UI.DisplayInsufficientCash(user, product);
                                }
                                else
                                    UI.DisplayUserBuysProduct(transaction);
                            }
                            else
                            {
                                UI.DisplayGeneralError("Product is not active, therefore unable to be purchased");
                            }
                        }
                        else // if lenght is == 3, we know that it should be a multibuy
                        {
                            if (pointsystem.GetProductByID(Int32.Parse(commandlist[2])) == null)
                            {
                                UI.DisplayProductNotFound(commandlist[2]);
                            }
                            else // Multibuy: first element will be user, second will be count, and third will be productID
                            {
                                product = pointsystem.GetProductByID(Int32.Parse(commandlist[2]));
                                for (int i = 0; i < Double.Parse(commandlist[1]) - 1; i++)
                                {
                                    pointsystem.BuyProduct(user, product);
                                }
                                UI.DisplayUserBuysProduct(Int32.Parse(commandlist[1]), pointsystem.BuyProduct(user, product));
                            }
                        }
                    }
                }

            }
        }




        void AdminstrationCommand(string[] commandlist) // I am aware that this collides with the requested dictionary definition, however i could simply not make it work
        {
            int argsCount = commandlist.Length;
            switch (commandlist[0]) // a switch over AdminCommands, this will if the commands are typed in a valid manner, and if it is, a relevant function will be called
            {
                case ":quit": // if commands is either q or quit, UI.close will be called to effectivly end the session
                case ":q":
                    if (argsCount == 1)
                    {
                        UI.Close();
                    }
                    else
                        UI.DisplayGeneralError("q or quit command can only be 1 arg");
                    break;
                case ":activate": // if commands is either activate or deactivate, SetActiveStatus will be called in order to change the value of Product.Active
                case ":deactivate":
                    if (argsCount == 2)
                    {
                        pointsystem.SetActiveStatus(commandlist[0], commandlist[1]);
                    }
                    else
                        UI.DisplayGeneralError("Command can only be 2 args: status and productID");
                    break;
                case ":crediton": // If commands is either crediton or creditoff, SetCreditStatus will be called in order to change the value of Product.CanBeBoughtOnCredit
                case ":creditoff":
                    if (argsCount == 2)
                    {
                        pointsystem.SetCreditStatus(commandlist[0], commandlist[1]);
                    }
                    else
                        UI.DisplayGeneralError("Command can only be 2 args: status and productID");
                    break;
                case ":addcredits": // If addcredits helper function is called, and UI will load a new feedbackLine
                    if (argsCount == 3)
                    {
                        pointsystem.AddCreditsToAccount(commandlist[2], Int32.Parse(commandlist[1]));
                        UI.DisplayCreditsAdded(commandlist[2], Int32.Parse(commandlist[1]));
                    }
                    else
                        UI.DisplayGeneralError("Command can only be 3 args, :addcredits, amount and user");
                    break;
                default:
                    UI.DisplayAdminCommandNotFoundMessage(commandlist.ToString()); // command was not in a recoqnized format
                    break;

            }
        }
        private void Ui_CommandEntered(object source, string command) // eventhandle for the command entered
        {
            ParseCommand(command);
        }


    }
}
