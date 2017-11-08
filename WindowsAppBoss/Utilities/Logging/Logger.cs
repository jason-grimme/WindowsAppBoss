using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppBoss.Utilities.Logging
{
    public class Logger 
    {
        public enum LogSeverity
        {
            Information,
            Warning,
            Error
        }

        public static void Log(Logger.LogSeverity severity, string message)
        {
            Instance.Log(severity, message);
        }

      

        public static void Log(Exception ex, string message)
        {
            Instance.Log(ex, message);
        }

       

        #region format overloads
        public static void Log(Exception ex, string format, params object[] args)
        {
            Instance.Log(ex, String.Format(format, args));
        }

        public static void Log(Logger.LogSeverity severity, string format, params object[] args)
        {
            Instance.Log(severity, String.Format(format, args));
        }
        #endregion format overloads

        private static ILogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    //System.Windows.Forms.MessageBox.Show("New loger");

                    _instance = new TraceFileLogger(Constants.DirectoryNames.LogDirectory);
                }
                return _instance;
            }
        }

        private static ILogger _instance;

    }
}
