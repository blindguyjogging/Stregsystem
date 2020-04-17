using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Stregsystem
{
    public class User : IComparable
    {
        public User(string firstname, string lastname, string username, int balance, string email)
        {
            if (firstname != null) FirstName = firstname;
            else throw new ArgumentNullException("User firstname in constructor is null"); // if firstname is loaded with null, this throws an execptions, since it breaks the run

            if (lastname != null) LastName = lastname;
            else throw new ArgumentNullException("User lastname in constructor is null"); // if firstname is loaded with null, this should throw an execption, since it breaks the run

            if (usernameRegex.IsMatch(username)) // if statement checks if username is in a valid format, if not it throws an execption
            {
                UserName = username;
            }
            else
            {
                throw new ArgumentException("Username contains illegal characters");
            }

            Balance = balance;

            if (emailRegex.IsMatch(email)) // if statement checks of email is in a valid format, if not it throws an execption
            {
                Email = email;
            }
            else
            {
                throw new ArgumentException("Email contains illegal characters, use only letters, and the symbols , . _");
            }

            ID = userCount++;
            UserList.Add(this);

            Logger.UserLog(DateTime.UtcNow + " User: " + ID + ":" + ToString() + " was registered"); // adds the user to a log

        }
        public static List<User> UserList = new List<User>(); // List of ALL users
        static int userCount = 0; // used to give users, their id, and also their overall count

        public List<Transaction> Transactions = new List<Transaction>();
        public int ID;
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        readonly string Email;
        public int Balance;
        readonly Regex usernameRegex = new Regex(@"^[a-z0-9_]+$");
        readonly Regex emailRegex = new Regex(@"^[a-zA-Z0-9.,_]+@[a-zA-Z0-9.,-]+\.[a-zA-Z0-9]+$");


        override
        public string ToString()
        {
            return "Name: " + FirstName + " " + LastName + " Email: " + Email;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is User objAsUser)) return false;
            else return Equals(objAsUser);
        }

        public override int GetHashCode()
        {
            return ID;
        }


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is User otherUser)
                return this.ID.CompareTo(otherUser.ID);
            else
                throw new ArgumentException("Object is not of class User");
        }
    }
}
