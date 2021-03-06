﻿using System;
using System.Collections.Generic;

namespace Stregsystem
{
    public class Product : IComparable
    {
        public Product(string name, int price, bool active, bool canBeBoughtOnCredit)
        {
            if (name != null) // name cant be null, if it is throw an exception
            {
                Name = name;
            }
            else
            {
                throw new ArgumentNullException("Product name in constructor is null");
            }
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
            ID = ProductCount++;

            if (ID > 1) // this logic will make sure, that the product list "Starts" at index 1
            {
                ProductList.Add(this);
            }
            else
            {
                ProductList.Add(null);
                ProductList.Add(this);
            }

            Logger.ProductLog(ToString()); // logs this instance of the product class

        }
        public static List<Product> ProductList = new List<Product>();

        public static int ProductCount = 1;

        public int ID { get; private set; }
        public string Name { get; private set; }
        public int Price;
        public bool Active;
        public bool CanBeBoughtOnCredit;

        override
        public string ToString()
        {
            return DateTime.UtcNow + " Product " + ID + ": " + Name + ", for " + Price + "credits ";
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Product otherProduct = obj as Product;
            if (otherProduct != null)
                return this.ID.CompareTo(otherProduct.ID);
            else
                throw new ArgumentException("Object is not of class User");
        }
    }

    public class SeasonalProduct : Product
    {
        public SeasonalProduct(string name, int price, bool active, bool canBeBoughtOnCredit, string seasonstartdate, string seasonenddate) : base(name, price, active, canBeBoughtOnCredit)
        {
            SeasonStartDate = seasonstartdate;
            SeasonEndDate = seasonenddate;
        }

        public string SeasonStartDate;
        public string SeasonEndDate;
    }
}
