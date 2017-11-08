using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Services.Packages
{
    internal class DismPackageManager
    {
        public DismPackageManager() : this(null)
        {
        }

        public DismPackageManager(Presenter.Progress.ProgressPresenter progressPresenter)
        {
            _progressPresenter = progressPresenter;
            this.LogLevel = DismInterop.DismLogLevel.WarningsInfo;
            this.LogPath = LocateLogFile();
        }

 
        public Task<IEnumerable<Model.Packages.PackageInformation>> GetProvisionedPackagesAsync()
        {
            return Task.Factory.StartNew<IEnumerable<Model.Packages.PackageInformation>>(GetProvisionedPackages);
        }

        public Task<bool> RemoveProvisionedPackageAsync(bool online, string packageName)
        {
            return Task.Factory.StartNew<bool>(() => 
                {
                    bool success = RemoveProvisionedPackage(online, packageName);;
                    return success;
                });
        }
        

        public Task<bool> AddProvisionedPackageAsync(
            bool online,
            Uri packagePath, 
            Uri folderPath, 
            bool skipLicense, 
            IEnumerable<Uri> listOfDependencyPackagePaths, 
            Uri licensePath, 
            Uri customDataPath)
        {
            return Task.Factory.StartNew<bool>(() =>
            {
                return AddProvisionedPackage(online, packagePath, folderPath, skipLicense, listOfDependencyPackagePaths, licensePath, customDataPath);
            });

        }

        

        #region Private Worker Methods

        private bool RemoveProvisionedPackage(bool online, string packageName)
        {
            var success = false;
            try
            {
                try
                {
                    DismOpenSession();
                    int hr = DismInterop._DismRemoveProvisionedAppxPackage(this.Session, packageName);
                    success = (hr == 0);
                    Log(true, "Dism: Remove-ProvisionedAppxPackage: {0}", LogIntResult(hr));
                }
                catch (Exception ex)
                {
                    Utilities.Logging.Logger.Log(ex, "Exception while removing provisioned app package");
                    success = false;
                }
                finally
                {
                    DismCloseSession();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Exception while removing provisioned app package");
            }
            return success;

        }

        private bool AddProvisionedPackage(
            bool online,
            Uri packagePath, 
            Uri folderPath, 
            bool skipLicense, 
            IEnumerable<Uri> listOfDependencyPackagePaths, 
            Uri licensePath, 
            Uri customDataPath)
        {
            
            bool success = false;
            try
            {
                this.Online = online;
                uint dependencyPackageCount = 0u;

                if (folderPath != null && (packagePath != null || (listOfDependencyPackagePaths != null && listOfDependencyPackagePaths.Any())))
                {
                    Log(true, "Folder path cannot be used along with Packagepath or ListOfDepnendencyPackagepaths");
                }
                if (folderPath == null && packagePath == null)
                {
                    Log(true, "Either the folder or package path must be provied");
                }
                if (skipLicense == true && licensePath != null)
                {
                    Log(true, "Cannot specify both SkipLicense and LicensePath");
                }
                if (listOfDependencyPackagePaths != null)
                {
                    dependencyPackageCount = Convert.ToUInt32(listOfDependencyPackagePaths.Count());
                }
                if (packagePath != null && !File.Exists(packagePath.AbsolutePath))
                {
                    Log(true, "Packagepath must point to a package, not a directory");
                }
                if (folderPath != null && !Directory.Exists(folderPath.AbsolutePath))
                {
                    Log(true, "FolderPath must point to a folder");
                }

                string pathToAppPackage = string.Empty;
                string[] dependencies = null;
                string pathToLicense = null;
                string pathToCustomData = null;

                pathToAppPackage = (folderPath != null) ? folderPath.OriginalString : packagePath.OriginalString;
                dependencies = (listOfDependencyPackagePaths != null && listOfDependencyPackagePaths.Any()) ? listOfDependencyPackagePaths.Select<Uri, string>(u => u.OriginalString).ToArray() : null;
                pathToLicense = (licensePath != null) ? licensePath.OriginalString : null;
                pathToCustomData = (customDataPath != null) ? customDataPath.OriginalString : null;

                try
                {
                    DismOpenSession();
                    uint sessionToken = this.Session;
                    Log(true, "DISM: Add ProvisionedAppxPackage: (Starting)");
                    Log(true, "Operation started.  Modification may take a minute.  Do not close the window.");
                    int hr = DismInterop._DismAddProvisionedAppxPackage(sessionToken, pathToAppPackage, dependencies, dependencyPackageCount, pathToLicense, skipLicense, pathToCustomData);
                    Log(true, "DISM: Add ProvisionedAppxPackage: (Finished) Result: {0}", LogIntResult(hr));
                     if (hr == 0)
                     {
                         success = true;
                     }
                }
                finally
                {
                    DismCloseSession();
                }
                _progressPresenter.OverallProgress = 100;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Exception while adding provisioned appx package");
            }
            Log(true, "DISM: Add ProvisionedAppxPackage: Complete");
            return success;
        }


        private IEnumerable<Model.Packages.PackageInformation> GetProvisionedPackages()
        {
            this.Online = true;
            IntPtr pointerToPackagesBuffer = IntPtr.Zero;
            uint numberOfPackages = 0u;
            var packages = new List<Model.Packages.PackageInformation>();
            try
            {
                DismOpenSession();

                int hr = DismInterop._DismGetProvisionedAppxPackages(this.Session, out pointerToPackagesBuffer, out numberOfPackages);

                IntPtr pointerToBuffer = pointerToPackagesBuffer;
                int sizeOfOriginalObject = Marshal.SizeOf(typeof(DismInterop.DismAppxPackage));
                int itemIndex = 0;
                while ((long)itemIndex < (long)((ulong)numberOfPackages))
                {
                    try
                    {
                        DismInterop.DismAppxPackage dismAppxPackage = (DismInterop.DismAppxPackage)Marshal.PtrToStructure(pointerToBuffer, typeof(DismInterop.DismAppxPackage));
                        if (dismAppxPackage != null)
                        {
                            int major = Convert.ToInt32(dismAppxPackage.MajorVersion);
                            int minor = Convert.ToInt32(dismAppxPackage.MinorVersion);
                            int build = Convert.ToInt32(dismAppxPackage.Build);
                            int revision = Convert.ToInt32(dismAppxPackage.Revision);
                            packages.Add(new Model.Packages.PackageInformation()
                            {
                                AppVersion = new Version(major, minor, build, revision),
                                InstallationDirectory = dismAppxPackage.InstallLocation,
                                FullName = dismAppxPackage.PackageName,
                                Name = dismAppxPackage.DisplayName,
                                Publisher = dismAppxPackage.PublisherId,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex, "Reading DismAppPackage out of memory buffer");
                    }
                    pointerToBuffer = new IntPtr(pointerToBuffer.ToInt64() + (long)sizeOfOriginalObject);
                    itemIndex++;
                }// End while

                int i = packages.Count;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Getting provisioned appx packages");
            }
            finally
            {
                if (pointerToPackagesBuffer != IntPtr.Zero)
                {
                    DismDelete(pointerToPackagesBuffer);
                }
                DismCloseSession();
            }

            return packages;
        }
        #endregion Private worker methods

        #region Private Dism Helpers

        private void DismOpenSession()
        {
            DismInitialize();
            int hr;
            if (this.Online)
            {
                uint sessionToken;
                hr = DismInterop.DismOpenSession(DismInterop.DismOnlineImage, this.WindowsDirectory, this.SystemDrive, out sessionToken);
                this.Session = sessionToken;
            }
            else
            {
                uint sessionToken;
                hr = DismInterop.DismOpenSession(this.Path, this.WindowsDirectory, this.SystemDrive, out sessionToken);
                this.Session = sessionToken;
            }
            Log(true, "DISM: Open Session: Session ID: {0}, Result:{1}", this.Session, LogIntResult(hr));
            _isSessionOpened = true;
        }

        private void DismCloseSession()
        {
            try
            {
                if (_isSessionOpened)
                {
                    int hr = DismInterop.DismCloseSession(this.Session);
                    Log(true, "DISM: Close Session: {0}", LogIntResult(hr));
                }
                DismShutdown();
            }
            catch (Exception ex)
            {
                Utilities.Logging.Logger.Log(ex, "DISM CLose session");
            }
        }

        private void DismShutdown()
        {
	        if (this.Initialized)
	        {
		        this.Initialized = false;
		        int hr = DismInterop.DismShutdown();
                Log(true, "DISM Shutdown: {0}", LogIntResult(hr));
	        }
	        if (this.SavedDirectory != null)
	        {
		        try
		        {
			        Directory.SetCurrentDirectory(this.SavedDirectory);
		        }
		        catch (Exception ex)
		        {
                    Utilities.Logging.Logger.Log(ex, "Dism Shutdown");
		        }
	        }
        }

        private void DismInitialize()
        {
            string currentDirectory;
            try
            {
                    currentDirectory = System.IO.Path.GetDirectoryName(this.LogPath);
                    if (Directory.Exists(currentDirectory))
                    {
                        Directory.SetCurrentDirectory(currentDirectory);
                        this.SavedDirectory = currentDirectory;
                    }

                    int hr = DismInterop.DismInitialize(LogLevel, this.LogPath, null);
                    Log(true, "DISM: Initialize: {0}", LogIntResult(hr));
                    Initialized = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Dism Initialize");
            }
        }

        private void DismDelete(IntPtr buffer)
        {
            int hr = DismInterop.DismDelete(buffer);
        }

        #endregion Private Dism Helpers

        #region private helpers

        public static string LogIntResult(int result)
        {
            return (result == 0) ? "Success" : ("Error: " + result.ToString());
        }
        /// <summary>
        /// Locates the path to the log file, and creates the directory if it does not exist
        /// </summary>
        /// <returns></returns>
        private string LocateLogFile()
        {
            string path = System.IO.Path.Combine(Constants.DirectoryNames.LogDirectory, Constants.FileNames.DismLog);
            string d = System.IO.Path.GetDirectoryName(path);
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
            }
            return path;
        }

        private void Log(bool append, string text)
        {
            if (_progressPresenter != null)
            {
                _progressPresenter.SetDetailsText(append, text);
            }
        }

        private void Log(bool append, string format, params object[] arg)
        {
            if (_progressPresenter != null)
            {
                _progressPresenter.SetDetailsText(append, format, arg);
            }
        } 
        #endregion private helpers

        #region DISM Interop
        private static class DismInterop
        {
            public enum DismLogLevel
            {
                Errors,
                Warnings,
                WarningsInfo
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public class DismAppxPackage
            {
                [MarshalAs(UnmanagedType.LPWStr)]
                public string PackageName;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string DisplayName;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string PublisherId;
                public uint MajorVersion;
                public uint MinorVersion;
                public uint Build;
                public uint Revision;
                public uint Architecture;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string ResourceId;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string InstallLocation;
            }

            internal static string DismOnlineImage = "DISM_{53BFAE52-B167-4E2F-A258-0A37B57FF845}";

            [DllImport("DismApi.dll")]
            public static extern int _DismAddProvisionedAppxPackage(uint Session, [MarshalAs(UnmanagedType.LPWStr)] string AppPath, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 3)] string[] DependencyPackages, uint DependencyPackageCount, [MarshalAs(UnmanagedType.LPWStr)] string LicensePath, bool SkipLicense, [MarshalAs(UnmanagedType.LPWStr)] string CustomDataPath);

            [DllImport("DismApi.dll")]
            public static extern int _DismGetProvisionedAppxPackages(uint Session, out IntPtr PackageBufPtr, out uint PackageCount);

            [DllImport("DismApi.dll")]
            public static extern int _DismRemoveProvisionedAppxPackage(uint Session, [MarshalAs(UnmanagedType.LPWStr)] string PackageName);

            [DllImport("DismApi.dll")]
            public static extern int DismCloseSession(uint Session);

            [DllImport("DismApi.dll")]
            public static extern int DismOpenSession([MarshalAs(UnmanagedType.LPWStr)] string ImagePath, [MarshalAs(UnmanagedType.LPWStr)] string WindowsDirectory, [MarshalAs(UnmanagedType.LPWStr)] string SystemDrive, out uint Session);

            [DllImport("DismApi.dll")]
            public static extern int DismInitialize(DismLogLevel LogLevel, [MarshalAs(UnmanagedType.LPWStr)] string LogFilePath, [MarshalAs(UnmanagedType.LPWStr)] string ScratchDirectory);

            [DllImport("DismApi.dll")]
            public static extern int DismShutdown();

            [DllImport("DismApi.dll")]
            public static extern int DismDelete(IntPtr DismStructure);


        }
        #endregion DISM interop

        public string Path { get; set;}
        public bool Initialized { get; private set;}
        public bool Online { get; set;}
        public uint Session { get; private set;}
        public string WindowsDirectory { get; set;}
        public string LogPath { get; set; }
        public string SystemDrive { get; set;}
        public string SavedDirectory { get; set;}
        private DismInterop.DismLogLevel LogLevel { get; set;}
        private bool _isSessionOpened = false;

        private readonly Presenter.Progress.ProgressPresenter _progressPresenter;
    }
}
