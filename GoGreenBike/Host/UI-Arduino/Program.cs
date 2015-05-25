using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daniel.arduino
{
    public class Program
    {
        static void Main(String[] args)
        {
            while (true)
            {
                try
                {
                    new goGreenBike.UI.Engine.ArduinoInterface(115200).Start();
                }
                catch (System.Runtime.InteropServices.SEHException c)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(c.StackTrace);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }
    }
}
