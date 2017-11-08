namespace WindowsAppBoss.View.Packages
{
    partial class ProvisionedPackageManagerView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProvisionedPackageManagerView));
            this.PackageDataGrid = new System.Windows.Forms.DataGridView();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PackageDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // PackageDataGrid
            // 
            this.PackageDataGrid.AllowUserToAddRows = false;
            this.PackageDataGrid.AllowUserToDeleteRows = false;
            this.PackageDataGrid.AllowUserToOrderColumns = true;
            this.PackageDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PackageDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PackageDataGrid.Location = new System.Drawing.Point(12, 56);
            this.PackageDataGrid.MultiSelect = false;
            this.PackageDataGrid.Name = "PackageDataGrid";
            this.PackageDataGrid.RowHeadersVisible = false;
            this.PackageDataGrid.Size = new System.Drawing.Size(631, 321);
            this.PackageDataGrid.TabIndex = 0;
            this.PackageDataGrid.SelectionChanged += new System.EventHandler(this.PackageDataGrid_SelectionChanged);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(568, 12);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 38);
            this.RemoveButton.TabIndex = 1;
            this.RemoveButton.Text = "&Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(12, 12);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 38);
            this.RefreshButton.TabIndex = 2;
            this.RefreshButton.Text = "Re&fresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ProvisionedPackageManagerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 407);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.PackageDataGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProvisionedPackageManagerView";
            this.Text = "Provisioned Package Manager";
            ((System.ComponentModel.ISupportInitialize)(this.PackageDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView PackageDataGrid;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button RefreshButton;
    }
}