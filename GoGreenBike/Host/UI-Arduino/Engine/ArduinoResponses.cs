using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goGreenBike.UI.Engine
{
    class ArduinoResponse
    {
        public String cmd { get; set; }
        public String val { get; set; }
        public Boolean succeeded { get; set; }
    }

    class ArduinoSpinsResponse
    {
        public Int32 Spins { get; set; }
        public long StartMillis { get; set; }
        public long EndMillis { get; set; }
    }

    class ArduinoCapabilitiesResponse
    {
        public class CapableField
        {
            public Boolean Capable { get; set; }
        }

        public class SystemField
        {
            public String Model { get; set; }
        }

        public class LedsField : CapableField
        {
            public Int32 Count { get; set; }
            public String Colors { get; set; }
        }
        public class ButtonsField : CapableField
        {
            public Int32 Count { get; set; }
            public String Actions { get; set; }
        }
        
        public SystemField System { get; set; }
        public LedsField Leds { get; set; }
        public ButtonsField Buttons { get; set; }
    }

}
