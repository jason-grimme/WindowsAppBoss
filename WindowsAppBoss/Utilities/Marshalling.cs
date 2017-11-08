using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Utilities
{
    public static class Marshalling
    {
        public static void InvokeIfNecessary(Control control, Action setValue)
        {
            try
            {
                if (control.InvokeRequired)
                {
                    control.BeginInvoke(setValue);
                }
                else
                {
                    setValue();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Unable to invoke action on control");
            }
        }
    }
}
