using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Services.Packages
{
    /// <summary>
    /// Wrapper for calling DISM.exe
    /// <seealso cref="http://technet.microsoft.com/en-us/library/hh824882.aspx"/>
    /// </summary>
    class DismWrapper
    {
        public DismWrapper(Presenter.Progress.ProgressPresenter progressPresenter)
        {
            _progressPresenter = progressPresenter;
        }

        /// <summary>
        /// This option will only remove the provisioning for a package if it is 
        /// registered to any user profile. Use the Remove-AppxPackage cmdlet 
        /// in Windows PowerShell to remove the app for each user that it is already
        /// registered to.
        /// If the app has not been registered to any user profile, 
        /// the /Remove-ProvisionedAppxPackage option will remove the package completely.
        /// </summary>
        /// <returns></returns>
        public Task<bool> RemoveProvisionedAppxPackageAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// Adds one or more packages to the image.

        /// Use /FolderPath to specify a folder of unpacked package files containing
        /// a main package, any dependency packages, and the license file, or use
        /// /PackagePath to specify a .appx file. You can use /PackagePath when
        /// provisioning a line-of-business app online.

        /// If the package has dependencies that are architecture-specific, you must
        /// install all of the applicable architectures for the dependency on the target
        /// image. For example, on an x64 target image, include a path to both the x86
        /// and x64 dependency packages or include them both in the app package (.appx)
        /// folder.
        /// Use /CustomDataPath to specify the OEM custom data for the application.
        /// Use /LicensePath with the /PackagePath option to specify the location of the
        /// .xml file containing your application license.

        /// Only use /SkipLicense with apps that do not require a license on a
        /// sideloading-enabled computer. Using /SkipLicense in other
        /// scenarios can compromise an image.
        /// </summary>
        /// <param name="online"></param>
        /// <param name="packagePath"></param>
        /// <param name="folderPath"></param>
        /// <param name="skipLicense"></param>
        /// <param name="listOfDependencyPackagePaths"></param>
        /// <param name="licensePath"></param>
        /// <param name="customDataPath"></param>
        /// <returns></returns>
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
                    bool success = false;
                    if (folderPath != null && (packagePath != null || (listOfDependencyPackagePaths != null && listOfDependencyPackagePaths.Any())))
                    {
                        _progressPresenter.SetDetailsText(true, "Folder path cannot be used along with Packagepath or ListOfDepnendencyPackagepaths");
                    }
                    if (folderPath == null && packagePath == null)
                    {
                        _progressPresenter.SetDetailsText(true, "Either the folder or package path must be provied");
                    }
                    if (skipLicense == true && licensePath != null)
                    {
                        _progressPresenter.SetDetailsText(true, "Cannot specify both SkipLicense and LicensePath");
                    }
                    if (packagePath != null && !File.Exists(packagePath.AbsolutePath))
                    {
                        _progressPresenter.SetDetailsText(true, "Packagepath must point to a package, not a directory");
                    }
                    if (folderPath != null && !Directory.Exists(folderPath.AbsolutePath))
                    {
                        _progressPresenter.SetDetailsText(true, "FolderPath must point to a folder");
                    }
                    try
                    {
                        var startInfo = new System.Diagnostics.ProcessStartInfo()
                        {
                            CreateNoWindow = false,
                            UseShellExecute = false,
                            FileName = Path.Combine(
                                Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                                Constants.FileNames.PathToDism),
                            RedirectStandardOutput = true,
                            RedirectStandardInput = true,
                            WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                        };

                        string arguments = GetDismArguments(online, packagePath, folderPath, skipLicense, listOfDependencyPackagePaths, licensePath, customDataPath);
                        _progressPresenter.SetDetailsText(true, arguments);

                        startInfo.Arguments = arguments;
                        var process = System.Diagnostics.Process.Start(startInfo);
                        string result = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                        _progressPresenter.SetDetailsText(true, result);
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        _progressPresenter.SetDetailsText(true, "Exception: {0}", ex.Message);
                        success = false;
                        Logger.Log(ex, "Add provisioned appx package (wrapper)");
                    }
                    finally
                    {
                    }
                    _progressPresenter.OverallProgress = 100;
                    _progressPresenter.SetDetailsText(true, "Complete!");
                    return success;
                });
        }


        private string GetDismArguments(
         bool online,
         Uri packagePath,
         Uri folderPath,
         bool skipLicense,
         IEnumerable<Uri> listOfDependencyPackagePaths,
         Uri licensePath,
         Uri customDataPath)
        {
            var arguments = string.Empty;

            if (online)
            {
                arguments += String.Format(DismArguments.ArgumentFlagFormat, DismArguments.Online);
            }

            arguments += String.Format(DismArguments.ArgumentFlagFormat, DismArguments.AddProvisionedAppxPackageCommand);

            if (packagePath != null)
            {
                arguments += String.Format(DismArguments.ArgumentValueFormat, DismArguments.PackagePath, packagePath.AbsolutePath);
            }
            else if (folderPath != null)
            {
                arguments += String.Format(DismArguments.ArgumentValueFormat, DismArguments.FolderPath, folderPath.AbsolutePath);
            }
            if (skipLicense)
            {
                arguments += String.Format(DismArguments.ArgumentFlagFormat, DismArguments.SkipLicense);
            }
            else if (licensePath != null)
            {
                arguments += String.Format(DismArguments.ArgumentValueFormat, DismArguments.LicensePath, licensePath.AbsolutePath);
            }

            if (customDataPath != null)
            {
                arguments += String.Format(DismArguments.ArgumentValueFormat, DismArguments.CustomDataPath, customDataPath.AbsolutePath);
            }
            if (listOfDependencyPackagePaths != null && listOfDependencyPackagePaths.Any())
            {
                foreach (var dependencyPath in listOfDependencyPackagePaths)
                {
                    arguments += String.Format(DismArguments.ArgumentValueFormat, DismArguments.DependencyPackagePath, dependencyPath.AbsolutePath);
                }
            }
            arguments += String.Format(DismArguments.ArgumentValueFormat, DismArguments.LogPath, LocatetLogFile());

            return arguments;
        }


        private readonly Presenter.Progress.ProgressPresenter _progressPresenter;




        /// <summary>
        /// Locates the path to the log file, and creates the directory if it does not exist
        /// </summary>
        /// <returns></returns>
        private string LocatetLogFile()
        {
            string path = System.IO.Path.Combine(Constants.DirectoryNames.LogDirectory, Constants.FileNames.DismLog);
            string d = Path.GetDirectoryName(path);
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            return path;
        }


        private class DismArguments
        {
            public const string AddProvisionedAppxPackageCommand = @"Add-ProvisionedAppxPackage";
            public const string ArgumentFlagFormat = @" /{0}";
            public const string ArgumentValueFormat = @" /{0}:{1}";
            public const string Online = @"Online";
            public const string FolderPath = @"FolderPath";
            public const string SkipLicense = @"SkipLicense";
            public const string CustomDataPath = @"CustomDataPath";
            public const string PackagePath = @"PackagePath";
            public const string DependencyPackagePath = @"DependencyPackagePath";
            public const string LicensePath = @"LicensePath";
            public const string LogPath = @"LogPath";
        }

    }
}
