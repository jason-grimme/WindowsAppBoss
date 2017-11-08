using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Services.Packages
{
    /// <summary>
    /// Launches apps
    /// </summary>
    class WindowsAppLauncher
    {

        #region Native Code
        public enum ActivateOptions
        {
            /// <summary>
            /// No Flags set
            /// </summary>
            None = 0x00000000,
            /// <summary>
            ///The application is being activated for design mode, and thus will not be able to
            // to create an immersive window. Window creation must be done by design tools which
            // load the necessary components by communicating with a designer-specified service on
            // the site chain established on the activation manager.  The splash screen normally
            // shown when an application is activated will also not appear.  Most activations
            // will not use this flag.
            /// </summary>
            DesignMode = 0x00000001, 
            /// <summary>
            /// Do not show an error dialog if the app fails to activate. 
            /// </summary>
            NoErrorUI = 0x00000002,  
            /// <summary>
            /// Do not show the splash screen when activating the app.
            /// </summary>
            NoSplashScreen = 0x00000004,
        }


        /// <summary>
        /// Provides methods which activate Windows Store apps for the Launch, File, and Protocol extensions.
        /// You will normally use this interface in debuggers and design tools. For instance, Microsoft Visual StudioIDE and Microsoft Expression Blend use this interface because both need to activate apps for the Launch contract to support F5 debugging.
        /// </summary>
        [ComImport, Guid("2e941141-7f97-4756-ba1d-9decde894a3d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        interface IApplicationActivationManager
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="appUserModelId"></param>
            /// <param name="arguments"></param>
            /// <param name="options"></param>
            /// <param name="processId"></param>
            /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            IntPtr ActivateApplication([In] String appUserModelId, [In] String arguments, [In] ActivateOptions options, [Out] out UInt32 processId);
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="appUserModelId"></param>
            /// <param name="itemArray"></param>
            /// <param name="verb"></param>
            /// <param name="processId"></param>
            /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            IntPtr ActivateForFile([In] String appUserModelId, [In] IntPtr /*IShellItemArray* */ itemArray, [In] String verb, [Out] out UInt32 processId);
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="appUserModelId"></param>
            /// <param name="itemArray"></param>
            /// <param name="processId">If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</param>
            /// <returns></returns>
            IntPtr ActivateForProtocol([In] String appUserModelId, [In] IntPtr /* IShellItemArray* */itemArray, [Out] out UInt32 processId);
        }

        [ComImport, Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")]//Application Activation Manager
        class ApplicationActivationManager : IApplicationActivationManager
        {
            
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)/*, PreserveSig*/]
            public extern IntPtr ActivateApplication([In] String appUserModelId, [In] String arguments, [In] ActivateOptions options, [Out] out UInt32 processId);
           
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            public extern IntPtr ActivateForFile([In] String appUserModelId, [In] IntPtr /*IShellItemArray* */ itemArray, [In] String verb, [Out] out UInt32 processId);
            
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            public extern IntPtr ActivateForProtocol([In] String appUserModelId, [In] IntPtr /* IShellItemArray* */itemArray, [Out] out UInt32 processId);
        }

        #endregion pinvoke

        /// <summary>
        /// Launches an app
        /// </summary>
        /// <param name="appUserModelId">something like (%package_family_name% + "!App"_</param>
        public static void LaunchApp(string appUserModelId)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(appUserModelId))
                {
                    IApplicationActivationManager appActiveManager = new ApplicationActivationManager();
                    // This call ensures that the app is launched as the foreground window
                    //CoAllowSetForegroundWindow(appActiveManager, IntPtr.Zero);

                    uint pid;
                    appActiveManager.ActivateApplication(appUserModelId, null, ActivateOptions.None, out pid);
                }
                else
                {
                    throw new ArgumentNullException("AppUserModelId cannot be null or empty");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Launching app {0}", appUserModelId);
                
            }
        }
    }
}
