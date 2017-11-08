using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppBoss.View.DeveloperLicense
{
    interface IDeveloperLicenseView : IView
    {
        DateTime LicenseExpirationDate { set; }
        bool IsAddEnabled { set; }
        bool IsRemoveEnabled { set; }
    }
}
