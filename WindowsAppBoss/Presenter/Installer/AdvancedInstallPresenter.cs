using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Presenter.Installer
{
    class AdvancedInstallPresenter : Presenter<View.Installer.IAdvancedInstallView>
    {
        public AdvancedInstallPresenter(View.Installer.IAdvancedInstallView view)
            : base(view)
        {
            _licenseAgent = new Services.Packages.DeveloperLicenseAgent();
        }

        public void OnSideLoadingButtonCick()
        {
            var asyncTask = ToggleSideLoadingAbilityAsync();
        }

        public void FormValueHasChanged()
        {
            AdjustAddButtonAbleness();
            AdjustElementAbleness();
        }

        public async Task OnAddPackageButtonClick()
        {
            IsWorking = true;
            var progressPresenter = new Presenter.Progress.ProgressPresenter( (View.Progress.IProgressWindowView)this.View)
                {
                    WindowTitle = "Add App Package",
                    TaskDescription = String.Format("Adding App Package {0}", View.PathToAppPackage)
                };

            var packageManager = new Services.Packages.PackageManager();
            bool success = false;

            bool isDataValid = false;
            Uri packageLocation = null;
            bool skipLicense = false;
            IEnumerable<Uri> dependencies  = null;
            Uri licensePath = null;
            Uri customDataPath = null;
            string packageName = null;

            try
            {
                packageLocation = new Uri(View.PathToAppPackage);
                skipLicense = (!View.IsLicenseChecked);
                dependencies = GatherDependencyUrls();
                licensePath = (View.IsProvisionedModeChecked && View.IsLicenseChecked) ? new Uri(View.PathToLicense) : null;
                customDataPath = (View.IsProvisionedModeChecked && View.IsCustomDataChecked) ? new Uri(View.PathToCustomData) : null;
                isDataValid = true;
                packageName = View.PackageName;
            }
            catch (Exception ex)
            {
                isDataValid = false;
                Logger.Log(Logger.LogSeverity.Error, "A portion of the add package form is invalid");
                throw new Exception("A portion of the form is invalid", ex); 
            }

            try
            {
                if (isDataValid && View.IsProvisionedModeChecked)
                {
                    success = await packageManager.AddProvisionedPackageAsync(packageLocation, packageName, skipLicense, dependencies, licensePath, customDataPath, progressPresenter);
                    Logger.Log(Logger.LogSeverity.Information, "Adding provisioned appx package {0}.  Result: ({1})", packageLocation.AbsolutePath, success);
                }
                else if(isDataValid)
                {
                    success = await packageManager.AddPackageAsync(packageLocation, dependencies, progressPresenter);
                    Logger.Log(Logger.LogSeverity.Information, "Adding appx package {0}.  Result: ({1})", packageLocation.AbsolutePath, success);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "An exception occured while adding appx package");
            }

            IsWorking = false;
            

        }

  


        protected override void OnViewLoad(object sender, EventArgs e)
        {
            var asyncTask = CheckSideLoadingAblenessAsync();
        }

        private async Task CheckSideLoadingAblenessAsync()
        {
            if (_licenseAgent != null)
            {
                try
                {
                    var isEnabled = await _licenseAgent.GetSideloadingPolicyAsync();
                    this.View.IsSideLoadingEnabled = isEnabled;
                }
                catch (Exception ex)
                {
                    Logger.Log(ex, "Checking side loading policy");
                }
            }
        }

        private async Task ToggleSideLoadingAbilityAsync()
        {
            this.IsWorking = true;
            if (_licenseAgent != null)
            {
                bool isEnabled = await _licenseAgent.GetSideloadingPolicyAsync();
                bool success = await _licenseAgent.SetSideloadingPolicyAsync(!isEnabled);
                Logger.Log(Logger.LogSeverity.Information, "Update AllowAllTrustedApps to {0}.  Result:{1}", !isEnabled, success);
                if (success)
                {
                    await CheckSideLoadingAblenessAsync();
                }
                else
                {
                    Logger.Log(Logger.LogSeverity.Error, "Unable to update AllowAllTrustedApps to {0} ", !isEnabled);
                }
            }
            this.IsWorking = false;
        }


        private IEnumerable<Uri> GatherDependencyUrls()
        {
            var dependencies = new List<Uri>();
            if (View.IsAddDependenciesChecked && !String.IsNullOrWhiteSpace(View.PathsToDependencies))
            {
                var lines = View.PathsToDependencies.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var uris = lines.Select<string, Uri>((line) =>
                {
                    try
                    {
                        return new Uri(line);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                });
                dependencies.AddRange(uris.Where(i => i != null));
            }
            return dependencies;
        }



        #region Element Ableness

        private void AdjustElementAbleness()
        {
            View.IsBrowseForFolderEnabled = View.IsProvisionedModeChecked;
            View.IsProvisionedOptionsEnabled = (View.IsProvisionedModeChecked);

            View.IsDependenciesOptionsEnabled = View.IsAddDependenciesChecked;

            View.IsCustomDataAndLicenseEnabled = !View.IsBrowseForFolderChecked;
            View.IsCustomDataValuesEnabled = View.IsCustomDataChecked;
            View.IsLicenseValuesEnabled = View.IsLicenseChecked;
        }

        private void AdjustAddButtonAbleness()
        {
            bool isEnabled = !this.IsWorking;

            if (String.IsNullOrWhiteSpace(View.PathToAppPackage))
            {
                isEnabled = false;
            }

            if (View.IsAddDependenciesChecked && String.IsNullOrWhiteSpace(View.PathsToDependencies))
            {
                isEnabled = false;
            }

            if ((View.IsProvisionedOptionsEnabled && View.IsProvisionedModeChecked) && View.IsCustomDataChecked && String.IsNullOrWhiteSpace(View.PathToCustomData))
            {
                isEnabled = false;
            }

            if ((View.IsProvisionedOptionsEnabled && View.IsProvisionedModeChecked) && View.IsLicenseChecked && String.IsNullOrWhiteSpace(View.PathToLicense))
            {
                isEnabled = false;
            }

            View.IsAddPackageButtonEnabled = isEnabled;
        }

        #endregion Element Ableness

        private bool IsWorking { 
            get
            {
                return _isWorking;
            }
            set
            {
                _isWorking = value;
                View.IsFormEnabled = !value;
            }
        }
        private bool _isWorking = false;

        private readonly Services.Packages.DeveloperLicenseAgent _licenseAgent;
        
    }
}
