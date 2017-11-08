using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppBoss.View.Packages
{
    interface IProvisionedPackageManagerView : IView
    {
        IList<Model.Packages.PackageInformation> ListOfPackages { set; }
        bool IsFormEnabled { set; }
    }
}
