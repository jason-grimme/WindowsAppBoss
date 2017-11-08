using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WindowsAppBoss.Utilities.Logging
{

    
    public class TraceFileLogger : ILogger
    {
        public TraceFileLogger(string pathToLogDirectory)
        {
            if (!Directory.Exists(pathToLogDirectory))
            {
                Directory.CreateDirectory(pathToLogDirectory);
            }
            else
            {
                // Delete log files older than 7 days
                (from file in new DirectoryInfo(pathToLogDirectory).GetFiles()
                 where file.CreationTime < DateTime.Now.Subtract(TimeSpan.FromDays(7))
                 where file.Name.StartsWith("Log")
                 select file).ToList().ForEach(oldFile => oldFile.Delete());
            }

            string logFile = Path.Combine(pathToLogDirectory, string.Format(@"Log_{0}.log", DateTime.Now.ToString(@"yy_MM_dd-hh-mm_ss")));
            var writer = new TextWriterTraceListener(logFile, "TraceFileLogger");
            Trace.Listeners.Add(writer);
            Trace.AutoFlush = true;

        }

        public void Log(Logger.LogSeverity severity, string message)
        {
            var text = String.Format("{0}\t\t{1}\t\t{2}", severity.ToString(), DateTime.Now.ToString(@"hh:mm:ss"), message);
            Trace.WriteLine(text);
        }

        public void Log(Exception ex, string message)
        {
            var text = String.Format("{0}\t\t{1}\t\t{2}\t\t{3}\r\n\t\t\t\t\t\t\t\t{4}",
                "Exception",
                DateTime.Now.ToString(@"hh:mm:ss"), 
                ex.GetType(), 
                message, ex.Message);
            Trace.WriteLine(text);
            
        }

    }
}
