using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Model.Packages;

namespace WindowsAppBoss.Presenter.Main
{
    public class PackageDataGridPresenter : Presenter<View.Main.IPackageDataGridView>
    {
        public PackageDataGridPresenter(View.Main.IPackageDataGridView view)
            : base(view)
            {
            }

        public PackageDataViewRow SelectedItem { get; private set;}

        #region events
        public void SelectedItemChanged(Model.Packages.PackageDataViewRow selectedPackage)
        {
            this.SelectedItem = selectedPackage;
            this.View.ArePackageContextButtonsEnabled = (SelectedItem != null);
        }

        protected override void OnViewLoad(object sender, EventArgs e)
        {
            this.View.ArePackageContextButtonsEnabled = false;
            var asyncTask = LoadPackageListAsync();
        }
        #endregion events


       
        public void UpdatePackageList()
        {
            var asyncTask = LoadPackageListAsync();
        }

        #region private methods
        private async Task LoadPackageListAsync()
        {
            try
            {
                var locator = new Services.Packages.PackageLocator();
                var installedPackages = await locator.GetInstalledPackagesAsync();
                var rows = installedPackages.Select<Model.Packages.PackageInformation, Model.Packages.PackageDataViewRow>(
                    (packageInfo) => new Model.Packages.PackageDataViewRow(packageInfo)
                );

                this.View.SetPackageItems = rows.Reverse<PackageDataViewRow>().ToList();
            }
            catch (Exception ex)
            {
                Utilities.Logging.Logger.Log(ex, "Loading packages");
            }
        }
        #endregion private methods



    }
}
