using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsAppBoss.View.Settings
{
    public partial class SettingsTextView : Form, ISettingsTextView
    {

        public SettingsTextView(string pathToSettingsFile) : this(pathToSettingsFile, string.Empty)
        {
        }

        public SettingsTextView(string pathToSettingsFile, string description)
        {
            _presenter = new Presenter.Settings.SettingsTextPresenter(this, pathToSettingsFile);
            InitializeComponent();
            this.Description = description;
        }


        #region ISettingsTextView

        public string Description
        {
            set
            {
                this.Text = (value + " Settings");
            }
        }
        public string SettingsText
        {
            set { this.SettingsTextBox.Text = value; }
        }

        public event EventHandler Initialize;
        #endregion

        private readonly Presenter.Settings.SettingsTextPresenter _presenter;
    }
}
