namespace WindowsAppBoss.View.Settings
{
    partial class SettingsTextView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsTextView));
            this.SettingsTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SettingsTextBox
            // 
            this.SettingsTextBox.Location = new System.Drawing.Point(12, 40);
            this.SettingsTextBox.Multiline = true;
            this.SettingsTextBox.Name = "SettingsTextBox";
            this.SettingsTextBox.ReadOnly = true;
            this.SettingsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SettingsTextBox.Size = new System.Drawing.Size(766, 524);
            this.SettingsTextBox.TabIndex = 0;
            this.SettingsTextBox.WordWrap = false;
            // 
            // SettingsTextView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 576);
            this.Controls.Add(this.SettingsTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsTextView";
            this.Text = "Settings Viewer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.MaximizeBox = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SettingsTextBox;
    }
}