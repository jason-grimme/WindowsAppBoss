using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Presenter.CustomData
{
    class CustomDataPresenter : Presenter<View.CustomData.ICustomDataView>
    {
        public CustomDataPresenter(View.CustomData.ICustomDataView view) : base(view)
        {
            _packageManager = new Services.Packages.PackageLocator();
        }

        public void ShowSelectedPackage()
        {
            try
            {
                if (this.SelectedPackage != null && !String.IsNullOrWhiteSpace(this.SelectedPackage.InstallationDirectory))
                {
                    string pathOfCustomData = System.IO.Path.Combine(this.SelectedPackage.InstallationDirectory, Constants.DirectoryNames.PackageMetadata, Constants.FileNames.CustomData);
                    if (System.IO.File.Exists(pathOfCustomData))
                    {
                        ViewCustomDataFile(pathOfCustomData);
                    }
                    else
                    {
                        Logger.Log(Logger.LogSeverity.Error, "Custom.data file {0} does not exist", pathOfCustomData);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Showing custom.data for selected package");
            }
        }

        public Model.Packages.PackageInformation SelectedPackage { get; set; }

        #region events
        protected override void OnViewLoad(object sender, EventArgs e)
        {
            var asyncTask = CreatePackageListAsync();
        }
        #endregion events

        #region private methods

        private bool ViewCustomDataFile(string filePath)
        {
            bool success = false;
            Process process = null;
            try
            {
                
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "notepad.exe";
                processStartInfo.Arguments = filePath;
                processStartInfo.Verb = "runas";
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                processStartInfo.UseShellExecute = true;
                process = Process.Start(processStartInfo);
                success = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Launching custom.data reader");
                success = false;
                process = null;
            }
            return success;

        }

        private async Task CreatePackageListAsync()
        {
            try
            {
                var packages = await FetchPackagesWithCustomDataAsync();
                if (packages != null && packages.Any())
                {
                    var rows = packages.Select<Model.Packages.PackageInformation, Model.Packages.PackageDataViewRow>((packageInfo) =>
                    {
                        return new Model.Packages.PackageDataViewRow(packageInfo);
                    });
                    this.View.PackagesWithCustomData = rows.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Creating list of packages with custom.data");
            }

        }


        private async Task<IEnumerable<Model.Packages.PackageInformation>> FetchPackagesWithCustomDataAsync()
        {
            var packagesWithCustomData = new List<Model.Packages.PackageInformation>();
            if (_packageManager != null)
            {
                var allPackages = await _packageManager.GetInstalledPackagesAsync();
                if (allPackages != null && allPackages.Any())
                {
                    var packages =
                        allPackages.Where(p =>
                        {
                            bool doesExist = false;
                            if (!String.IsNullOrWhiteSpace(p.InstallationDirectory))
                            {
                                string pathOfCustomData = System.IO.Path.Combine(p.InstallationDirectory, Constants.DirectoryNames.PackageMetadata, Constants.FileNames.CustomData);
                                doesExist = System.IO.File.Exists(pathOfCustomData);
                            }
                            return doesExist;
                        });
                    if (packages != null && packages.Any())
                    {
                        packagesWithCustomData.AddRange(packages);
                    }
                    
                }
            }
            return packagesWithCustomData;
        }
        #endregion private methods


        private Services.Packages.PackageLocator _packageManager;
        
    }
}
