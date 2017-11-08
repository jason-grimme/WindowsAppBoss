using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppBoss.View.CustomData
{
    interface ICustomDataView : IView
    {
        IList<Model.Packages.PackageDataViewRow> PackagesWithCustomData { set; }
    }
}
