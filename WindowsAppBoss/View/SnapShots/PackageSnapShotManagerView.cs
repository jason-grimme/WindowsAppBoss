using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsAppBoss.Model.SnapShots;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.View.SnapShots
{
    public partial class PackageSnapShotManagerView : Form, IPackageSnapShotManagerView
    {
        public PackageSnapShotManagerView(Model.Packages.PackageInformation package)
        {
            var manager = new Services.SnapShots.SnapShotManager();
            _presenter = new Presenter.SnapShots.SnapShotManagerPresenter(this, manager, package);
            InitializeComponent();
        }

        public string AppName
        {
            set 
            {
                this.Text = "SnapShots for " + value;
            }
        }

        public IEnumerable<SnapShot> SnapShots
        {
            set
            {
                this.SnapShotDataGrid.DataSource = value;
            }
        }


        public string LogContent
        {
            get
            {
                return this.LogTextBox.Text ?? "";
            }
            set
            {
                this.LogTextBox.Text = value;
                this.LogTextBox.SelectionStart = value.Length;
                this.LogTextBox.ScrollToCaret();
            }
          
        }

        public event EventHandler Initialize;


        #region events
        private void CreateSnapShotButton_Click(object sender, EventArgs e)
        {
            var asyncTask = CreateSnapShotWithUserInputAsync();

        }

       

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_presenter != null)
            {
                var asyncTask = _presenter.RemoveSelectedSnapShotAsync();
            }
        }

        private void InjectButton_Click(object sender, EventArgs e)
        {
            if (_presenter != null)
            {
                var asyncTask = _presenter.InjectSelectedSnapShot();
            }

        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (_presenter != null && _presenter.SelectedSnapShot != null)
            {
                System.Diagnostics.Process.Start(_presenter.SelectedSnapShot.FullPath);
            }
        }


        #endregion events


        private async Task CreateSnapShotWithUserInputAsync()
        {
            string inputName = DateTime.Now.ToString();
            do
            {
                inputName = Utilities.Prompt.ShowDialog("Please provide a name", "Filename friendly");
                if (String.IsNullOrWhiteSpace(inputName))
                {
                    inputName = DateTime.Now.ToString("yyyy-MM-dd__hh-mm-ss");
                }
            }
            while (await _presenter.DoesSnapShotExistAsync(inputName));

            await _presenter.CreateNewSnapShotAsync(inputName);
        }

        private Presenter.SnapShots.SnapShotManagerPresenter _presenter;

        private void SnapShotDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var row = this.SnapShotDataGrid.CurrentRow.DataBoundItem as SnapShot;
                if (row != null)
                {
                    _presenter.SelectedSnapShot = row;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex,  "SnapShotDataGrid_SelectionChanged");
            }
        }

     




    }
}
