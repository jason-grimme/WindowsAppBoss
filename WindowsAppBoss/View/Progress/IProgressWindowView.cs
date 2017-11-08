using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppBoss.View.Progress
{
    interface IProgressWindowView : IView
    {
        string WindowTitle { set; }
        string TaskDescription { set; }
        string SubTaskDescription { set; }
        int OverallProgress { set; }
        string DetailsText { get;  set; }
    }
}
