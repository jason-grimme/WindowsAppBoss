using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Services.Packages
{
/// <summary>
/// Manages Windows developer license
/// </summary>
/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/jj572812(v=vs.85).aspx</remarks>
public class DeveloperLicenseAgent
{
    /// <summary>
    /// Acquires a developer license.
    /// </summary>
    /// <param name="hwnd">The handler to the parent window.</param>
    /// <param name="filetime">Indicates when the developer license expires.</param>
    /// <returns>Returns an HResult structure with any error codes that occurred.</returns>
    [DllImport("WSClient.dll", EntryPoint = "AcquireDeveloperLicense", SetLastError = true)]
    private static extern int AcquireDeveloperLicense(IntPtr hwnd, out System.Runtime.InteropServices.ComTypes.FILETIME filetime);

    /// <summary>
    /// Checks to see if a developer license exists.
    /// </summary>
    /// <param name="filetime">Indicates when the developer license expires.</param>
    /// <returns>Returns an HResult structure with any error codes that occurred.</returns>
    [DllImport("WSClient.dll", EntryPoint = "CheckDeveloperLicense", SetLastError = true)]
    private static extern int CheckDeveloperLicense(out System.Runtime.InteropServices.ComTypes.FILETIME filetime);

    /// <summary>
    /// Removes a developer license.
    /// </summary>
    /// <param name="hwnd">The handler to the parent window.</param>
    /// <returns>Returns an HResult structure with any error codes that occurred.</returns>
    [DllImport("WSCLient.dll", EntryPoint = "RemoveDeveloperLicense", SetLastError = true)]
    private static extern int RemoveDeveloperLicense(IntPtr hwnd);



    /// <summary>
    /// Acquires a developer license.
    /// </summary>
    /// <returns>Date of license expiration</returns>
    public Task<DateTime> AcquireDeveloperLicenseAsync()
    {
        return Task.Factory.StartNew<DateTime>(() =>
        {
            DateTime dateOfExpiration = DateTime.MinValue;
            System.Runtime.InteropServices.ComTypes.FILETIME fileTime;
            int hResult = DeveloperLicenseAgent.AcquireDeveloperLicense(IntPtr.Zero, out fileTime);
            long adjustedFileTime = (((long)fileTime.dwHighDateTime) << 32) + fileTime.dwLowDateTime;
            dateOfExpiration = DateTime.FromFileTime(adjustedFileTime);
            return dateOfExpiration;
        });
    }

    /// <summary>
    /// Checks to see if a developer license exists.
    /// </summary>
    /// <returns>Date of license expiration.  DateTime.MinValue if does not exist</returns>
    public Task<DateTime> CheckDeveloperLicenseAsync()
    {
        return Task.Factory.StartNew<DateTime>(() =>
        {
            DateTime dateOfExpiration = DateTime.MinValue;
            System.Runtime.InteropServices.ComTypes.FILETIME fileTime;
            int hResult = DeveloperLicenseAgent.CheckDeveloperLicense(out fileTime);
            long adjustedFileTime = (((long)fileTime.dwHighDateTime) << 32) + fileTime.dwLowDateTime;

            dateOfExpiration = (adjustedFileTime == 0) ? DateTime.MinValue : DateTime.FromFileTime(adjustedFileTime);
            return dateOfExpiration;
        });
    }

    /// <summary>
    /// Removes a developer license.
    /// </summary>
    /// <returns>Indicator of success</returns>
    public Task<bool> RemoveDeveloperLicenseAsync()
    {
        return Task.Factory.StartNew<bool>(() =>
        {
            int hResult = DeveloperLicenseAgent.RemoveDeveloperLicense(IntPtr.Zero);
            return true;
        });
    }

    public Task<bool> GetSideloadingPolicyAsync()
    {
        return Task.Factory.StartNew<bool>(() =>
        {
            bool isEnabled = false;
            try
            {
                var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(Constants.RegistryKeys.PathToEnableSideLoading);
                if (key != null)
                {
                    var value = key.GetValue(Constants.RegistryValues.AllowAllTrustedApps) as int?;
                    if(value != null && value == 1)
                    {
                        isEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Getting AllowAllTrustedApps policy");
            }
            return isEnabled;
        });
    }

    public Task<bool> SetSideloadingPolicyAsync(bool isEnabled)
    {
        return Task.Factory.StartNew<bool>(() =>
        {
            bool wasSaved = false;
            try
            {
                var key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(Constants.RegistryKeys.PathToEnableSideLoading);
                //var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(Constants.RegistryKeys.PathToEnableSideLoading);
                if (key != null)
                {
                    key.SetValue(Constants.RegistryValues.AllowAllTrustedApps, Convert.ToInt32(isEnabled));
                    wasSaved = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Setting AllowAllTrustedApps to {0}", isEnabled);

            }
            return wasSaved;
        });
    }
}
}
