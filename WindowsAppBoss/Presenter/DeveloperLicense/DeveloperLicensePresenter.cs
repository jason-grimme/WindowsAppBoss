using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Presenter.DeveloperLicense
{
    class DeveloperLicensePresenter : Presenter<View.DeveloperLicense.IDeveloperLicenseView>
    {
        public DeveloperLicensePresenter(View.DeveloperLicense.IDeveloperLicenseView view)
            : base(view)
        {
            _licenseAgent = new Services.Packages.DeveloperLicenseAgent();

            this.View.IsRemoveEnabled = false;
            this.View.IsAddEnabled = true;
        }

        public async Task AddDeveloperLicenseAsync()
        {
            Logger.Log(Logger.LogSeverity.Information, "Requesting developer license");
            var expirationDate = await _licenseAgent.AcquireDeveloperLicenseAsync();
            await GetLicenseAvailabilityAsync();
        }

        public async Task RemoveDeveloperLicenseAsync()
        {
            Logger.Log(Logger.LogSeverity.Information, "Removing developer license");
            bool wasRemoved = await _licenseAgent.RemoveDeveloperLicenseAsync();
            await GetLicenseAvailabilityAsync();
        }

        protected override void OnViewLoad(object sender, EventArgs e)
        {
            var asyncTask = GetLicenseAvailabilityAsync();
        }

        private async Task GetLicenseAvailabilityAsync()
        {
            try
            {
                DateTime expirationDate = await _licenseAgent.CheckDeveloperLicenseAsync();
                Utilities.Logging.Logger.Log(Logger.LogSeverity.Information, "Developer license expires on {0}", expirationDate);
                this.View.LicenseExpirationDate = expirationDate;
                this.View.IsAddEnabled = true;
                this.View.IsRemoveEnabled = (expirationDate != DateTime.MinValue);
            }
            catch (Exception ex)
            {
                Utilities.Logging.Logger.Log(ex, "Getting developer license");
            }
        }

        private readonly Services.Packages.DeveloperLicenseAgent _licenseAgent;
        
    }
}
