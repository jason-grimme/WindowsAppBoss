using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Utilities.Logging;

namespace WindowsAppBoss.Presenter.Settings
{
    public class SettingsTextPresenter : Presenter<View.Settings.ISettingsTextView>
    {

        public SettingsTextPresenter(View.Settings.ISettingsTextView view, string pathToSettingsFile)
            : base(view)
        {
            _pathToSettingsFile = pathToSettingsFile;
        }

        protected override void OnViewLoad(object sender, EventArgs e)
        {
            var asyncTask = GetAndDisplaySettingsAsync();
        }

        private async Task GetAndDisplaySettingsAsync()
        {
            try
            {
                Utilities.Logging.Logger.Log(Logger.LogSeverity.Information, "Displaying settings for ({0})", _pathToSettingsFile);
                var settingsAgent = new WindowsAppBoss.Services.Settings.WindowsAppSettingsReader(_pathToSettingsFile);
                string content = await settingsAgent.GetSettingAsTextAsync();
                this.View.SettingsText = content;
            }
            catch (Exception ex)
            {
                Utilities.Logging.Logger.Log(ex, "Displaying settings for ({0})", _pathToSettingsFile);
            }
        }

        private readonly string _pathToSettingsFile;
    }
}
