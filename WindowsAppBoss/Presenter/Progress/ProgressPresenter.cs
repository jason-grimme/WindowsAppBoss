using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppBoss.Presenter.Progress
{
    class ProgressPresenter : Presenter<View.Progress.IProgressWindowView>
    {
        public ProgressPresenter(View.Progress.IProgressWindowView view)
            : base(view)
        {
            this.View.OverallProgress = 0;
            this.View.SubTaskDescription = string.Empty;
            this.View.TaskDescription = string.Empty;
            this.View.DetailsText = string.Empty;
        }




        protected override void OnViewLoad(object sender, EventArgs e)
        {
            
        }

        public string WindowTitle
        {
            set
            {
                View.WindowTitle = value;
            }
        }

        public string TaskDescription
        {
            set
            {
                View.TaskDescription = value;
            }
        }

        public string SubTaskDescription
        {
            set
            {
                View.SubTaskDescription = value;
            }
        }

        public double OverallProgress
        {
            set
            {
                View.OverallProgress = Convert.ToInt32(value);
            }
        }

        public void SetDetailsText(bool append, string text)
        {
            if (append)
            {
                View.DetailsText += text + Environment.NewLine;
            }
            else
            {
                View.DetailsText = text;
            }
        }

        public void SetDetailsText(bool append, string format, params object[] arg)
        {
            SetDetailsText(append, String.Format(format, arg));
        }

    }
}
