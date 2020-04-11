using System;
using System.Collections.Generic;
using System.Text;

namespace Stregsystem
{
    public abstract class Transaction
    {

        delegate void userBalanceNotification(User user, decimal balance);

        userBalanceNotification Handler;
        public static void DelegateMethod(User user)
        {
                //UI.MakePopup("Balance running low, currently: "+user.Balance);          
        }

        public Transaction(User user, double amount)
        {
            if (user != null) T_User = user;
            else Feedback.NullReference("User in transaction constructor is null");

            Amount = amount;

            Date = DateTime.Now.ToString();

            ID = TransactionCounter++;
        }

        // Attributes
        public static int TransactionCounter;

        public int ID;
        public User T_User; // renamed T_User, in order to avoid confusion and errorprone code with the object
        public string Date;
        public double Amount;
        
        //Methods
        override
        public string ToString()
        {
            return ID+" " + T_User + " "+ Amount + " " + Date;
        }

        public abstract void Execute(); // Main logic behind realising a transaction
    
        
    }

    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, double amount) : base(user, amount)
        {
            Execute(); // all is loaded, transaction will now execute
        }

        public override void Execute()
        {
            T_User.Balance += Amount;
            Logger.TransactionLog(ToString());
            T_User.Transactions.Add(this);


        }

        public override string ToString()
        {
            return Date+" Deposit transaction "+ID+" For the amount of "+Amount+" was inserted by User "+ T_User.ID;
        }

    }

    public class BuyTransaction : Transaction
    {
        public BuyTransaction(User user, double amount, Product product) : base(user, amount)
        {
            if (product != null)
            {
                T_Product = product;
                Execute(); // all is loaded, transaction will now execute
            }
            else
                Feedback.NullReference("Product in buytransaction constructor is null");
        }

        //Attributes
        public Product T_Product; 

        //Methods
        public override void Execute()
        {
            if (T_User.Balance < Amount)
            {
                if (!T_Product.CanBeBoughtOnCredit)
                {
                Feedback.InsufficientCreditsException("User "+T_User.ID+"Tried to buy Product"+T_Product.Name+"But had insufficient funds");
                }
                else
                {
                    T_User.Balance -= Amount;
                    Logger.TransactionLog(ToString());
                    T_User.Transactions.Add(this);
                }
            }
            else
            {
                T_User.Balance -= Amount;
                Logger.TransactionLog(ToString());
                T_User.Transactions.Add(this);

            }
        }

        public override string ToString()
        {
            return Date+" Purchase transaction " + ID + " of Product " + T_Product.Name+ " For the amount of " + Amount + " was bought by User " + T_User.ID;
        }
    }
}