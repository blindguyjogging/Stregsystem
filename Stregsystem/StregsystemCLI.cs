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
            Runner();
        }
        public static void Runner()
        {
            Application.Init();

            var top = Application.Top;
            var window = new Window(new Rect(0, 1, top.Frame.Width, top.Frame.Height - 1), "Pointsystem");
            top.Add(window);


            window.Add(
                new TextField(35, 2, 50, ""),
                new Label(35, 1, "Please input your order like this:"),
                new Label(6, 5, "---------------------------------------------------------------------------------------------------------")

                );

            Application.Run();
        }

        public string ProductWindow()
        {

            return "";
        }
    }
}
