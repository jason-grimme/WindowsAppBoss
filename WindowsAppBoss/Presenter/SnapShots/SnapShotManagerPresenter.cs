using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.Model.SnapShots;
using WindowsAppBoss.Utilities.Logging;
using WindowsAppBoss.View.SnapShots;

namespace WindowsAppBoss.Presenter.SnapShots
{
    public class SnapShotManagerPresenter : Presenter<View.SnapShots.IPackageSnapShotManagerView>
    {
        public SnapShotManagerPresenter(IPackageSnapShotManagerView view, Services.SnapShots.SnapShotManager manager, Model.Packages.PackageInformation packageInformation)
            : base(view)
        {
            _snapShotManager = manager;
            _packageInformation = packageInformation;
        }

        protected override void OnViewLoad(object sender, EventArgs e)
        {
            var asyncTask = PopulateListAsync();
            View.AppName = _packageInformation.Name;
        }

        public SnapShot SelectedSnapShot
        {
            get;
            set;
        }

        public Task<bool> DoesSnapShotExistAsync(string name)
        {
            return _snapShotManager.DoesSnapShotAlreadyExistAsync(_packageInformation, name);
        }


        public async Task<bool> RemoveSelectedSnapShotAsync()
        {
            bool success = false;
            try
            {
                success = await _snapShotManager.RemoveSnapShotAsync(_packageInformation, SelectedSnapShot.Name);
                Logger.Log(Logger.LogSeverity.Information, "Removing snap shot ({0}).  Result: {1}", SelectedSnapShot.Name, success);
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Removing snap shot ({0})", SelectedSnapShot.FullPath);
                LogText(true, ex.Message);
            }
            LogText(true, "Removed {0}? : {1}", SelectedSnapShot.Name, success);
            await PopulateListAsync();
            return success;
        }

        public async Task<bool> InjectSelectedSnapShot()
        {
            bool success = false;
            try
            {
                success = await _snapShotManager.InjectSnapShotAsync(_packageInformation, SelectedSnapShot.Name);
                Logger.Log(Logger.LogSeverity.Information, "Inecting snap shot ({0}).  Result: {1}", SelectedSnapShot.Name, success);
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Injecting snap shot ({0})", SelectedSnapShot.FullPath);
                LogText(true, ex.Message);
                LogText(true, "If the app is running, try closing it");
            }
            LogText(true, "Injected {0}? : {1}", SelectedSnapShot.Name, success);
            await PopulateListAsync();
            return success;
        }


        public async Task CreateNewSnapShotAsync(string name)
        {
            string path = null;
            try
            {
                path = await _snapShotManager.GenerateNewSnapShotAsync(_packageInformation, name);
                Logger.Log(Logger.LogSeverity.Information, "Created snap shot ({0}).  Result: {1}", SelectedSnapShot.Name, path);
            }
            catch (Exception ex)
            {
                LogText(true, ex.Message);
                LogText(true, "If the app is running, try closing it");
                Logger.Log(ex, "Creating snap shot ({0})", name);
            }
            if(String.IsNullOrWhiteSpace(path))
            {
                LogText(true, "Failed to create {0}.", name);
            }else{
                LogText(true, "Saved {0} at {1}", name, path);
            }
            await PopulateListAsync();
        }

        private async Task PopulateListAsync()
        {
            var list = await _snapShotManager.GetSnapShotsAsync(_packageInformation);
            this.View.SnapShots = list;
        }

        public void LogText(bool append, string text)
        {
            if (append)
            {
                View.LogContent += text + Environment.NewLine;
            }
            else
            {
                View.LogContent = text;
            }
        }

        public void LogText(bool append, string format, params object[] arg)
        {
            LogText(append, String.Format(format, arg));
        }


        private readonly Model.Packages.PackageInformation _packageInformation;
        private readonly Services.SnapShots.SnapShotManager _snapShotManager;
    }
}
