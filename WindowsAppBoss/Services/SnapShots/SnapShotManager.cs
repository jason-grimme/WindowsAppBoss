using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsAppBoss.Model.Packages;
using WindowsAppBoss.Model.SnapShots;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Services.SnapShots
{
    public class SnapShotManager
    {



        #region Public tasks
        public Task<IEnumerable<SnapShot>> GetSnapShotsAsync(PackageInformation package)
        {
            return Task.Factory.StartNew<IEnumerable<SnapShot>>(() =>
            {
                var list = new List<SnapShot>();
                string appDataPath = GetPathForUserDataStorage(package);
                list.AddRange(GetSnapShots(appDataPath));
                return list;
            });
        }

        public Task<bool> DoesSnapShotAlreadyExistAsync(PackageInformation package, string snapShotName)
        {
            return Task.Factory.StartNew<bool>(() =>
                {
                    bool doesExist = false;
                    var basePath = GetPathForUserDataStorage(package);
                    string pathToSnapShot = Path.Combine(basePath, snapShotName);
                    if (Directory.Exists(pathToSnapShot))
                    {
                        doesExist = true;
                    }
                    return doesExist;
                });
        }

        public Task<string> GenerateNewSnapShotAsync(PackageInformation package, string snapShotName)
        {
            return Task.Factory.StartNew<string>(() =>
                {
                    string pathToSnapShot = Path.Combine(GetPathForUserDataStorage(package), snapShotName);
                    Directory.CreateDirectory(pathToSnapShot);
                    string liveSettings = Path.Combine(package.DataDirectory, Constants.DirectoryNames.Settings);
                    string liveLocal = Path.Combine(package.DataDirectory, Constants.DirectoryNames.LocalState);
                    string savedSettings = Path.Combine(pathToSnapShot, Constants.DirectoryNames.Settings);
                    string savedLocal = Path.Combine(pathToSnapShot, Constants.DirectoryNames.LocalState);
                    DirectoryCopy(liveSettings, savedSettings, true, true);
                    DirectoryCopy(liveLocal, savedLocal, true, true);
                    return pathToSnapShot;
                });
        }

        public Task<bool> RemoveSnapShotAsync(PackageInformation package, string snapShotName)
        {
            return Task.Factory.StartNew<bool>(() =>
                {
                    if (package != null && !String.IsNullOrWhiteSpace(snapShotName))
                    {
                        string pathToSnapShot = Path.Combine(GetPathForUserDataStorage(package), snapShotName);
                        if (Directory.Exists(pathToSnapShot))
                        {
                            DeleteDirectory(pathToSnapShot, true);
                        }
                    }
                    return true;
                });
        }

        public Task<bool> InjectSnapShotAsync(PackageInformation package, string snapShotName)
        {
            return Task.Factory.StartNew<bool>(() =>
                {
                    try
                    {
                        if (package != null && !String.IsNullOrWhiteSpace(snapShotName))
                        {
                            string pathToSnapShot = Path.Combine(GetPathForUserDataStorage(package), snapShotName);
                            if (Directory.Exists(pathToSnapShot))
                            {
                                string liveSettings = Path.Combine(package.DataDirectory, Constants.DirectoryNames.Settings);
                                string liveLocal = Path.Combine(package.DataDirectory, Constants.DirectoryNames.LocalState);
                                string savedSettings = Path.Combine(pathToSnapShot, Constants.DirectoryNames.Settings);
                                string savedLocal = Path.Combine(pathToSnapShot, Constants.DirectoryNames.LocalState);
                                if (Directory.Exists(savedSettings) && Directory.Exists(liveSettings))
                                {
                                    DeleteDirectory(liveSettings, true);
                                }
                                if (Directory.Exists(savedLocal) && Directory.Exists(liveLocal))
                                {
                                    DeleteDirectory(liveLocal, true);
                                }

                                DirectoryCopy(savedSettings, liveSettings, true, true);
                                DirectoryCopy(savedLocal, liveLocal, true, true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex, "Injecting snap shot {0}", snapShotName);
                    }
                    return true;
                });
        }
        #endregion public


        #region private

        private static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }

        private static IEnumerable<SnapShot> GetSnapShots(string storageLocation)
        {
            var list = new List<SnapShot>();
            try
            {
                if(Directory.Exists(storageLocation))
                {
                    var snapShotDirectories = Directory.EnumerateDirectories(storageLocation);
                    if(snapShotDirectories != null && snapShotDirectories.Any())
                    {
                        foreach(var directoryPath in snapShotDirectories)
                        {
                            list.Add( new SnapShot()
                            {
                                Name = Path.GetFileName(directoryPath),
                                FullPath = directoryPath,
                                DateCreated = Directory.GetCreationTime(directoryPath),
                                Size = (GetDirectorySize(directoryPath) / 1024),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Getting snap shots under {0}", storageLocation);
            }

            return list;
        }


        private static string GetPathForUserDataStorage(PackageInformation package)
        {
            string path = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                Constants.DirectoryNames.WindowsAppBoss, 
                Constants.DirectoryNames.SnapShots, 
                package.Name
            );
            if (!System.IO.File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }



        private static void DeleteDirectory(string path, bool recurse)
        {
            try
            {
                Directory.Delete(path, recurse);
            }
            catch (IOException)
            {
                Thread.Sleep(0);
                Directory.Delete(path, recurse);
            }
        }

        private static void DirectoryCopy(string sourcePath, string destinationPath, bool recursive, bool preservePermissions)
        {
            if (Directory.Exists(sourcePath))
            {
                DirectoryInfo sourceInfo = new DirectoryInfo(sourcePath);
                DirectoryInfo destinationInfo = new DirectoryInfo(destinationPath);
                DirectoryInfo[] sourceSubFolders = sourceInfo.GetDirectories();
                FileInfo[] sourceFiles = sourceInfo.GetFiles();

                if (!sourceInfo.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourcePath);
                }

                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }

                // Copy directory security
                if (preservePermissions)
                {
                    var security = sourceInfo.GetAccessControl();
                    security.SetAccessRuleProtection(true, true);
                    destinationInfo.SetAccessControl(security);
                }

                
                foreach (FileInfo sourceFile in sourceFiles)
                {
                    string destinationFilePath = Path.Combine(destinationPath, sourceFile.Name);
                    sourceFile.CopyTo(destinationFilePath, false);

                    // Copy file security
                    if (preservePermissions)
                    {
                        var security = sourceFile.GetAccessControl();
                        security.SetAccessRuleProtection(true, true);
                        File.SetAccessControl(destinationFilePath, security);
                    }

                }

                if (recursive)
                {
                    foreach (DirectoryInfo soiurceSubDirectory in sourceSubFolders)
                    {
                        string temppath = Path.Combine(destinationPath, soiurceSubDirectory.Name);
                        DirectoryCopy(soiurceSubDirectory.FullName, temppath, recursive, preservePermissions);
                    }
                }
            }
        }
        #endregion private methods
    }
}
