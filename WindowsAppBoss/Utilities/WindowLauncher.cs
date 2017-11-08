using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Utilities
{
    /// <summary>
    /// Launches windows, and handles re-launching app for windows that require elevation
    /// </summary>
    internal class WindowLauncher
    {
        /// <summary>
        /// Launches a new window, and launches new exe with admin privileges if necessary
        /// </summary>
        /// <param name="typeOfWindow"></param>
        /// <param name="requiresElevation">If the new window will need  elevated privileges</param>
        /// <returns>An indicator of whether or not the window was launched</returns>
        public bool LaunchWindow(Type typeOfWindow, bool requiresElevation)
        {
            return ReLaunchProcessIfNecessary(typeOfWindow.ToString(), typeOfWindow.Assembly.FullName, requiresElevation);
        }

        /// <summary>
        /// Looks at startup arguments and determines if a window was requested to be launched
        /// Launches window if requested
        /// </summary>
        /// <param name="arguments">arguments provided to Main()</param>
        /// <returns>An indicator of whether or not the window was launched</returns>
        public static bool LaunchWindowFromStartupArguments(string[] arguments)
        {
            bool windowLaunched = false;
            try
            {
                if (arguments != null && arguments.Any())
                {
                    Utilities.Logging.Logger.Log(Logger.LogSeverity.Information, "App started with arguments " + String.Join(", ", arguments));

                    string windowParam = arguments.FirstOrDefault(a => a.StartsWith(Constants.WindowParameter));
                    string assemblyParam = arguments.FirstOrDefault(a => a.StartsWith(Constants.AssemblyParameter));
                    if (!String.IsNullOrWhiteSpace(windowParam) && !String.IsNullOrWhiteSpace((assemblyParam)))
                    {
                        string windowValue = windowParam.Substring(windowParam.IndexOf(':') +1).Trim('"');
                        string assemblyValue = assemblyParam.Substring(assemblyParam.IndexOf(':') + 1).Trim('"');
                        windowLaunched = InstantiateWindow(windowValue, assemblyValue, true);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Launching window from startup arguments: {0}", String.Join(", ", arguments));
                windowLaunched = false;
            }
            return windowLaunched;
        }

        /// <summary>
        /// Determines if the current process is running with elevated privileges
        /// </summary>
        /// <returns></returns>
        public static bool IsRunningWithElevatedPrivileges()
        {
            bool isAdmin = false;
            var identity = WindowsIdentity.GetCurrent();
            if (identity != null)
            {
                var principal = new WindowsPrincipal(identity);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return isAdmin;
        }

        /// <summary>
        /// Examines a request for a new window and determines if the current privileges match the desired privileges
        /// </summary>
        /// <param name="typeOfWindow">type of window</param>
        /// <param name="assemblyName">assembly containing window type</param>
        /// <param name="requiresElevation">Whether or not the window needs elevation</param>
        /// <returns>An indicator of whether or not the window was launched</returns>
        private static bool ReLaunchProcessIfNecessary(string typeOfWindow, string assemblyName, bool requiresElevation)
        {
            bool success = false;
            try
            {
                var evaluatedType = DetermineType(typeOfWindow, assemblyName);
                if (evaluatedType != null)
                {
                    bool isCurrentlyAdmin = IsRunningWithElevatedPrivileges();
                        // If needs admin and already is admin
                    if (requiresElevation && isCurrentlyAdmin)
                    {
                        success = InstantiateWindow(typeOfWindow, assemblyName);
                    }
                        // If doesn't need admin but already is admin
                    else if (requiresElevation == false && isCurrentlyAdmin)
                    {
                        string pathToExe = GetPathToSelf();
                        string arguments = String.Format(Constants.ParameterFormat, typeOfWindow, assemblyName);
                        success = LaunchNewProcess(pathToExe, arguments, false);
                    }
                        // If needs admin but isn't admin
                    else if (requiresElevation && isCurrentlyAdmin == false)
                    {
                        string pathToExe = GetPathToSelf();
                        string arguments = String.Format(Constants.ParameterFormat, typeOfWindow, assemblyName);
                        success = LaunchNewProcess(pathToExe, arguments, true);
                    }
                        // Does not need admin, and we aren't admin
                    else if (requiresElevation == false && isCurrentlyAdmin == false)
                    {
                        success = InstantiateWindow(typeOfWindow, assemblyName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Relaunch window if necessary {0}", typeOfWindow);
                success = false;
            }
            return success;

        }

        /// <summary>
        /// Launches a new process with or without admin privileges
        /// </summary>
        /// <remarks>*.vshost.exe processes will launch the respective non-VsHost exe</remarks>
        /// <param name="pathToExe">Complete path to the exe to launch</param>
        /// <param name="arguments">Arguments for exe</param>
        /// <param name="launchWithElevation">Whether or not to launch with elevated privileges</param>
        /// <returns>An indicator of whether or not the window was launched</returns>
        private static bool LaunchNewProcess(string pathToExe, string arguments, bool launchWithElevation)
        {
            bool success = false;

            if (!String.IsNullOrWhiteSpace(pathToExe))
            {
                // Detect if running in vshost wrapper
                // Cannot launch multiples
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    if (Constants.VsHostExtension == System.IO.Path.GetExtension(System.IO.Path.GetFileNameWithoutExtension(pathToExe)))
                    {
                        Debug.WriteLine("Starting non-VsHost version of exe.  Code will not be debugged");
                        string path = pathToExe.Replace(Constants.VsHostExtension, "");
                        pathToExe = (System.IO.File.Exists(path)) ? path : pathToExe;
                    }
                }

                try
                {
                    var processStartInfo = new ProcessStartInfo();
                    processStartInfo.FileName = pathToExe;
                    processStartInfo.Arguments = arguments;
                    if (launchWithElevation)
                    {
                        processStartInfo.Verb = "runas";
                    }
                    processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    processStartInfo.UseShellExecute = true;
                    Process.Start(processStartInfo);
                    success = true;
                }
                catch (Exception ex)
                {
                    Logger.Log(ex, "Launching new process Admin:{0} File:{1} Args:{2}", launchWithElevation, pathToExe, arguments);
                    success = false;
                }
            }
            return success;
        }

        /// <summary>
        /// Instantiates a new window of the desired type
        /// </summary>
        /// <param name="typeOfWindow">type of window</param>
        /// <param name="assemblyName">assembly that contains window</param>
        /// <param name="isApplicationStartup">If this will be the main app window</param>
        /// <returns>An indicator of whether or not the window was launched</returns>
        private static bool InstantiateWindow(string typeOfWindow, string assemblyName, bool isApplicationStartup = false)
        {
            Form form = null;
           
            var type = DetermineType(typeOfWindow, assemblyName);
            Utilities.Logging.Logger.Log(Logger.LogSeverity.Information, "Evaluated start window as " + type.ToString());
            if (type != null)
            {
                form = Activator.CreateInstance(type) as Form;
                if (form != null)
                {
                    if (isApplicationStartup)
                    {
                        Application.Run(form);
                    }
                    else
                    {
                        form.Show();
                    }
                }
            }
            return form != null;
        }

        /// <summary>
        /// Resolves the type object of the supplied type and assembly
        /// </summary>
        /// <param name="typeOfWindow">type of window</param>
        /// <param name="assemblyName">assembly that contains window</param>
        /// <returns>The resolved type</returns>
        private static Type DetermineType(string typeOfWindow, string assemblyName)
        {
            Type type = null;
            try
            {
                string fullName = String.Format("{0}, {1}", typeOfWindow, assemblyName);
                type = Type.GetType(fullName);
            }
            catch (Exception)
            {
                type = null;
            }
            return type;
        }

        /// <summary>
        /// Locates the current process executable
        /// </summary>
        /// <returns>Full path to current exe</returns>
        private static string GetPathToSelf()
        {
            return Process.GetCurrentProcess().MainModule.FileName;
        }


        private static class Constants
        {
            public const string VsHostExtension = ".vshost";
            public const string WindowParameter = @"/windowType";
            public const string AssemblyParameter = @"/assembly";
            public const string ParameterFormat = (Constants.WindowParameter + ":\"{0}\" " + Constants.AssemblyParameter + ":\"{1}\"");
            

        }
    }
}
