using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppBoss.View.Installer
{
    public interface IAdvancedInstallView : IView
    {
        string DetailsText { get; set; }
        
        bool IsBrowseForFolderChecked { get; set; }
        bool IsAddDependenciesChecked { get; set; }
        bool IsProvisionedModeChecked { get; set; }
        bool IsCustomDataChecked { get; set; }
        bool IsLicenseChecked { get; set; }

        bool IsFormEnabled { get; set; }
        bool IsAddPackageButtonEnabled { get; set; }
        bool IsDependenciesOptionsEnabled { get; set; }
        bool IsProvisionedOptionsEnabled { get; set; }
        bool IsSideLoadingEnabled { set; }
        bool IsBrowseForFolderEnabled { get; set; }
        bool IsCustomDataAndLicenseEnabled { set; }
        bool IsCustomDataValuesEnabled { set; }
        bool IsLicenseValuesEnabled { set; }

        string PathToAppPackage { get; set; }
        string PathsToDependencies { get; set; }
        string PathToCustomData { get; set; }
        string PathToLicense { get; set; }

        string PackageName { get; set; }



        


    }
}
