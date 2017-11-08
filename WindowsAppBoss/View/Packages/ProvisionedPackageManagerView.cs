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

namespace WindowsAppBoss.View.Packages
{
    public partial class ProvisionedPackageManagerView : Form,  IProvisionedPackageManagerView
    {
        public ProvisionedPackageManagerView()
        {
            InitializeComponent();
            _presenter = new Presenter.Packages.ProvisionedPackageManagerPresenter(this);
            if (Utilities.WindowLauncher.IsRunningWithElevatedPrivileges())
            {
                this.Text += Constants.Strings.RunningAsAdministrator;
            }
        }

        #region View.Packages.IProvisionedPackageManagerView
        public IList<Model.Packages.PackageInformation> ListOfPackages
        {
            set
            {
                if (value != null)
                {
                    var asRowData = value.Select<Model.Packages.PackageInformation, Model.Packages.PackageDataViewRow>((packageInfo) =>
                        {
                            return new Model.Packages.PackageDataViewRow(packageInfo);
                        });
                    
                        Utilities.Marshalling.InvokeIfNecessary(this.PackageDataGrid, () =>
                        {
                            this.PackageDataGrid.DataSource = asRowData.ToList();
                        });
                }
                else
                {
                    Utilities.Marshalling.InvokeIfNecessary(this.PackageDataGrid, () =>
                        {
                            this.PackageDataGrid.DataSource = null;
                        });
                    
                }

                
            }
        }

        public bool IsFormEnabled
        {
            get
            {
                return this.Enabled;
            }
            set
            {
                Utilities.Marshalling.InvokeIfNecessary(this, () =>
                {
                    this.Enabled = value;
                });
            }
        }
        #endregion View.Packages.IProvisionedPackageManagerView

       

        private void PackageDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var row = this.PackageDataGrid.CurrentRow.DataBoundItem as Model.Packages.PackageDataViewRow;
                if (row != null && _presenter != null)
                {
                    _presenter.SelectedRow = row.GetOriginalData();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "PackageDataGrid_SelectionChanged");
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (_presenter != null)
            {
                _presenter.OnRemoveButtonClick();
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            if (_presenter != null)
            {
                _presenter.OnRefreshButtonClick();
            }
        }

        public event EventHandler Initialize;
        private readonly Presenter.Packages.ProvisionedPackageManagerPresenter _presenter;
    }
}
