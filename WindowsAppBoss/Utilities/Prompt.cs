using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsAppBoss.Utilities
{
    public static class Prompt
    {
        
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 300;
            prompt.Height = 200;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 10, Top = 20, Text = text, Width = 250, IsAccessible=false, };
            TextBox textBox = new TextBox() { Left = 10, Top = 50, Width = 250, IsAccessible = true, TabIndex=0, AcceptsTab=false, AcceptsReturn=false};
            textBox.Focus();
            Button okayButton = new Button() { Text = "Ok", Left = 10, Width = 75, Top = 70, IsAccessible = true, TabIndex=1};
            Button cancelButton = new Button() { Text = "Cancel", Left = 100, Width = 75, Top = 70, IsAccessible = true, TabIndex = 2 };
            okayButton.Click += (sender, e) => { prompt.Close(); };
            cancelButton.Click += (sender, e) => { textBox.Text = string.Empty; prompt.Close(); };
            prompt.Controls.Add(okayButton);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(cancelButton);
            prompt.ShowDialog();
            return textBox.Text;
        }
    }
}
