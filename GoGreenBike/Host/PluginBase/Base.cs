using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace goGreenBike
{
    public enum UserActions
    {
        Reset,
    }

    //base
    public interface IConnectable
    {
        void Start();

        //events
        bool Reset();

        //status
        String Poll();
    }

    public abstract class Plugin : IConnectable
    {
        private String _name;
        public String Name { get { return _name; } }

        protected IEnumerable<XElement> items;
        protected XElement currentItem;
        
        public Plugin(String name)
        {
            _name = name;
        }

        public virtual void Start()
        {
            
            throw new NotImplementedException();
        }
        
        //go back -> currentMenu
        public bool Reset()
        {
            if (currentItem.Parent.Name == "root")
            {
                currentItem = items.First();
                return false; //flag to controller
            }

            currentItem = currentItem.Parent;
            return true;
        }
        
        //show current status
        public virtual string Poll()
        {
            return currentItem.Attribute("Text").Value;
        }
    }
}
