using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.View.Installer
{
    public partial class AdvancedInstallView : Form, View.Installer.IAdvancedInstallView, View.Progress.IProgressWindowView
    {
        public AdvancedInstallView()
        {
            InitializeComponent();
            _presenter = new Presenter.Installer.AdvancedInstallPresenter(this);
            this.SideLoadingToggleButton.Enabled = false;
            this.SideLoadingToggleButton.Visible = false;
            IsBrowseForFolderEnabled = false;

            if (Utilities.WindowLauncher.IsRunningWithElevatedPrivileges())
            {
                this.Text += Constants.Strings.RunningAsAdministrator;
            }
        }

        #region View.Installer.IAdvancedInstallView
        public bool IsSideLoadingEnabled
        {
            set
            {
                this.SideLoadingToggleButton.Enabled = true;
                this.SideLoadingToggleButton.Visible = true;
                this.SideLoadingToggleButton.Text = (value) ? "Disable side loading" : "Enable side loading";
            }
        }

        public string DetailsText
        {
            get
            {
                return this.OutputLogTextBox.Text;
            }
            set
            {
                Utilities.Marshalling.InvokeIfNecessary(this, () =>
                {
                    Utilities.Marshalling.InvokeIfNecessary(this.OutputLogTextBox, () =>
                    {
                        this.OutputLogTextBox.Text = value;
                        this.OutputLogTextBox.SelectionStart = value.Length;
                        this.OutputLogTextBox.ScrollToCaret();
                    });
                });
            }
        }


        public bool IsBrowseForFolderChecked
        {
            get
            {
                return this.FolderCheckbox.Checked;
            }
            set
            {
                this.FolderCheckbox.Checked = value;
            }
        }

        public bool IsAddDependenciesChecked
        {
            get
            {
                return this.DependenciesCheckBox.Checked;
            }
            set
            {
                this.DependenciesCheckBox.Checked = value;
            }
        }

        public bool IsProvisionedModeChecked
        {
            get
            {
                return this.ProvisionedCheckbox.Checked;
            }
            set
            {
                this.ProvisionedCheckbox.Checked = value;
            }
        }

        public bool IsCustomDataChecked
        {
            get
            {
                return this.CustomDataCheckbox.Checked;
            }
            set
            {
                this.CustomDataCheckbox.Checked = value;
            }
        }

        public bool IsLicenseChecked
        {
            get
            {
                return this.LicenseCheckBox.Checked;
            }
            set
            {
                this.LicenseCheckBox.Checked = value;
            }
        }



        public bool IsFormEnabled
        {
            get
            {
                return this.Enabled;
            }
            set
            {
                this.Enabled = value;
            }
        }

        public bool IsAddPackageButtonEnabled
        {
            get
            {
                return this.AddPackageButton.Enabled;
            }
            set
            {
                this.AddPackageButton.Enabled = value;
            }
        }

        public bool IsDependenciesOptionsEnabled
        {
            get
            {
                return this.DependenciesGroupBox.Enabled;
            }
            set
            {
                this.DependenciesGroupBox.Enabled = value;
            }
        }

        public bool IsProvisionedOptionsEnabled
        {
            get
            {
                return this.ProvisionedOptionsGroupBox.Enabled;
            }
            set
            {
                this.ProvisionedOptionsGroupBox.Enabled = value;
            }
        }

        public bool IsBrowseForFolderEnabled
        {
            get
            {
                return this.FolderCheckbox.Enabled;
            }
            set
            {
                this.FolderCheckbox.Enabled = value;
            }
        }

        public bool IsCustomDataAndLicenseEnabled
        {
            set
            {
                this.CustomDataCheckbox.Enabled = value;
                this.LicenseCheckBox.Enabled = value;
                IsCustomDataValuesEnabled = value;
                IsLicenseValuesEnabled = value;
            }
        }
        public bool IsCustomDataValuesEnabled
        {
            set
            {
                this.CustomDataBrowseButton.Enabled = value;
                this.CustomDataPathTextBox.Enabled = value;
            }
        }

        public bool IsLicenseValuesEnabled
        {
            set
            {
                this.LicenseBrowseButton.Enabled = value;
                this.LicesnsePathTextBox.Enabled = value;
            }
        }

        public string PathToAppPackage
        {
            get
            {
                return this.AppPathTextBox.Text;
            }
            set
            {
                this.AppPathTextBox.Text = value;
            }
        }

        public string PathsToDependencies
        {
            get
            {
                return this.DependenciesTextBox.Text;
            }
            set
            {
                this.DependenciesTextBox.Text = value;
            }
        }

        public string PathToCustomData
        {
            get
            {
                return this.CustomDataPathTextBox.Text;
            }
            set
            {
                this.CustomDataPathTextBox.Text = value;
            }
        }

        public string PathToLicense
        {
            get
            {
                return this.LicesnsePathTextBox.Text;
            }
            set
            {
                this.LicesnsePathTextBox.Text = value;
            }
        }

        public string PackageName
        {
            get
            {
                return this.PackageNameTextBox.Text;
            }
            set
            {
                this.PackageNameTextBox.Text = value;
            }
        }


        #endregion

        #region events

        private void AddPackageButton_Click(object sender, EventArgs e)
        {
            var taskAsync = _presenter.OnAddPackageButtonClick();
        }

        #region Browse Buttons
        private void BrowseForPackageButton_Click(object sender, EventArgs e)
        {
            try
            {

                // FOLDER
                if (IsBrowseForFolderChecked && IsProvisionedModeChecked)
                {
                    var prompt = new FolderBrowserDialog();
                    prompt.Description = "Select a folder that contains app package(s)";
                    prompt.ShowNewFolderButton = false;
                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        PathToAppPackage = prompt.SelectedPath;
                        PopuatePackageNameField(prompt.SelectedPath);
                    }
                    else // cancel
                    {
                        PathToAppPackage = string.Empty;
                    }
                }
                else // FILE
                {
                    var prompt = new OpenFileDialog
                        {
                            Multiselect = false,
                            CheckFileExists = true,
                            Filter = Constants.FileNames.AppxFileSelectionFilter
                        };
                    if (prompt.ShowDialog() == DialogResult.OK && System.IO.File.Exists(prompt.FileName))
                    {
                        PathToAppPackage = string.Empty;
                        if (!String.IsNullOrWhiteSpace(prompt.FileName))
                        {
                            PathToAppPackage += prompt.FileName;
                            PopuatePackageNameField(prompt.FileName);
                        }
                    }
                    else // cancel
                    {
                        PathToAppPackage = string.Empty;
                    }
                }

                _presenter.FormValueHasChanged();
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "BrowseForPackageButton_Click");

            }
        }


        private void PopuatePackageNameField(string packageLocation)
        {
            try
            {
                string packageName = PackageName;
                if (!String.IsNullOrWhiteSpace(packageLocation))
                {
                    if (System.IO.File.Exists(packageLocation))
                    {
                        packageName = System.IO.Path.GetFileNameWithoutExtension(packageLocation);
                    }
                    else if (System.IO.Directory.Exists(packageLocation))
                    {
                        var subDirectories = System.IO.Directory.GetDirectories(packageLocation);
                        if (subDirectories != null && subDirectories.Any())
                        {
                            string mainPackageDirectory = subDirectories.FirstOrDefault(dir => dir.EndsWith(".main"));
                            if (!String.IsNullOrWhiteSpace(mainPackageDirectory))
                            {
                                string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(mainPackageDirectory);
                                packageName = nameWithoutExtension;
                            }
                        }
                    }
                }
                PackageName = packageName;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Populating package name field in form");
            }

        }

        private void DependenciesBrowsButton_Click(object sender, EventArgs e)
        {
            try
            {

                var prompt = new OpenFileDialog();
                prompt.Multiselect = true;
                prompt.CheckFileExists = true;
                prompt.Filter = Constants.FileNames.AppxFileSelectionFilter;
                if (prompt.ShowDialog() == DialogResult.OK && System.IO.File.Exists(prompt.FileName))
                {
                    PathsToDependencies = string.Empty;
                    if (prompt.FileNames != null && prompt.FileNames.Any())
                    {
                        foreach (var file in prompt.FileNames)
                        {
                            PathsToDependencies += file + Environment.NewLine;
                        }
                    }
                }
                else // cancel
                {
                    PathsToDependencies = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "DependenciesBrowsButton_Click");
            }
           

        }

        private void LicenseBrowseButton_Click(object sender, EventArgs e)
        {
            var prompt = new OpenFileDialog();
            prompt.Multiselect = false;
            prompt.CheckFileExists = true;
            prompt.Filter = Constants.FileNames.LicenseFileSelectionFilter;
            if (prompt.ShowDialog() == DialogResult.OK && System.IO.File.Exists(prompt.FileName))
            {
                PathToLicense = string.Empty;
                if (!String.IsNullOrWhiteSpace(prompt.FileName))
                {
                    PathToLicense += prompt.FileName;
                }
            }
            else // cancel
            {
                PathToLicense = string.Empty;
            }
            _presenter.FormValueHasChanged();

        }

        private void CustomDataBrowseButton_Click(object sender, EventArgs e)
        {
            var prompt = new OpenFileDialog();
            prompt.Multiselect = false;
            prompt.CheckFileExists = true;
            //prompt.Filter = Constants.FileNames.LicenseFileSelectionFilter;
            if (prompt.ShowDialog() == DialogResult.OK && System.IO.File.Exists(prompt.FileName))
            {
                PathToCustomData = string.Empty;
                if (!String.IsNullOrWhiteSpace(prompt.FileName))
                {
                    PathToCustomData += prompt.FileName;
                }
            }
            else // cancel
            {
                PathToCustomData = string.Empty;
            }
            _presenter.FormValueHasChanged();
        }


        #endregion browse buttons

        #region check boxes

        private void FolderCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _presenter.FormValueHasChanged();

        }
        private void ProvisionedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _presenter.FormValueHasChanged();

            if (IsProvisionedModeChecked)
            {
                bool isAdmin = Utilities.WindowLauncher.IsRunningWithElevatedPrivileges();
                if (isAdmin == false)
                {
                    var windowLauncher = new Utilities.WindowLauncher();
                    bool windowShown = windowLauncher.LaunchWindow(this.GetType(), true);
                    if (windowShown)
                    {
                        this.Close();
                    }
                    else
                    {
                        IsProvisionedModeChecked = false;
                    }
                }
            }



        }

        private void DependenciesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _presenter.FormValueHasChanged();
        }

        private void CustomDataCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _presenter.FormValueHasChanged();
        }

        private void LicenseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _presenter.FormValueHasChanged();
        }
        #endregion checkboxes

        private void SideLoadingToggleButton_Click(object sender, EventArgs e)
        {
            if (_presenter != null)
            {
                bool isAdmin = Utilities.WindowLauncher.IsRunningWithElevatedPrivileges();
                if (isAdmin == false)
                {
                    var windowLauncher = new Utilities.WindowLauncher();
                    bool windowShown = windowLauncher.LaunchWindow(this.GetType(), true);
                    if (windowShown)
                    {
                        this.Close();
                    }
                    else
                    {
                    }
                }
                else
                {
                    _presenter.OnSideLoadingButtonCick();
                }
            }
        }
        #endregion events




        #region View.Progress.IProgressWindowView
        public string WindowTitle
        {
            set 
            { 
                //nothing 
            }
        }

        public string TaskDescription
        {
            set
            {
                this.DetailsText += value;
            }
        }

        public string SubTaskDescription
        {
            set
            {
                this.DetailsText += value;
            }
        }

        public int OverallProgress
        {
            set
            {
                Utilities.Marshalling.InvokeIfNecessary(this.OverallProgressText, () =>
                {
                    this.OverallProgressText.Text = (value + "%");
                });
                Utilities.Marshalling.InvokeIfNecessary(this.ProgressBar, () =>
                {
                    this.ProgressBar.Value = (value);
                });
            }
        }
        #endregion View.Progress.IProgressWindowView

        public event EventHandler Initialize;
        private Presenter.Installer.AdvancedInstallPresenter _presenter;

















    }
}
