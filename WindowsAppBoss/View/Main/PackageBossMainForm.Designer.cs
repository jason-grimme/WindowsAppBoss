namespace WindowsAppBoss.View.Main
{
    partial class PackageBossMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageBossMainForm));
            this.PackageDataGridView = new System.Windows.Forms.DataGridView();
            this.DeletePackageButton = new System.Windows.Forms.Button();
            this.InstallPackageButton = new System.Windows.Forms.Button();
            this.ViewSettingsButton = new System.Windows.Forms.Button();
            this.LaunchAppButton = new System.Windows.Forms.Button();
            this.ManageSnapShotsButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.developerLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customDataViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.provisionedPackageManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.PackageDataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PackageDataGridView
            // 
            this.PackageDataGridView.AllowUserToAddRows = false;
            this.PackageDataGridView.AllowUserToDeleteRows = false;
            this.PackageDataGridView.AllowUserToOrderColumns = true;
            this.PackageDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PackageDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PackageDataGridView.Location = new System.Drawing.Point(12, 98);
            this.PackageDataGridView.MultiSelect = false;
            this.PackageDataGridView.Name = "PackageDataGridView";
            this.PackageDataGridView.ReadOnly = true;
            this.PackageDataGridView.RowHeadersVisible = false;
            this.PackageDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PackageDataGridView.Size = new System.Drawing.Size(868, 471);
            this.PackageDataGridView.TabIndex = 0;
            this.PackageDataGridView.SelectionChanged += new System.EventHandler(this.PackageDataGridView_SelectionChanged);
            // 
            // DeletePackageButton
            // 
            this.DeletePackageButton.Location = new System.Drawing.Point(769, 45);
            this.DeletePackageButton.Name = "DeletePackageButton";
            this.DeletePackageButton.Size = new System.Drawing.Size(110, 47);
            this.DeletePackageButton.TabIndex = 1;
            this.DeletePackageButton.Text = "&Uninstall";
            this.DeletePackageButton.UseVisualStyleBackColor = true;
            this.DeletePackageButton.Click += new System.EventHandler(this.DeletePackageButton_Click);
            // 
            // InstallPackageButton
            // 
            this.InstallPackageButton.Location = new System.Drawing.Point(12, 45);
            this.InstallPackageButton.Name = "InstallPackageButton";
            this.InstallPackageButton.Size = new System.Drawing.Size(111, 47);
            this.InstallPackageButton.TabIndex = 2;
            this.InstallPackageButton.Text = "&Install";
            this.InstallPackageButton.UseVisualStyleBackColor = true;
            this.InstallPackageButton.Click += new System.EventHandler(this.InstallPackageButton_Click);
            // 
            // ViewSettingsButton
            // 
            this.ViewSettingsButton.Location = new System.Drawing.Point(655, 45);
            this.ViewSettingsButton.Name = "ViewSettingsButton";
            this.ViewSettingsButton.Size = new System.Drawing.Size(102, 47);
            this.ViewSettingsButton.TabIndex = 3;
            this.ViewSettingsButton.Text = "S&ettings";
            this.ViewSettingsButton.UseVisualStyleBackColor = true;
            this.ViewSettingsButton.Click += new System.EventHandler(this.ViewSettingsButton_Click);
            // 
            // LaunchAppButton
            // 
            this.LaunchAppButton.AllowDrop = true;
            this.LaunchAppButton.Location = new System.Drawing.Point(436, 45);
            this.LaunchAppButton.Name = "LaunchAppButton";
            this.LaunchAppButton.Size = new System.Drawing.Size(95, 47);
            this.LaunchAppButton.TabIndex = 4;
            this.LaunchAppButton.Text = "&Launch";
            this.LaunchAppButton.UseVisualStyleBackColor = true;
            this.LaunchAppButton.Click += new System.EventHandler(this.LaunchAppButton_Click);
            // 
            // ManageSnapShotsButton
            // 
            this.ManageSnapShotsButton.Location = new System.Drawing.Point(546, 45);
            this.ManageSnapShotsButton.Name = "ManageSnapShotsButton";
            this.ManageSnapShotsButton.Size = new System.Drawing.Size(95, 47);
            this.ManageSnapShotsButton.TabIndex = 5;
            this.ManageSnapShotsButton.Text = "&Snap Shots";
            this.ManageSnapShotsButton.UseVisualStyleBackColor = true;
            this.ManageSnapShotsButton.Click += new System.EventHandler(this.ManageSnapShotsButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolsToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(892, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.updatesToolStripMenuItem,
            this.tipsToolStripMenuItem,
            this.logsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(80, 20);
            this.toolStripMenuItem1.Text = "&Application";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // updatesToolStripMenuItem
            // 
            this.updatesToolStripMenuItem.Name = "updatesToolStripMenuItem";
            this.updatesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.updatesToolStripMenuItem.Text = "Updates";
            this.updatesToolStripMenuItem.Click += new System.EventHandler(this.updatesToolStripMenuItem_Click);
            // 
            // tipsToolStripMenuItem
            // 
            this.tipsToolStripMenuItem.Name = "tipsToolStripMenuItem";
            this.tipsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tipsToolStripMenuItem.Text = "Tips";
            this.tipsToolStripMenuItem.Click += new System.EventHandler(this.tipsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.developerLicenseToolStripMenuItem,
            this.customDataViewerToolStripMenuItem,
            this.provisionedPackageManagerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // developerLicenseToolStripMenuItem
            // 
            this.developerLicenseToolStripMenuItem.Name = "developerLicenseToolStripMenuItem";
            this.developerLicenseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.developerLicenseToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.developerLicenseToolStripMenuItem.Text = "Developer License";
            this.developerLicenseToolStripMenuItem.ToolTipText = "Acquire, remove, renew, and check your developer license";
            this.developerLicenseToolStripMenuItem.Click += new System.EventHandler(this.developerLicenseToolStripMenuItem_Click);
            // 
            // customDataViewerToolStripMenuItem
            // 
            this.customDataViewerToolStripMenuItem.Name = "customDataViewerToolStripMenuItem";
            this.customDataViewerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.customDataViewerToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.customDataViewerToolStripMenuItem.Text = "Custom.Data Viewer";
            this.customDataViewerToolStripMenuItem.Click += new System.EventHandler(this.customDataViewerToolStripMenuItem_Click);
            // 
            // provisionedPackageManagerToolStripMenuItem
            // 
            this.provisionedPackageManagerToolStripMenuItem.Name = "provisionedPackageManagerToolStripMenuItem";
            this.provisionedPackageManagerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
            this.provisionedPackageManagerToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.provisionedPackageManagerToolStripMenuItem.Text = "Provisioned Package Manager";
            this.provisionedPackageManagerToolStripMenuItem.Click += new System.EventHandler(this.provisionedPackageManagerToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.logsToolStripMenuItem.Text = "Logs";
            this.logsToolStripMenuItem.Click += new System.EventHandler(this.logsToolStripMenuItem_Click);
            // 
            // PackageBossMainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 584);
            this.Controls.Add(this.ManageSnapShotsButton);
            this.Controls.Add(this.LaunchAppButton);
            this.Controls.Add(this.ViewSettingsButton);
            this.Controls.Add(this.InstallPackageButton);
            this.Controls.Add(this.DeletePackageButton);
            this.Controls.Add(this.PackageDataGridView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "PackageBossMainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Windows App Boss";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PackageBossMainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.PackageBossMainForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.PackageDataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView PackageDataGridView;
        private System.Windows.Forms.Button DeletePackageButton;
        private System.Windows.Forms.Button InstallPackageButton;
        private System.Windows.Forms.Button ViewSettingsButton;
        private System.Windows.Forms.Button LaunchAppButton;
        private System.Windows.Forms.Button ManageSnapShotsButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem developerLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customDataViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem provisionedPackageManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
    }
}