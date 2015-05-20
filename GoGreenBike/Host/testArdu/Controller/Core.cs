using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using System.IO;

namespace goGreenBike.Controller
{
    public class Core
    {
        protected System.Timers.Timer poller;
        protected goGreenBike.UI.Engine.ArduinoInterface ui;

        public Core()
        {
            //poll
            poller = new System.Timers.Timer();
            poller.Interval = 1000;
            poller.Elapsed += poller_Elapsed;
            
            //ui
            ui = new UI.Engine.ArduinoInterface();
            ui.OnConnect += ui_OnConnect;
            ui.OnDisconnect += ui_OnDisconnect;
            ui.ResetClick += ui_ResetClick;
            ui.Start();
        }

        void ui_ResetClick()
        {
            Action(UserActions.Reset);
            poller_Elapsed(poller, null);
        }
        
        void ui_OnDisconnect()
        {
            //
        }

        void ui_OnConnect()
        {
            poller.Start();
        }

        ~Core()
        {
            poller.Elapsed -= poller_Elapsed;
            poller.Stop();
        }

        public void Action(UserActions action)
        {
            Console.WriteLine("Action: {0}", action.ToString());

            //switch (action)
            //{
                //case UserActions.Reset:
                //    break;
            //}
        }

        void poller_Elapsed(object sender, ElapsedEventArgs e)
        {
           // var displayText = String.Empty;            
           // //CONSOLE
           // //Console.Clear();
           // //Console.WriteLine("MIMO");
           // //Console.WriteLine("----------------------------------------------");
           // //Console.WriteLine("Current Plugin: {0}", currentPlugin.Name);
           //// Console.WriteLine("UI State: {0}", ui.GetCurrentStatus());
           // //Console.WriteLine("Now Displaying: {0}", displayText);
           // Console.ForegroundColor = ConsoleColor.Magenta;
           // Console.WriteLine("{0}: {1}", DateTime.Now, displayText);
           // Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
