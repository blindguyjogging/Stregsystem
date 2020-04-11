using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Stregsystem
{
    class UI
    {
        public static void Runner()
        {
            Application.Init();

            var top = Application.Top;
            var window = new Window(new Rect(0, 1, top.Frame.Width, top.Frame.Height - 1), "Pointsystem");
            top.Add(window);


            window.Add(
                new TextField(35, 2, 50, ""),
                new Label(35, 1, "Please input your order like this:"),
                new Label(6,5,"---------------------------------------------------------------------------------------------------------")

                ) ;
            
            Application.Run();
        }

        public string ProductWindow()
        {

            return "";
        }
        public void MakePopUp() { }

    }
}
