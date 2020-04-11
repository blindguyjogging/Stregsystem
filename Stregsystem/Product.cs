using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stregsystem
{
    public class Product
    {
        public Product(string name, double price, bool active, bool canBeBoughtOnCredit)
        {
            if (name != null)
            {
                Name = name;
            }
            else
            {
                Feedback.NullReference("Product name in constructor is null");
            }
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
            ID = ProductCount++;

            if (ID > 0)
            {
                ProductList.Add(this);
            }
            else
            {
                ProductList.Skip(1);
                ProductList.Add(this);
            }

            Logger.ProductLog(ToString());

        }
        public static List<Product> ProductList = new List<Product>();
        
        public static int ProductCount= 1;

        public int ID;
        public string Name;
        public double Price;
        public bool Active;
        public bool CanBeBoughtOnCredit;

        override
        public string ToString() 
        {
            return  DateTime.Now +" Product "+ID+" called "+Name+" and costing "+Price+"dkk, was registered ";
        }
    }

    public class SeasonalProduct : Product
    {
        public SeasonalProduct(string name, double price, bool active, bool canBeBoughtOnCredit, string seasonstartdate, string seasonenddate) : base(name, price, active, canBeBoughtOnCredit)
        {
            SeasonStartDate = seasonstartdate;
            SeasonEndDate = seasonenddate;
        }

        public string SeasonStartDate;
        public string SeasonEndDate;
    }
}
