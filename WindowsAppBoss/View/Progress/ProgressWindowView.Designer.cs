namespace WindowsAppBoss.View.Progress
{
    partial class ProgressWindowView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressWindowView));
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProgressTaskLabel = new System.Windows.Forms.Label();
            this.ProgressSubTaskLabel = new System.Windows.Forms.Label();
            this.DetailsTextBox = new System.Windows.Forms.TextBox();
            this.OverallProgressText = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(35, 75);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(548, 23);
            this.ProgressBar.TabIndex = 0;
            // 
            // ProgressTaskLabel
            // 
            this.ProgressTaskLabel.AutoSize = true;
            this.ProgressTaskLabel.Location = new System.Drawing.Point(35, 120);
            this.ProgressTaskLabel.Name = "ProgressTaskLabel";
            this.ProgressTaskLabel.Size = new System.Drawing.Size(31, 13);
            this.ProgressTaskLabel.TabIndex = 1;
            this.ProgressTaskLabel.Text = "Task";
            // 
            // ProgressSubTaskLabel
            // 
            this.ProgressSubTaskLabel.AutoSize = true;
            this.ProgressSubTaskLabel.Location = new System.Drawing.Point(35, 146);
            this.ProgressSubTaskLabel.Name = "ProgressSubTaskLabel";
            this.ProgressSubTaskLabel.Size = new System.Drawing.Size(53, 13);
            this.ProgressSubTaskLabel.TabIndex = 2;
            this.ProgressSubTaskLabel.Text = "Sub Task";
            // 
            // DetailsTextBox
            // 
            this.DetailsTextBox.Location = new System.Drawing.Point(35, 173);
            this.DetailsTextBox.Multiline = true;
            this.DetailsTextBox.Name = "DetailsTextBox";
            this.DetailsTextBox.ReadOnly = true;
            this.DetailsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DetailsTextBox.Size = new System.Drawing.Size(548, 85);
            this.DetailsTextBox.TabIndex = 3;
            this.DetailsTextBox.WordWrap = false;
            // 
            // OverallProgressText
            // 
            this.OverallProgressText.AutoSize = true;
            this.OverallProgressText.Location = new System.Drawing.Point(562, 56);
            this.OverallProgressText.Name = "OverallProgressText";
            this.OverallProgressText.Size = new System.Drawing.Size(21, 13);
            this.OverallProgressText.TabIndex = 4;
            this.OverallProgressText.Text = "0%";
            // 
            // ProgressWindowView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 270);
            this.Controls.Add(this.OverallProgressText);
            this.Controls.Add(this.DetailsTextBox);
            this.Controls.Add(this.ProgressSubTaskLabel);
            this.Controls.Add(this.ProgressTaskLabel);
            this.Controls.Add(this.ProgressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProgressWindowView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Task Monitor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label ProgressTaskLabel;
        private System.Windows.Forms.Label ProgressSubTaskLabel;
        private System.Windows.Forms.TextBox DetailsTextBox;
        private System.Windows.Forms.Label OverallProgressText;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}