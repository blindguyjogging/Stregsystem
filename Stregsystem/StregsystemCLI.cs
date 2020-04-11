using System;
using System.Collections.Generic;
using System.Text;

namespace Stregsystem
{
    public class StregsystemCLI : IStregSystemUI
    {
        public delegate void StregsystemEvent(object source, EventArgs args);

        public bool running = false;
        public event StregsystemEvent CommandEntered;

        event IStregSystemUI.StregsystemEvent IStregSystemUI.CommandEntered
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        protected virtual void OnCommandEntered()
        {
            if (CommandEntered!= null)
            {
                CommandEntered(this,EventArgs.Empty);
            }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            throw new NotImplementedException();
        }

        public void DisplayGeneralError(string errorString)
        {
            throw new NotImplementedException();
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            throw new NotImplementedException();
        }

        public void DisplayProductNotFound(string product)
        {
            throw new NotImplementedException();
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserNotFound(string username)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            UI.Runner();
        }
    }
}
