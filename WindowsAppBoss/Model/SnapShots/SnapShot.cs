using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppBoss.Model.SnapShots
{
    public class SnapShot
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public DateTime DateCreated { get; set; }
        public long Size { get; set; }
    }
}
