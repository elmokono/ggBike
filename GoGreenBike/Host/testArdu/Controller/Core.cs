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
using System.Configuration;

namespace goGreenBike.Controller
{
    public class Core
    {
        protected System.Timers.Timer poller;
        protected goGreenBike.UI.Engine.ArduinoInterface ui;
        protected String deviceID = ConfigurationManager.AppSettings["deviceID"];
        protected Int32 serialPortSpeed = Int32.Parse(ConfigurationManager.AppSettings["serialPortSpeed"]);

        public Core()
        {
            //poll
            //poller = new System.Timers.Timer();
            //poller.Interval = 1000;
            //poller.Elapsed += poller_Elapsed;

            //ui
            ui = new UI.Engine.ArduinoInterface(serialPortSpeed);
            ui.OnConnect += ui_OnConnect;
            ui.OnDisconnect += ui_OnDisconnect;
            ui.ResetClick += ui_ResetClick;
            ui.Spins += ui_Spins;
            ui.Start();
        }

        void ui_Spins(long millis, double spins)
        {
            renderStats(millis, spins);

            using (var db = new Model.DatabaseEntitiesDataContext())
            {
                db.AddSpins(deviceID, Convert.ToInt32(millis), spins);
                db.SubmitChanges();
            }
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
            //poller.Start();
        }

        ~Core()
        {
            //poller.Elapsed -= poller_Elapsed;
            //poller.Stop();
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

        void renderStats(long millis, double spins)
        {
            double circ = 0.0;
            using (var db = new Model.DatabaseEntitiesDataContext())
            {
                circ = double.Parse(ConfigurationManager.AppSettings["deviceCirc"]);
            }
            
            var kmh = (circ / 1000.0 / 1000.0 * spins) / (millis / 1000.0 / 60.0 / 60.0);
            
            Console.Clear();
            Console.WriteLine(new String('-', 79));
            Console.WriteLine("| Arduino Controller");
            Console.WriteLine(new String('-', 79));
            Console.WriteLine("Current spins: {0:#0.00}", spins);
            Console.WriteLine("Current millis lapse: {0}ms", millis);
            Console.WriteLine("Current speed: {0:#0.00}km/h", kmh);
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
