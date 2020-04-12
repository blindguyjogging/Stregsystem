using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Stregsystem
{
    class StregsystemCommandParser
    {
        public StregsystemCommandParser(PointSystem ps, IStregSystemUI  ui)
        {
            pointsystem = ps;
            UI = ui;
            UI.CommandEntered += Ui_CommandEntered;
        }


        User user;
        Product product;
        PointSystem pointsystem;
        IStregSystemUI UI;

        public void ParseCommand(string command)
        {
            
            if (command.Contains(':'))
            {
                AdminstrationCommand(command);
            }
            else
            {
                string[] commandlist = Regex.Split(command," ");

                if (pointsystem.GetUserByUsername(commandlist[0]) == null)
                {
                    UI.DisplayUserNotFound(commandlist[0]);
                }
                else if (commandlist.Length == 1)
                {
                    Userview(pointsystem.GetUserByUsername(commandlist[0]));
                }
                else if(commandlist.Length == 2)
                {
                    if (pointsystem.GetProductByID(Int32.Parse( commandlist[1])) == null)
                    {
                        UI.DisplayProductNotFound(commandlist[1]);
                    }
                    else
                    {
                        user = pointsystem.GetUserByUsername(commandlist[0]);
                        product = pointsystem.GetProductByID(Int32.Parse(commandlist[1]));
                        pointsystem.ExecuteTransaction(new BuyTransaction(user,product.Price,product));
                        //UI.DisplayUserBuysProduct("");
                    }
                }
                else
                {
                    if (pointsystem.GetProductByID(Int32.Parse(commandlist[1])) == null)
                    {
                        UI.DisplayProductNotFound(commandlist[1]);
                    }
                    else
                        product = pointsystem.GetProductByID(Int32.Parse(commandlist[2]));
                        user = pointsystem.GetUserByUsername(commandlist[0]);
                        MultiBuy(user,Int32.Parse( commandlist[1]),product);
                }
                
            }

            
        }

        void AdminstrationCommand(string command)
        {
        }

        void Userview(User user)
        {

        }

        void MultiBuy(User user, int count, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(user,product.Price*count,product);
            
            UI.DisplayUserBuysProduct(count,transaction);
        }
        private void Ui_CommandEntered(object source, string command)
        {
            ParseCommand(command);
        }

        
    }
}
