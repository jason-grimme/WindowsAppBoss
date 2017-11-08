namespace WindowsAppBoss.View.SnapShots
{
    partial class PackageSnapShotManagerView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageSnapShotManagerView));
            this.SnapShotDataGrid = new System.Windows.Forms.DataGridView();
            this.CreateSnapShotButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.InjectButton = new System.Windows.Forms.Button();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SnapShotDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SnapShotDataGrid
            // 
            this.SnapShotDataGrid.AllowUserToAddRows = false;
            this.SnapShotDataGrid.AllowUserToDeleteRows = false;
            this.SnapShotDataGrid.AllowUserToOrderColumns = true;
            this.SnapShotDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SnapShotDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SnapShotDataGrid.Location = new System.Drawing.Point(12, 169);
            this.SnapShotDataGrid.MultiSelect = false;
            this.SnapShotDataGrid.Name = "SnapShotDataGrid";
            this.SnapShotDataGrid.ReadOnly = true;
            this.SnapShotDataGrid.RowHeadersVisible = false;
            this.SnapShotDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SnapShotDataGrid.Size = new System.Drawing.Size(665, 350);
            this.SnapShotDataGrid.TabIndex = 0;
            this.SnapShotDataGrid.SelectionChanged += new System.EventHandler(this.SnapShotDataGrid_SelectionChanged);
            // 
            // CreateSnapShotButton
            // 
            this.CreateSnapShotButton.Location = new System.Drawing.Point(12, 113);
            this.CreateSnapShotButton.Name = "CreateSnapShotButton";
            this.CreateSnapShotButton.Size = new System.Drawing.Size(154, 43);
            this.CreateSnapShotButton.TabIndex = 1;
            this.CreateSnapShotButton.Text = "Create";
            this.CreateSnapShotButton.UseVisualStyleBackColor = true;
            this.CreateSnapShotButton.Click += new System.EventHandler(this.CreateSnapShotButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(582, 113);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(95, 43);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // InjectButton
            // 
            this.InjectButton.Location = new System.Drawing.Point(480, 113);
            this.InjectButton.Name = "InjectButton";
            this.InjectButton.Size = new System.Drawing.Size(95, 43);
            this.InjectButton.TabIndex = 3;
            this.InjectButton.Text = "Apply";
            this.InjectButton.UseVisualStyleBackColor = true;
            this.InjectButton.Click += new System.EventHandler(this.InjectButton_Click);
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(13, 22);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogTextBox.Size = new System.Drawing.Size(664, 85);
            this.LogTextBox.TabIndex = 4;
            this.LogTextBox.WordWrap = false;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(379, 113);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(95, 43);
            this.BrowseButton.TabIndex = 5;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // PackageSnapShotManagerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 533);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.InjectButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.CreateSnapShotButton);
            this.Controls.Add(this.SnapShotDataGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PackageSnapShotManagerView";
            this.Text = "SnapShot Manager";
            ((System.ComponentModel.ISupportInitialize)(this.SnapShotDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView SnapShotDataGrid;
        private System.Windows.Forms.Button CreateSnapShotButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button InjectButton;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.Button BrowseButton;
    }
}