using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace goGreenBike
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new Controller.Core();
            Thread.Sleep(int.MaxValue);                        
        }
    }
}
