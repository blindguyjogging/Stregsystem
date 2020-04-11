using System;
using System.Collections.Generic;
using System.Text;

namespace Stregsystem
{
    public interface IStregsystem
    {
        List<Product> ActiveProducts();
        void AddCreditsToAccount(User user, int amount);
        void BuyProduct(User user, Product product);
        Product GetProductByID(int id);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        User GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        delegate void UserBalanceNotification(object source, EventArgs args);
        event UserBalanceNotification UserBalanceWarning;
    }

}
