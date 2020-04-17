using System;
using System.Collections.Generic;

namespace Stregsystem
{
    public interface IStregsystem
    {
        void AddCreditsToAccount(string username, int amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int id);
        List<Transaction> GetTransactions(User user, int count);
        List<User> GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        void SetActiveStatus(string status, string productID);
        void SetCreditStatus(string status, string productID);
        delegate void UserBalanceNotification(object source, int balance);
        event UserBalanceNotification UserBalanceWarning;
    }

}
