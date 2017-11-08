using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Model.Packages;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Services.Packages
{
    internal class PackageLocator
    {
        public Task<IEnumerable<PackageInformation>> GetInstalledPackagesAsync()
        {
            return Task.Factory.StartNew<IEnumerable<PackageInformation>>(() =>
                {
                    var listOfApps = new List<PackageInformation>();
                    try
                    {
                        var packagesDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.DirectoryNames.Name);
                        var packageManager = new Windows.Management.Deployment.PackageManager();
                        var packages = packageManager.FindPackagesForUser(System.Security.Principal.WindowsIdentity.GetCurrent().User.Value);
                        if (packages != null && packages.Any())
                        {
                            foreach (var package in packages)
                            {
                                try
                                {
                                    var app = new PackageInformation()
                                    {
                                        AppVersion = new Version(package.Id.Version.Major, package.Id.Version.Minor, package.Id.Version.Build, package.Id.Version.Revision),
                                        DataDirectory = Path.Combine(packagesDirectory, package.Id.FamilyName),
                                        FamilyName = package.Id.FamilyName,
                                        FullName = package.Id.FullName,
                                        Name = package.Id.Name,
                                        Publisher = package.Id.PublisherId,
                                    };

                                    try
                                    {
                                        if (null != package.InstalledLocation)
                                        {
                                            app.InstallationDirectory = package.InstalledLocation.Path;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Logger.Log(Logger.LogSeverity.Warning, "App package has no install location: ({0})", package.Id.FullName);
                                    }
                                    listOfApps.Add(app);
                                }
                                catch (Exception ex)
                                {
                                    Logger.Log(ex, "Error while loading information for package: ({0})", package.Id.FullName);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex, "Exception while loading list of installed packages");
                    }
                    return listOfApps;
                });
        }

        /// <summary>
        /// Retrieves the AppUserModelId for a installed app
        /// </summary>
        /// <param name="packageFullName">Package full name (ex: Microsoft.Bing_1.2.0.137_x64__8wekyb3d8bbwe)</param>
        /// <returns>AppUserModelId for the app. (ex: Microsoft.Bing_8wekyb3d8bbwe!Microsoft.Bing)</returns>
        /// <remarks>
        ///  HKEY_CLASSES_ROOT\ActivatableClasses\Package\%packageFullName%\Server\[0] : AppUserModelId
        /// </remarks>
        public static Task<string> GetAppUserModelIdAsync(string packageFullName)
        {
            return Task.Factory.StartNew<string>(() =>
                {
                    string appUserModelId = null;
                    try
                    {
                        string path = String.Format(Constants.RegistryKeys.PathToActivatableClassServer, packageFullName);
                        var serverKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(path);
                        var firstAppServerKey = serverKey.OpenSubKey(serverKey.GetSubKeyNames()[0]);
                        appUserModelId = firstAppServerKey.GetValue(Constants.RegistryValues.AppUserModelId) as string;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex, "Unable to retrieve AppUserModelId");
                    }
                    return appUserModelId;
                });
        }
    }
}
