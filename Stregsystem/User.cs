﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Stregsystem
{
    public class User : IComparable
    {
        public User(string firstname, string lastname, string username,double balance, string email)
        {
            if (firstname != null) FirstName = firstname;
            else Feedback.NullReference("User firstname in constructor is null");

            if (lastname != null) LastName = lastname;
            else Feedback.NullReference("User lastname in constructor is null");

            if (usernameRegex.IsMatch(username) )
            {
                UserName = username;
            }
            else
            {
                throw new ArgumentException("Username contains illegal characters");
            }

            Balance = balance;

            if (emailRegex.IsMatch(email))
            {
                Email = email;
            }
            else
            {
                throw new ArgumentException("Email contains illegal characters, use only letters, and the symbols , . _");
            }

            ID = userCount++;
            UserList.Add(this);

            Logger.UserLog(ToString());
            
        }
        public static List<User> UserList = new List<User>();
        static int userCount = 0;

        public List<Transaction> Transactions = new List<Transaction>();
        public int ID;
        string FirstName;
        string LastName;
        public string UserName;
        string Email;
        public double Balance;
        Regex usernameRegex = new Regex(@"^[a-z0-9_]+$");
        Regex emailRegex = new Regex(@"^[a-zA-Z0-9.,_]+@[a-zA-Z0-9,-]+\.[a-zA-Z0-9]+$");



        override
        public string ToString()
        {
            return DateTime.Now + "User "+ID+"Registered with name "+FirstName + " " + LastName + ", and mail " + Email;
        }
/*
        public override bool Equals(object obj)
        {
            if (obj == null) return 1;

            User otherUser = obj as User;
            if (otherUser != null)
                return this.ID.CompareTo(otherUser.ID);
            else
                throw new ArgumentException("Object is not of class User");
        }

        public override int GetHashCode()
        {
            int HashCode = 53749785 ;
            return HashCode;
        }
*/

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            User otherUser = obj as User;
            if (otherUser != null)
                return this.ID.CompareTo(otherUser.ID);
            else
                throw new ArgumentException("Object is not of class User");
        }
    }
}