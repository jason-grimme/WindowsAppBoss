namespace WindowsAppBoss.View.Installer
{
    partial class AdvancedInstallView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedInstallView));
            this.AddPackageGroupBox = new System.Windows.Forms.GroupBox();
            this.AppPathTextBox = new System.Windows.Forms.TextBox();
            this.DependenciesCheckBox = new System.Windows.Forms.CheckBox();
            this.FolderCheckbox = new System.Windows.Forms.CheckBox();
            this.ProvisionedCheckbox = new System.Windows.Forms.CheckBox();
            this.BrowseForPackageButton = new System.Windows.Forms.Button();
            this.OutputLogTextBox = new System.Windows.Forms.TextBox();
            this.OverallProgressText = new System.Windows.Forms.Label();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.SideLoadingToggleButton = new System.Windows.Forms.Button();
            this.CustomDataCheckbox = new System.Windows.Forms.CheckBox();
            this.CustomDataBrowseButton = new System.Windows.Forms.Button();
            this.CustomDataPathTextBox = new System.Windows.Forms.TextBox();
            this.LicenseCheckBox = new System.Windows.Forms.CheckBox();
            this.LicesnsePathTextBox = new System.Windows.Forms.TextBox();
            this.LicenseBrowseButton = new System.Windows.Forms.Button();
            this.ProvisionedOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.labelPackageName = new System.Windows.Forms.Label();
            this.PackageNameTextBox = new System.Windows.Forms.TextBox();
            this.DependenciesGroupBox = new System.Windows.Forms.GroupBox();
            this.DependenciesBrowsButton = new System.Windows.Forms.Button();
            this.DependenciesTextBox = new System.Windows.Forms.TextBox();
            this.AddPackageButton = new System.Windows.Forms.Button();
            this.AddPackageGroupBox.SuspendLayout();
            this.ProvisionedOptionsGroupBox.SuspendLayout();
            this.DependenciesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddPackageGroupBox
            // 
            this.AddPackageGroupBox.Controls.Add(this.AppPathTextBox);
            this.AddPackageGroupBox.Controls.Add(this.DependenciesCheckBox);
            this.AddPackageGroupBox.Controls.Add(this.FolderCheckbox);
            this.AddPackageGroupBox.Controls.Add(this.ProvisionedCheckbox);
            this.AddPackageGroupBox.Controls.Add(this.BrowseForPackageButton);
            this.AddPackageGroupBox.Location = new System.Drawing.Point(15, 224);
            this.AddPackageGroupBox.Name = "AddPackageGroupBox";
            this.AddPackageGroupBox.Size = new System.Drawing.Size(478, 102);
            this.AddPackageGroupBox.TabIndex = 0;
            this.AddPackageGroupBox.TabStop = false;
            this.AddPackageGroupBox.Text = "Add Package";
            // 
            // AppPathTextBox
            // 
            this.AppPathTextBox.Enabled = false;
            this.AppPathTextBox.Location = new System.Drawing.Point(7, 71);
            this.AppPathTextBox.Name = "AppPathTextBox";
            this.AppPathTextBox.ReadOnly = true;
            this.AppPathTextBox.Size = new System.Drawing.Size(459, 20);
            this.AppPathTextBox.TabIndex = 10;
            // 
            // DependenciesCheckBox
            // 
            this.DependenciesCheckBox.AutoSize = true;
            this.DependenciesCheckBox.Location = new System.Drawing.Point(127, 39);
            this.DependenciesCheckBox.Name = "DependenciesCheckBox";
            this.DependenciesCheckBox.Size = new System.Drawing.Size(117, 17);
            this.DependenciesCheckBox.TabIndex = 4;
            this.DependenciesCheckBox.Text = "Add Dependencies";
            this.DependenciesCheckBox.UseVisualStyleBackColor = true;
            this.DependenciesCheckBox.CheckedChanged += new System.EventHandler(this.DependenciesCheckBox_CheckedChanged);
            // 
            // FolderCheckbox
            // 
            this.FolderCheckbox.AutoSize = true;
            this.FolderCheckbox.Location = new System.Drawing.Point(127, 17);
            this.FolderCheckbox.Name = "FolderCheckbox";
            this.FolderCheckbox.Size = new System.Drawing.Size(105, 17);
            this.FolderCheckbox.TabIndex = 2;
            this.FolderCheckbox.Text = "Browse for folder";
            this.FolderCheckbox.UseVisualStyleBackColor = true;
            this.FolderCheckbox.CheckedChanged += new System.EventHandler(this.FolderCheckbox_CheckedChanged);
            // 
            // ProvisionedCheckbox
            // 
            this.ProvisionedCheckbox.AutoSize = true;
            this.ProvisionedCheckbox.Location = new System.Drawing.Point(305, 17);
            this.ProvisionedCheckbox.Name = "ProvisionedCheckbox";
            this.ProvisionedCheckbox.Size = new System.Drawing.Size(81, 17);
            this.ProvisionedCheckbox.TabIndex = 1;
            this.ProvisionedCheckbox.Text = "Provisioned";
            this.ProvisionedCheckbox.UseVisualStyleBackColor = true;
            this.ProvisionedCheckbox.CheckedChanged += new System.EventHandler(this.ProvisionedCheckbox_CheckedChanged);
            // 
            // BrowseForPackageButton
            // 
            this.BrowseForPackageButton.Location = new System.Drawing.Point(7, 20);
            this.BrowseForPackageButton.Name = "BrowseForPackageButton";
            this.BrowseForPackageButton.Size = new System.Drawing.Size(100, 37);
            this.BrowseForPackageButton.TabIndex = 0;
            this.BrowseForPackageButton.Text = "Browse";
            this.BrowseForPackageButton.UseVisualStyleBackColor = true;
            this.BrowseForPackageButton.Click += new System.EventHandler(this.BrowseForPackageButton_Click);
            // 
            // OutputLogTextBox
            // 
            this.OutputLogTextBox.Location = new System.Drawing.Point(12, 62);
            this.OutputLogTextBox.Multiline = true;
            this.OutputLogTextBox.Name = "OutputLogTextBox";
            this.OutputLogTextBox.ReadOnly = true;
            this.OutputLogTextBox.Size = new System.Drawing.Size(478, 107);
            this.OutputLogTextBox.TabIndex = 2;
            // 
            // OverallProgressText
            // 
            this.OverallProgressText.AutoSize = true;
            this.OverallProgressText.Location = new System.Drawing.Point(12, 9);
            this.OverallProgressText.Name = "OverallProgressText";
            this.OverallProgressText.Size = new System.Drawing.Size(21, 13);
            this.OverallProgressText.TabIndex = 6;
            this.OverallProgressText.Text = "0%";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 33);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(478, 23);
            this.ProgressBar.TabIndex = 5;
            // 
            // SideLoadingToggleButton
            // 
            this.SideLoadingToggleButton.Location = new System.Drawing.Point(348, 175);
            this.SideLoadingToggleButton.Name = "SideLoadingToggleButton";
            this.SideLoadingToggleButton.Size = new System.Drawing.Size(145, 43);
            this.SideLoadingToggleButton.TabIndex = 7;
            this.SideLoadingToggleButton.Text = "Side Loading";
            this.SideLoadingToggleButton.UseVisualStyleBackColor = true;
            this.SideLoadingToggleButton.Click += new System.EventHandler(this.SideLoadingToggleButton_Click);
            // 
            // CustomDataCheckbox
            // 
            this.CustomDataCheckbox.AutoSize = true;
            this.CustomDataCheckbox.Location = new System.Drawing.Point(6, 55);
            this.CustomDataCheckbox.Name = "CustomDataCheckbox";
            this.CustomDataCheckbox.Size = new System.Drawing.Size(87, 17);
            this.CustomDataCheckbox.TabIndex = 4;
            this.CustomDataCheckbox.Text = "Custom Data";
            this.CustomDataCheckbox.UseVisualStyleBackColor = true;
            this.CustomDataCheckbox.CheckedChanged += new System.EventHandler(this.CustomDataCheckbox_CheckedChanged);
            // 
            // CustomDataBrowseButton
            // 
            this.CustomDataBrowseButton.Enabled = false;
            this.CustomDataBrowseButton.Location = new System.Drawing.Point(99, 49);
            this.CustomDataBrowseButton.Name = "CustomDataBrowseButton";
            this.CustomDataBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.CustomDataBrowseButton.TabIndex = 5;
            this.CustomDataBrowseButton.Text = "Select";
            this.CustomDataBrowseButton.UseVisualStyleBackColor = true;
            this.CustomDataBrowseButton.Click += new System.EventHandler(this.CustomDataBrowseButton_Click);
            // 
            // CustomDataPathTextBox
            // 
            this.CustomDataPathTextBox.Enabled = false;
            this.CustomDataPathTextBox.Location = new System.Drawing.Point(181, 53);
            this.CustomDataPathTextBox.Name = "CustomDataPathTextBox";
            this.CustomDataPathTextBox.Size = new System.Drawing.Size(282, 20);
            this.CustomDataPathTextBox.TabIndex = 6;
            // 
            // LicenseCheckBox
            // 
            this.LicenseCheckBox.AutoSize = true;
            this.LicenseCheckBox.Location = new System.Drawing.Point(6, 88);
            this.LicenseCheckBox.Name = "LicenseCheckBox";
            this.LicenseCheckBox.Size = new System.Drawing.Size(63, 17);
            this.LicenseCheckBox.TabIndex = 7;
            this.LicenseCheckBox.Text = "License";
            this.LicenseCheckBox.UseVisualStyleBackColor = true;
            this.LicenseCheckBox.CheckedChanged += new System.EventHandler(this.LicenseCheckBox_CheckedChanged);
            // 
            // LicesnsePathTextBox
            // 
            this.LicesnsePathTextBox.Enabled = false;
            this.LicesnsePathTextBox.Location = new System.Drawing.Point(181, 88);
            this.LicesnsePathTextBox.Name = "LicesnsePathTextBox";
            this.LicesnsePathTextBox.Size = new System.Drawing.Size(282, 20);
            this.LicesnsePathTextBox.TabIndex = 9;
            // 
            // LicenseBrowseButton
            // 
            this.LicenseBrowseButton.Enabled = false;
            this.LicenseBrowseButton.Location = new System.Drawing.Point(99, 84);
            this.LicenseBrowseButton.Name = "LicenseBrowseButton";
            this.LicenseBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.LicenseBrowseButton.TabIndex = 8;
            this.LicenseBrowseButton.Text = "Select";
            this.LicenseBrowseButton.UseVisualStyleBackColor = true;
            this.LicenseBrowseButton.Click += new System.EventHandler(this.LicenseBrowseButton_Click);
            // 
            // ProvisionedOptionsGroupBox
            // 
            this.ProvisionedOptionsGroupBox.Controls.Add(this.labelPackageName);
            this.ProvisionedOptionsGroupBox.Controls.Add(this.PackageNameTextBox);
            this.ProvisionedOptionsGroupBox.Controls.Add(this.LicesnsePathTextBox);
            this.ProvisionedOptionsGroupBox.Controls.Add(this.CustomDataCheckbox);
            this.ProvisionedOptionsGroupBox.Controls.Add(this.LicenseBrowseButton);
            this.ProvisionedOptionsGroupBox.Controls.Add(this.CustomDataBrowseButton);
            this.ProvisionedOptionsGroupBox.Controls.Add(this.LicenseCheckBox);
            this.ProvisionedOptionsGroupBox.Controls.Add(this.CustomDataPathTextBox);
            this.ProvisionedOptionsGroupBox.Enabled = false;
            this.ProvisionedOptionsGroupBox.Location = new System.Drawing.Point(15, 462);
            this.ProvisionedOptionsGroupBox.Name = "ProvisionedOptionsGroupBox";
            this.ProvisionedOptionsGroupBox.Size = new System.Drawing.Size(478, 119);
            this.ProvisionedOptionsGroupBox.TabIndex = 8;
            this.ProvisionedOptionsGroupBox.TabStop = false;
            this.ProvisionedOptionsGroupBox.Text = "Provisioned Package Options";
            // 
            // labelPackageName
            // 
            this.labelPackageName.AutoSize = true;
            this.labelPackageName.Location = new System.Drawing.Point(6, 22);
            this.labelPackageName.Name = "labelPackageName";
            this.labelPackageName.Size = new System.Drawing.Size(81, 13);
            this.labelPackageName.TabIndex = 11;
            this.labelPackageName.Text = "Package Name";
            // 
            // PackageNameTextBox
            // 
            this.PackageNameTextBox.Location = new System.Drawing.Point(99, 19);
            this.PackageNameTextBox.Name = "PackageNameTextBox";
            this.PackageNameTextBox.Size = new System.Drawing.Size(364, 20);
            this.PackageNameTextBox.TabIndex = 10;
            // 
            // DependenciesGroupBox
            // 
            this.DependenciesGroupBox.Controls.Add(this.DependenciesBrowsButton);
            this.DependenciesGroupBox.Controls.Add(this.DependenciesTextBox);
            this.DependenciesGroupBox.Enabled = false;
            this.DependenciesGroupBox.Location = new System.Drawing.Point(15, 337);
            this.DependenciesGroupBox.Name = "DependenciesGroupBox";
            this.DependenciesGroupBox.Size = new System.Drawing.Size(478, 109);
            this.DependenciesGroupBox.TabIndex = 9;
            this.DependenciesGroupBox.TabStop = false;
            this.DependenciesGroupBox.Text = "Dependencies";
            // 
            // DependenciesBrowsButton
            // 
            this.DependenciesBrowsButton.Location = new System.Drawing.Point(7, 19);
            this.DependenciesBrowsButton.Name = "DependenciesBrowsButton";
            this.DependenciesBrowsButton.Size = new System.Drawing.Size(100, 37);
            this.DependenciesBrowsButton.TabIndex = 5;
            this.DependenciesBrowsButton.Text = "Browse";
            this.DependenciesBrowsButton.UseVisualStyleBackColor = true;
            this.DependenciesBrowsButton.Click += new System.EventHandler(this.DependenciesBrowsButton_Click);
            // 
            // DependenciesTextBox
            // 
            this.DependenciesTextBox.Location = new System.Drawing.Point(113, 19);
            this.DependenciesTextBox.Multiline = true;
            this.DependenciesTextBox.Name = "DependenciesTextBox";
            this.DependenciesTextBox.ReadOnly = true;
            this.DependenciesTextBox.Size = new System.Drawing.Size(353, 82);
            this.DependenciesTextBox.TabIndex = 0;
            // 
            // AddPackageButton
            // 
            this.AddPackageButton.Enabled = false;
            this.AddPackageButton.Location = new System.Drawing.Point(197, 175);
            this.AddPackageButton.Name = "AddPackageButton";
            this.AddPackageButton.Size = new System.Drawing.Size(145, 43);
            this.AddPackageButton.TabIndex = 10;
            this.AddPackageButton.Text = "Add Package";
            this.AddPackageButton.UseVisualStyleBackColor = true;
            this.AddPackageButton.Click += new System.EventHandler(this.AddPackageButton_Click);
            // 
            // AdvancedInstallView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 605);
            this.Controls.Add(this.AddPackageButton);
            this.Controls.Add(this.DependenciesGroupBox);
            this.Controls.Add(this.ProvisionedOptionsGroupBox);
            this.Controls.Add(this.OverallProgressText);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.SideLoadingToggleButton);
            this.Controls.Add(this.OutputLogTextBox);
            this.Controls.Add(this.AddPackageGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdvancedInstallView";
            this.Text = "Advanced Package Installer";
            this.AddPackageGroupBox.ResumeLayout(false);
            this.AddPackageGroupBox.PerformLayout();
            this.ProvisionedOptionsGroupBox.ResumeLayout(false);
            this.ProvisionedOptionsGroupBox.PerformLayout();
            this.DependenciesGroupBox.ResumeLayout(false);
            this.DependenciesGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox AddPackageGroupBox;
        private System.Windows.Forms.TextBox OutputLogTextBox;
        private System.Windows.Forms.Button BrowseForPackageButton;
        private System.Windows.Forms.CheckBox ProvisionedCheckbox;
        private System.Windows.Forms.Label OverallProgressText;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Button SideLoadingToggleButton;
        private System.Windows.Forms.CheckBox FolderCheckbox;
        private System.Windows.Forms.TextBox LicesnsePathTextBox;
        private System.Windows.Forms.Button LicenseBrowseButton;
        private System.Windows.Forms.CheckBox LicenseCheckBox;
        private System.Windows.Forms.TextBox CustomDataPathTextBox;
        private System.Windows.Forms.Button CustomDataBrowseButton;
        private System.Windows.Forms.CheckBox CustomDataCheckbox;
        private System.Windows.Forms.GroupBox ProvisionedOptionsGroupBox;
        private System.Windows.Forms.CheckBox DependenciesCheckBox;
        private System.Windows.Forms.GroupBox DependenciesGroupBox;
        private System.Windows.Forms.Button DependenciesBrowsButton;
        private System.Windows.Forms.TextBox DependenciesTextBox;
        private System.Windows.Forms.TextBox AppPathTextBox;
        private System.Windows.Forms.Button AddPackageButton;
        private System.Windows.Forms.Label labelPackageName;
        private System.Windows.Forms.TextBox PackageNameTextBox;
    }
}