using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;


namespace Stregsystem.Logs
{
    class PointSystem
    {
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
        public void GetUsers(Func<User,bool>predicate) //////////////////////////////////////////////// NOT DONE /////////////////////////////////////////////////////////////////////////////////
        {
            
        }
        public User GetUserByUsername(string username)
        {
            int i = 0;
            while (User.UserList[i] != null && User.UserList[i].UserName != username)
            {
                i++;
            }
            if (User.UserList[i] != null)
            {
                return User.UserList[i];
            }
            else
                Feedback.NullReference("Unable to find User of username: "+username);
                return null;
        }
        public void GetTransactions(User user, int count)
        {
            List<Transaction> Transactions = new List<Transaction>();

            if (user.Transactions.Count < count)
            {
                Feedback.InvalidArg("Asked for "+count+" newest transactions, but user only has "+ user.Transactions.Count);
            }
            else
            {

                for (int i = 0; i <= count; i++)
                {
                    if (Transactions[i] == null)
                    {
                        Feedback.NullReference("Nullreference when trying to acces element"+i+"In Transactions");
                    }
                    else
                    {
                     Transactions[i] = user.Transactions[i];
                    }
                }
            }
        }
        public List<Product> ActiveProducts()
        {
            List<Product> activeproducts = new List<Product>();
            int i = 1;
            while (i <= Product.ProductCount)
            {
                if (Product.ProductList[i].Active)
                {
                    activeproducts.Add(Product.ProductList[i]);
                }

                i++;
            }
            return activeproducts;
        }
    }
}
