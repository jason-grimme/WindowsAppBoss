using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Model.Packages;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Presenter.Packages
{
    class ProvisionedPackageManagerPresenter : Presenter<View.Packages.IProvisionedPackageManagerView>
    {
        public ProvisionedPackageManagerPresenter(View.Packages.IProvisionedPackageManagerView view)
            : base(view)
        {
            _packageManager = new Services.Packages.DismPackageManager();
        }

        #region events
        protected override void OnViewLoad(object sender, EventArgs e)
        {
            var asyncTask = LoadPackageListAsync();
        }

        public Model.Packages.PackageInformation SelectedRow { get; set; }

        public void OnRemoveButtonClick()
        {
            if (SelectedRow != null)
            {
                var asyncTask = RemovePackageAsync(SelectedRow.FullName);
            }
        }

        public void OnRefreshButtonClick()
        {
            View.ListOfPackages = new List<PackageInformation>();
            var asyncTask = LoadPackageListAsync();
        }
        #endregion events


        private async Task RemovePackageAsync(string packageName)
        {
            if (!String.IsNullOrWhiteSpace(packageName))
            {
                View.IsFormEnabled = false;
                try
                {
                    bool success = await _packageManager.RemoveProvisionedPackageAsync(true, packageName);
                    Logger.Log(Logger.LogSeverity.Information, "Removing provisioned packages ({0}).  Result: {1}", packageName, success);
                    if (success)
                    {
                        await LoadPackageListAsync();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex, "Removing provisioned package {0}", packageName);
                }
                View.IsFormEnabled = true;
               
            }
        }


        private async Task LoadPackageListAsync()
        {
            View.IsFormEnabled = false;
            try
            {
                
                var packages = await _packageManager.GetProvisionedPackagesAsync();
                if (packages != null && packages.Any())
                {
                    this.View.ListOfPackages = packages.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Loading provisioned package list");
            }
            View.IsFormEnabled = true;
        }

        private Services.Packages.DismPackageManager _packageManager;

    }
}
