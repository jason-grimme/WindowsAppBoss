using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Services.Packages
{
    internal class PackageManager
    {
        public PackageManager()
        {

        }

        #region public methods
        public Task<bool> AddPackageAsync(Uri packageLocation, IEnumerable<Uri> dependencyPaths, Presenter.Progress.ProgressPresenter progressPresenter)
        {
            return Task.Factory.StartNew<bool>(() =>
                {
                    bool success = false;
                    try
                    {
                        if (packageLocation != null && progressPresenter != null)
                        {
                            progressPresenter.SetDetailsText(true, "Adding package {0}", packageLocation.AbsolutePath);
                            if (dependencyPaths != null && dependencyPaths.Any())
                            {
                                var pathsAsText = String.Join(System.Environment.NewLine, dependencyPaths);
                                progressPresenter.SetDetailsText(true, "Will add dependencies:{0}{1}", Environment.NewLine, pathsAsText);
                            }
                            if (File.Exists(packageLocation.AbsolutePath))
                            {
                                var packageManager = new Windows.Management.Deployment.PackageManager();
                                var deploymentTask = packageManager.AddPackageAsync(packageLocation, dependencyPaths, Windows.Management.Deployment.DeploymentOptions.None);
                                success = UpdateProgressForDeploymentTask(progressPresenter, deploymentTask);
                                //packageManager.RegisterPackageAsync(
                            }
                            else
                            {
                                progressPresenter.SetDetailsText(true, "{0} does not exist.", packageLocation);
                            }
                        }
                        else
                        {
                            progressPresenter.SetDetailsText(true, "Null parameters");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex, "Add appx package");
                        progressPresenter.SetDetailsText(true, "AddPackageAsync failed, error message: {0}", ex.Message);
                        progressPresenter.SetDetailsText(true, "Full Stacktrace: {0}", ex.ToString());
                    }
                    return success;
                });
        }

        public async Task<bool> AddProvisionedPackageAsync(Uri packageLocation, string packageName, bool skipLicense, IEnumerable<Uri> listOfDependencyPackagePaths, Uri licensePath, Uri customDataPath, Presenter.Progress.ProgressPresenter progressPresenter)
        {
            bool success = false;
            try
            {
                if (packageLocation != null && progressPresenter != null)
                {
                    progressPresenter.SetDetailsText(true, "Adding package {0}", packageLocation.AbsolutePath); 

                    Uri packageFilePath = (packageLocation != null && File.Exists(packageLocation.AbsolutePath)) ? packageLocation : null;
                    Uri packageFolder = (packageLocation != null && Directory.Exists(packageLocation.AbsolutePath)) ? packageLocation : null;
                    if (packageFolder != null)
                    {
                        skipLicense = false;
                    }

                    var dismAgent = new DismPackageManager(progressPresenter);
                    success = await dismAgent.AddProvisionedPackageAsync(true, packageFilePath, packageFolder, skipLicense, listOfDependencyPackagePaths, licensePath, customDataPath);
                    //var dismAgent = new DismWrapper(progressPresenter);
                    //success = await dismAgent.AddProvisionedPackageAsync(true, packageFilePath, packageFolder, skipLicense, listOfDependencyPackagePaths, licensePath, customDataPath);

                    if (success && !String.IsNullOrWhiteSpace(packageName))
                    {
                        progressPresenter.SetDetailsText(true, "Will attempt to register {0}", packageName);
                        success = await AttemptToRegisterPackageAsync(packageName, progressPresenter);
                    }
                }
                else
                {
                    progressPresenter.SetDetailsText(true, "Null parameters");
                }
            }
            catch (Exception ex)
            {
                progressPresenter.SetDetailsText(true, "AddProvisionedPackageAsync failed, error message: {0}", ex.Message);
                progressPresenter.SetDetailsText(true, "Full Stacktrace: {0}", ex.ToString());
                Logger.Log(ex, "Add provisioned appx package");

            }
            return success;
        }


        private Task<bool> AttemptToRegisterPackageAsync(string packageName, Presenter.Progress.ProgressPresenter progressPresenter)
        {
            return Task.Factory.StartNew<bool>(() =>
                {
                    bool success = false;
                    try
                    {
                        if (progressPresenter != null)
                        {
                            string predictedManifestPath = Path.Combine(
                                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                                Constants.DirectoryNames.WindowsApps,
                                packageName,
                                Constants.FileNames.AppxManifest);

                            if (File.Exists(predictedManifestPath))
                            {
                                var manifestLocation = new Uri(predictedManifestPath);
                                var packageManager = new Windows.Management.Deployment.PackageManager();
                                var task = packageManager.RegisterPackageAsync(manifestLocation, null, Windows.Management.Deployment.DeploymentOptions.None);
                                success = UpdateProgressForDeploymentTask(progressPresenter, task);
                            }
                            else
                            {
                                progressPresenter.SetDetailsText(true, "Manifest does not exist at {0}. Please register manually", predictedManifestPath);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        progressPresenter.SetDetailsText(true, "Exception: {0}", ex.Message);
                        success = false;
                        Logger.Log(ex, "Attempt to register appx package" );
                    }
                    return success;
                });
        }

       

        

        public Task<bool> RemovePackageAsync(Presenter.Progress.ProgressPresenter progressPresenter, string packageFamilyName)
        {
            return Task.Factory.StartNew<bool>( () =>
                {
                    var success = false;
                    try
                    {
                        progressPresenter.SetDetailsText(true, "Removing {0}", packageFamilyName);
                        var packageManager = new Windows.Management.Deployment.PackageManager();

                        var deploymentTask = packageManager.RemovePackageAsync(packageFamilyName);

                        success = UpdateProgressForDeploymentTask(progressPresenter, deploymentTask);
                    }
                    catch (Exception ex)
                    {
                        progressPresenter.SetDetailsText(true, "RemovePackageSample failed, error message: {0}", ex.Message);
                        progressPresenter.SetDetailsText(true, "Full Stacktrace: {0}", ex.ToString());
                        Logger.Log(ex, "Remove appx package");
                        success = false;
                    }

                    if (success)
                    {
                        progressPresenter.SetDetailsText(true, "Success.  Tile will appear until signoff");
                    }

                    return success;
                });
        }
        #endregion public methods

        #region private methods
        private bool UpdateProgressForDeploymentTask(Presenter.Progress.ProgressPresenter progressPresenter, IAsyncOperationWithProgress<Windows.Management.Deployment.DeploymentResult, Windows.Management.Deployment.DeploymentProgress> deploymentTask)
        {
            bool success = false;
            var addCompletedEvent = new ManualResetEvent(false);
            deploymentTask.Completed = (result, progress) =>
            {
                progressPresenter.OverallProgress = 100;
                addCompletedEvent.Set();
            };

            deploymentTask.Progress = (result, progress) =>
            {
                progressPresenter.OverallProgress = progress.percentage;
            };

            progressPresenter.SetDetailsText(true, "Waiting for task to complete...");
            addCompletedEvent.WaitOne();

            if (deploymentTask.Status == Windows.Foundation.AsyncStatus.Error)
            {
                var result = deploymentTask.GetResults();
                progressPresenter.SetDetailsText(true, "Error: {0}", result.ExtendedErrorCode);
                progressPresenter.SetDetailsText(true, "Detailed Error Text: {0}", result.ErrorText);
                success = false;
            }
            else if (deploymentTask.Status == Windows.Foundation.AsyncStatus.Canceled)
            {
                success = false;
                progressPresenter.SetDetailsText(true, "Task Canceled");
            }
            else if (deploymentTask.Status == Windows.Foundation.AsyncStatus.Completed)
            {
                progressPresenter.SetDetailsText(true, "Task succeeded!");
                success = true;
            }
            else
            {
                success = false;
                progressPresenter.SetDetailsText(true, "Task status unknown");
            }
            return success;
        }
        #endregion
    }
}
