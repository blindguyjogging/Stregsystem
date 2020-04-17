using System;
using System.Collections.Generic;
using System.Text;

namespace Stregsystem
{
    public abstract class Transaction
    {
        public Transaction(User user, int amount)
        {
            T_User = user;
            ID = TransactionCounter++;
            Date = DateTime.UtcNow.ToShortDateString();
            Amount = amount;          
        }

        // Attributes
        public static int TransactionCounter;

        public int ID;
        public User T_User; // renamed T_User, in order to avoid confusion and errorprone code with the object
        public string Date;
        public int Amount;
        
        
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
        public InsertCashTransaction(User user, int amount) : base(user, amount)
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
            return Date+" Deposit "+ID+": "+Amount+" was inserted to "+ T_User.ID;
        }

    }

    public class BuyTransaction : Transaction
    {
        public BuyTransaction(User user, int amount, Product product) : base(user, amount)
        {
            T_Product = product;
        }

        //Attributes
        public Product T_Product; 

        //Methods
        public override void Execute() // if i Arrive here, everything is validated for the purchase 
        {

                T_User.Balance -= Amount;
                Logger.TransactionLog(ToString());
                T_User.Transactions.Add(this);
        }

        public override string ToString()
        {
            return Date+" Purchase transaction " + ID + " of Product " + T_Product.Name+ " For the amount of " + Amount + " was bought by User " + T_User.ID;
        }
    }
}