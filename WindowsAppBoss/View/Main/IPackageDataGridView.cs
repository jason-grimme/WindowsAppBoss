using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppBoss.View.Main
{
    public interface IPackageDataGridView : IView
    {
        bool ArePackageContextButtonsEnabled { set; }
        IList<Model.Packages.PackageDataViewRow> SetPackageItems { set; }

    }
}
