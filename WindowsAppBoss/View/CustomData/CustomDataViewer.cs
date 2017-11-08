using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsAppBoss.View.CustomData
{
    public partial class CustomDataViewer : Form, ICustomDataView
    {
        public CustomDataViewer()
        {
            InitializeComponent();
            _presenter = new Presenter.CustomData.CustomDataPresenter(this);
        }

        public IList<Model.Packages.PackageDataViewRow> PackagesWithCustomData
        {
            set
            {
                Utilities.Marshalling.InvokeIfNecessary(this.PackageDataGrid, () =>
                {
                    this.PackageDataGrid.DataSource = value;
                });
                
            }
        }

        private Presenter.CustomData.CustomDataPresenter _presenter;


        public event EventHandler Initialize;

        private void PackageDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (_presenter != null)
            {
                var row = this.PackageDataGrid.CurrentRow.DataBoundItem as Model.Packages.PackageDataViewRow;
                if (row != null)
                {
                    _presenter.SelectedPackage = row.GetOriginalData();
                }
            }
        }

        private void ViewDataButton_Click(object sender, EventArgs e)
        {
            _presenter.ShowSelectedPackage();
        }
    }
}
