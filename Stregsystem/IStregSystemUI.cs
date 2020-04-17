using System;
using System.Collections.Generic;
using System.Text;


namespace Stregsystem
{
    public interface IStregSystemUI
    {
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(string product);
        void DisplayUserInfo(User user);
        void DisplayTooManyArgumentsError(string command);
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(int count, BuyTransaction transaction);
        void DisplayInsufficientCash(User user, Product product);
        void DisplayGeneralError(string errorString);
        void DisplayCreditsAdded(string username, int amount);
        void Start();
        void Close();
        delegate void StregsystemEvent(object source,string arg);
        event StregsystemEvent CommandEntered;
    }
}
