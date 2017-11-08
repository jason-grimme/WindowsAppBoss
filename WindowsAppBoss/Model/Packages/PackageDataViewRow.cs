using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppBoss.Model.Packages
{
    public class PackageDataViewRow
    {
        public PackageDataViewRow()
        {

        }

        public PackageDataViewRow(PackageInformation information)
        {
            _originalData = information;
            this.Name = information.Name;
            this.AppVersion = information.AppVersion;
            this.Publisher = information.Publisher;
            this.FamilyName = information.FamilyName;
        }

        public PackageInformation GetOriginalData()
        {
            return _originalData;
        }

        [System.ComponentModel.DisplayName("Name")]
        public string Name { get; set; }

        [System.ComponentModel.DisplayName("Version")]
        public Version AppVersion { get; set; }

        [System.ComponentModel.DisplayName("Publisher")]
        public string Publisher { get; set; }

        [System.ComponentModel.DisplayName("Family")]
        public string FamilyName { get; set; }
        
        //public string InstallationDirectory { get; set; }
        //public string DataDirectory { get; set; }
        //public string FullName { get; set; }



        //public static implicit operator Model.Packages.PackageInformation(PackageDataViewRow source)
        //{
        //    return new PackageInformation()
        //    {
        //        Name = source.Name,
        //        AppVersion = source.AppVersion,
        //        Publisher = source.Publisher,
        //        FamilyName = source.FamilyName,
        //    };
        //}

        ///// <summary>
        ///// Convert from PackageDataViewRow to PackageInformation
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //public static explicit operator Model.Packages.PackageInformation(PackageDataViewRow source)
        //{
        //    return new PackageInformation()
        //    {
        //        AppVersion = source.AppVersion,
        //        FamilyName = source.FamilyName,
        //        Name = source.Name,
        //        Publisher = source.Publisher,
        //    };
        //}

        ///// <summary>
        ///// Convert from PackageInformation to PackageDataViewRow
        ///// </summary>
        ///// <param name="packageInformation"></param>
        ///// <returns></returns>
        //public static explicit operator PackageDataViewRow(PackageInformation packageInformation)
        //{
        //    return new PackageDataViewRow()
        //    {
        //        AppVersion = packageInformation.AppVersion,
        //        FamilyName = packageInformation.FamilyName,
        //        Name = packageInformation.Name,
        //        Publisher = packageInformation.Publisher,
        //    };
        //}


        private PackageInformation _originalData;

    }
}
