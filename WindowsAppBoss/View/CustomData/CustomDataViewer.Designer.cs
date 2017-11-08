namespace WindowsAppBoss.View.CustomData
{
    partial class CustomDataViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomDataViewer));
            this.PackageDataGrid = new System.Windows.Forms.DataGridView();
            this.ViewDataButton = new System.Windows.Forms.Button();
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
            this.PackageDataGrid.Size = new System.Drawing.Size(631, 336);
            this.PackageDataGrid.TabIndex = 0;
            this.PackageDataGrid.SelectionChanged += new System.EventHandler(this.PackageDataGrid_SelectionChanged);
            // 
            // ViewDataButton
            // 
            this.ViewDataButton.Location = new System.Drawing.Point(556, 12);
            this.ViewDataButton.Name = "ViewDataButton";
            this.ViewDataButton.Size = new System.Drawing.Size(87, 38);
            this.ViewDataButton.TabIndex = 1;
            this.ViewDataButton.Text = "View";
            this.ViewDataButton.UseVisualStyleBackColor = true;
            this.ViewDataButton.Click += new System.EventHandler(this.ViewDataButton_Click);
            // 
            // CustomDataViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 415);
            this.Controls.Add(this.ViewDataButton);
            this.Controls.Add(this.PackageDataGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CustomDataViewer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.MaximizeBox = false;
            this.Text = "Packages with Custom.Data";
            ((System.ComponentModel.ISupportInitialize)(this.PackageDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView PackageDataGrid;
        private System.Windows.Forms.Button ViewDataButton;
    }
}