using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsAppBoss.Model.Packages;
using WindowsAppBoss.Utilities;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.View.Main
{
    public partial class PackageBossMainForm : Form, IPackageDataGridView
    {
        public PackageBossMainForm()
        {
            InitializeComponent();
            _presenter = new Presenter.Main.PackageDataGridPresenter(this);  
            _windowLauncher = new WindowLauncher();
        }

        #region IPackageDataGridView 
        public bool ArePackageContextButtonsEnabled
        {
            set
            {
                this.DeletePackageButton.Enabled = value;
                this.ViewSettingsButton.Enabled = value;
                this.LaunchAppButton.Enabled = value;
                this.ManageSnapShotsButton.Enabled = value;
            }
        }

        public IList<Model.Packages.PackageDataViewRow> SetPackageItems
        {
            set
            {
                Utilities.Marshalling.InvokeIfNecessary(this.PackageDataGridView, () =>
                {
                    this.PackageDataGridView.DataSource = value;
                });
                
            
            }
        }
        #endregion IPackageDataGridView

        #region Events

        #region Menu Strip events

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutDialog = new View.About.About();
            aboutDialog.Show();
        }

        private void updatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Constants.Urls.AppHomePage);
        }

        private void tipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Constants.Urls.AppHomePage);
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Constants.DirectoryNames.LogDirectory);
        }
        

        private void developerLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _windowLauncher.LaunchWindow(typeof (View.DeveloperLicense.DeveloperLicenseView), false);
            //(new View.DeveloperLicense.DeveloperLicenseView()).Show();
        }

        private void customDataViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _windowLauncher.LaunchWindow(typeof(View.CustomData.CustomDataViewer), false);
            //(new View.CustomData.CustomDataViewer()).Show();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_presenter != null)
            {
                this.PackageDataGridView.DataSource = null;
                _presenter.UpdatePackageList();
            }
        }

        private void provisionedPackageManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            _windowLauncher.LaunchWindow(typeof (View.Packages.ProvisionedPackageManagerView), true);
            //(new View.Packages.ProvisionedPackageManagerView()).Show();
        }


        #endregion Menu Strip events

        #region Drag and drop events
        private void PackageBossMainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void PackageBossMainForm_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var asyncTask = HandleUserAddedFilesAsync(files);
        }
        #endregion drag and drop events

        private void PackageDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var row = this.PackageDataGridView.CurrentRow.DataBoundItem as PackageDataViewRow;
                if (row != null && _presenter != null)
                {
                    _presenter.SelectedItemChanged(row);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "PackageDataGridView_SelectionChanged");

            }
        }

        #region Button events
        private void ViewSettingsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if((_presenter != null && _presenter.SelectedItem != null))
                {
                    var selectedPackage = _presenter.SelectedItem.GetOriginalData();
                    string pathToSettings = Services.Settings.WindowsAppSettingsReader.GetSettingsPath(selectedPackage.DataDirectory);
                    ShowSettingsView(pathToSettings, selectedPackage.Name);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "ViewSettingsButton_Click");
            }
        }


        private void DeletePackageButton_Click(object sender, EventArgs e)
        {
            try
            {
                if((_presenter != null && _presenter.SelectedItem != null))
                {
                    string packageFullName = _presenter.SelectedItem.GetOriginalData().FullName;
                    var progressView = new View.Progress.ProgressWindowView();
                    var progressPresenter = new Presenter.Progress.ProgressPresenter(progressView);
                    progressPresenter.WindowTitle = "Remove App Package";
                    progressPresenter.TaskDescription = String.Format("Removing App Package {0}", packageFullName);
                    progressView.Show();
                    var manager = new Services.Packages.PackageManager();
                    var asyncTask = manager.RemovePackageAsync(progressPresenter, packageFullName);
                    asyncTask.ContinueWith(task =>
                    {
                        _presenter.UpdatePackageList();
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "DeletePackageButton_Click");

            }
        }

        private void InstallPackageButton_Click(object sender, EventArgs e)
        {
            try
            {
                var window = new View.Installer.AdvancedInstallView();
                window.Show();

                //string pathToFile = string.Empty;
                //var prompt = new OpenFileDialog();
                //prompt.Multiselect = true;
                //prompt.CheckFileExists = true;
                //prompt.Filter = Constants.FileNames.AppxFileSelectionFilter;
                //if (prompt.ShowDialog() == DialogResult.OK && System.IO.File.Exists(prompt.FileName))
                //{
                //    var asyncTask = HandleUserAddedFilesAsync(prompt.FileNames);
                //}
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "InstallPackageButton_Click");
            }
        }

        private void ManageSnapShotsButton_Click(object sender, EventArgs e)
        {
            try
            {
                var package = _presenter.SelectedItem.GetOriginalData();
                var snapShotManagerView = new View.SnapShots.PackageSnapShotManagerView(package);
                snapShotManagerView.Show();
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "ManageSnapShotsButton_Click");
            }
        }

        private void LaunchAppButton_Click(object sender, EventArgs e)
        {
            try
            {
                var package = _presenter.SelectedItem.GetOriginalData();
                var asyncTask = LaunchAppAsync(package.FullName);
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "LaunchAppButton_Click");
            }
        }
        #endregion button events
        #endregion events


        #region private methods
        private async Task HandleUserAddedFilesAsync(IEnumerable<string> files)
        {
            if (files != null && files.Any())
            {
                var appXFiles = files.Where(f => System.IO.Path.GetExtension(f).Equals(Constants.FileNames.PackageFileExtension));
                var settingsFiles = files.Where(f => System.IO.Path.GetFileName(f).EndsWith(Constants.FileNames.SettingsFileExtension, StringComparison.OrdinalIgnoreCase));

                foreach (var appXFile in appXFiles)
                {
                    if (!String.IsNullOrWhiteSpace(appXFile))
                    {
                        await InstallAppxPackageAsync(appXFile);
                    }
                }

                foreach (var settingsFile in settingsFiles)
                {
                    if (System.IO.File.Exists(settingsFile))
                    {
                        ShowSettingsView(settingsFile, System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(settingsFile)));
                    }

                }
            }
        }


        private async Task LaunchAppAsync(string packageFullname)
        {
            try
            {
                var appUserModelId = await Services.Packages.PackageLocator.GetAppUserModelIdAsync(packageFullname);
                Services.Packages.WindowsAppLauncher.LaunchApp(appUserModelId);
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Launching app {0}", packageFullname);
            }
        }

        private async Task InstallAppxPackageAsync(string appPackagePath)
        {
            var progressView = new View.Progress.ProgressWindowView();
            var progressPresenter = new Presenter.Progress.ProgressPresenter(progressView);
            progressPresenter.WindowTitle = "Add App Package";
            progressPresenter.TaskDescription = String.Format("Adding App Package {0}", appPackagePath) ;
            progressView.Show();
            var manager = new Services.Packages.PackageManager();
            //await  manager.AddPackageAsync(progressPresenter, appPackagePath);
            _presenter.UpdatePackageList();
            
        }

        private static void ShowSettingsView(string pathToSettingsFile, string description)
        {
            var settingsForm = new View.Settings.SettingsTextView(pathToSettingsFile, description);
            settingsForm.ShowDialog();
        }
        #endregion


        private readonly Presenter.Main.PackageDataGridPresenter _presenter;
        private readonly Utilities.WindowLauncher _windowLauncher;


        public event EventHandler Initialize;

       








     



   
    }
}
