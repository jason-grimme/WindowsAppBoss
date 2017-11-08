using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsAppBoss.View.DeveloperLicense
{
    public partial class DeveloperLicenseView : Form, IDeveloperLicenseView
    {
        public DeveloperLicenseView()
        {
            InitializeComponent();
            _presenter = new Presenter.DeveloperLicense.DeveloperLicensePresenter(this);  
        }


        public DateTime LicenseExpirationDate
        {
            set
            {
                this.StatusTextBox.Text = (value == DateTime.MinValue) ? "No license" : ("Expires on " + value.ToString());
            }
        }

        public bool IsAddEnabled
        {
            set { this.AddButton.Enabled = value; }
        }

        public bool IsRemoveEnabled
        {
            set { this.RemoveButton.Enabled = value; }
        }

        #region events
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            this.IsAddEnabled = false;
            this.IsRemoveEnabled = false;
            var asyncTask = _presenter.RemoveDeveloperLicenseAsync();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            this.IsAddEnabled = false;
            this.IsRemoveEnabled = false;
            var asyncTask = _presenter.AddDeveloperLicenseAsync();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Constants.Urls.DeveloperLicenseAbout);
        }
        #endregion events

  


        public event EventHandler Initialize;

        private readonly Presenter.DeveloperLicense.DeveloperLicensePresenter _presenter;




        


    }
}
