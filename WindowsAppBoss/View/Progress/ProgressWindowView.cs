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

namespace WindowsAppBoss.View.Progress
{
    public partial class ProgressWindowView : Form, IProgressWindowView
    {
        public ProgressWindowView()
        {
            InitializeComponent();
        }

        public string WindowTitle
        {
            set
            {
                this.Text = value;
            }
        }

        public string TaskDescription
        {
            set
            {
                this.ProgressTaskLabel.Text = value;
            }
        }

        public string SubTaskDescription
        {
            set
            {
                this.ProgressSubTaskLabel.Text = value;
            }
        }

        public int OverallProgress
        {
            set
            {
                try
                {

                    Utilities.Marshalling.InvokeIfNecessary(this, () =>
                    {
                        Utilities.Marshalling.InvokeIfNecessary(this.OverallProgressText, () =>
                        {
                            this.OverallProgressText.Text = (value + "%");
                        });

                        Utilities.Marshalling.InvokeIfNecessary(this.ProgressBar, () =>
                        {
                            this.ProgressBar.Value = (value);
                        });
                    });

                }
                catch (Exception ex)
                {
                    Logger.Log(ex, "OverallProgress View updating");
                }
            }
        }

        public string DetailsText
        {
            get
            {
                return this.DetailsTextBox.Text;

            }
            set
            {
                Utilities.Marshalling.InvokeIfNecessary(this, () =>
                {
                    Utilities.Marshalling.InvokeIfNecessary(this.DetailsTextBox, () =>
                    {
                        this.DetailsTextBox.Text = value;
                        this.DetailsTextBox.SelectionStart = value.Length;
                        this.DetailsTextBox.ScrollToCaret();
                    });
                });


                //this.DetailsTextBox.Text = value;
                //this.DetailsTextBox.SelectionStart = value.Length;
                //this.DetailsTextBox.ScrollToCaret();
            }
        }

        

        public event EventHandler Initialize;




        

        
    }
}
