using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Model.SnapShots;

namespace WindowsAppBoss.View.SnapShots
{
    public interface IPackageSnapShotManagerView : IView
    {
        string AppName
        {
            set;
        }

        IEnumerable<SnapShot> SnapShots
        {
            set;
        }

        string LogContent
        {
            get;
            set;
        }
    }
}
