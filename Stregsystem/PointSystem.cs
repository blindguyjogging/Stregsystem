using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;


namespace Stregsystem
{
    public class PointSystem
    {
        public PointSystem()
        {
            Logger.ClearLogs();
            
            FileLoader loader = new FileLoader();

            loader.ProductLoader();
            loader.UserLoader();

            int i = 1;
            while (i + 1 < Product.ProductCount)
            {
                if (Product.ProductList[i].Active)
                {
                    ActiveProducts.Add(Product.ProductList[i]);
                }

                i++;
            }
        }

        public List<Product> ActiveProducts = new List<Product>();

        public void BuyProduct(User user, Product product)
        {
            BuyTransaction buy = new BuyTransaction(user, product.Price, product);
        }
        public void AddCreditsToAccount(User user, double amount)
        {
            InsertCashTransaction insert = new InsertCashTransaction(user, amount);
        }
        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
        }
        public Product GetProductByID(int id)
        {
            return Product.ProductList[id];
        }
        public void GetUsers(Func<User, bool> predicate) //////////////////////////////////////////////// NOT DONE /////////////////////////////////////////////////////////////////////////////////
        {

        }
        public User GetUserByUsername(string username)
        {
 
            if (User.UserList.Find(x => x.UserName.Equals(username)) == null)
            {
                return User.UserList.Find(x => x.UserName.Equals(username));
            }
            else
            {
                return null;
            }
        }
        public void GetTransactions(User user, int count)
        {
            List<Transaction> Transactions = new List<Transaction>();

            if (user.Transactions.Count < count)
            {
                Feedback.InvalidArg("Asked for " + count + " newest transactions, but user only has " + user.Transactions.Count);
            }
            else
            {

                for (int i = 0; i <= count; i++)
                {
                    if (Transactions[i] == null)
                    {
                        Feedback.NullReference("Nullreference when trying to acces element" + i + "In Transactions");
                    }
                    else
                    {
                        Transactions[i] = user.Transactions[i];
                    }
                }
            }
        }
    }
}
