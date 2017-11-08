using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppBoss.Model.Packages
{
    public class PackageInformation
    {
        public string InstallationDirectory { get; set; }
        public string DataDirectory { get; set; }
        public Version AppVersion { get; set; }
        public string Publisher { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}
