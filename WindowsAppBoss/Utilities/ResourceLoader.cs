using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace WindowsAppBoss.Utilities
{


    public class ResourceLoader
    {

        public ResourceManager Manger
        {
            get
            {
                if(_manager == null)
                {
                    _manager = new ResourceManager("Resources.Strings", this.GetType().Assembly);
                }
                return _manager;
            }
        }
        private ResourceManager _manager = null;

        public string GetString(string name)
        {
            return this.Manger.GetString(name);
        }


    }
}
