using System;
using System.Collections.Generic;


namespace Stregsystem
{
    public class PointSystem : IStregsystem
    {
        public PointSystem()
        {
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

        public event IStregsystem.UserBalanceNotification UserBalanceWarning;
        public delegate void UserBalanceNotification(object source, int balance); // delegate to pass when user balance fall below critical levels
        protected virtual void OnUserBalanceWarning(int balance)
        {
            UserBalanceWarning?.Invoke(this, balance);
        }

        public BuyTransaction BuyProduct(User user, Product product) // first checks if user has enough credits for the purchase, if not checks if it can be bought on credit
        {
            BuyTransaction transaction = new BuyTransaction(user, product.Price, product);
            if (transaction.T_User.Balance < product.Price)
            {
                if (!transaction.T_Product.CanBeBoughtOnCredit)
                {
                    return null; // if the user is unable to buy the product, the function will return null, which is evaluated on in the commandparser
                }
            }
            else if (user.Balance - product.Price <= 50) // checking if event needs to be invoked, passes balance - price, since balance, will first update on next UI cycle
            {
                OnUserBalanceWarning(user.Balance - product.Price);
            }
            transaction.Execute(); // Now that everything has been checked to validate to transaction, it will now be executed, through the transactions own method
            return transaction;
        }
        public void AddCreditsToAccount(string username, int amount) // Adds a specified amount of credits to the recieving user
        {
            User user = GetUserByUsername(username);
            InsertCashTransaction insert = new InsertCashTransaction(user, amount);
        }
        public Product GetProductByID(int id) // Gets a certain product, by iterating through the id list, since products are saved accourding to their ID's it is directly accesed
        {
            if (id <= Product.ProductCount)
            {
                return Product.ProductList[id];

            }
            else
            {
                return null;
            }
        }
        public List<User> GetUsers(Func<User, bool> predicate) // not sure how this is implented, and ive had no use of it, but the logic is that it will look through the list and find all elements, where the delegate returns true
        {
            List<User> list = User.UserList.FindAll(x => predicate(x));

            return list;
        }
        public User GetUserByUsername(string username) // Finds a user specified by the predicate exists in the UserList, it is returned to the caller 
        {

            if (User.UserList.Find(x => x.UserName.Equals(username)) == null)
            {
                return null;
            }
            else
            {
                return User.UserList.Find(x => x.UserName.Equals(username));
            }
        }
        public List<Transaction> GetTransactions(User user, int count) // Uses lists getrange method to find the last 10 or less transactions, in reversed order, so newest is first.
        {
            if (user.Transactions.Count < count) // if there is not 10 transactions, we set the offset to the amount of transactions
            {
                List<Transaction> transactions = user.Transactions.GetRange(user.Transactions.Count - user.Transactions.Count, user.Transactions.Count);
                transactions.Reverse();
                return transactions;
            }
            else // if there are more transactions than count parameter, we return the list using the parameter as offset
            {
                List<Transaction> transactions = user.Transactions.GetRange(user.Transactions.Count - count, count);
                transactions.Reverse();
                return transactions;
            }
        }

        public void SetActiveStatus(string status, string productID) // Will change the active status on a product, this is a helper menu, for the admin commands :activate and :
        {
            Product product = GetProductByID(Int32.Parse(productID));
            if (status == ":activate")
            {
                product.Active = true;
                ActiveProducts.Add(product);
            }
            else // shouldent have to failcheck, since only correct commands SHOULD bring us here
            {
                product.Active = false;
                ActiveProducts.Remove(product);
            }
            ActiveProducts.Sort(); // will sort the new list, inorder for the product to appear in a pretty order in UI
        }

        public void SetCreditStatus(string status, string productID) // Will change the active status on a product, this is a helper menu, for the admin commands :crediton and :creditoff
        {
            Product product = GetProductByID(Int32.Parse(productID));
            if (status == ":crediton")
            {
                product.CanBeBoughtOnCredit = true;
            }
            else // shouldent have to failcheck, since only correct commands SHOULD bring us here
            {
                product.CanBeBoughtOnCredit = false;
            }
        }
    }
}
