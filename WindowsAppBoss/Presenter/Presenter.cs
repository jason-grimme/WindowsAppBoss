using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppBoss.View;

namespace WindowsAppBoss.Presenter
{
    public class Presenter<TView> where TView : class, IView
    {
        private TView view;
        public TView View
        { 
            get { return view; } 
            private set { view = value; } 
        }

        public Presenter(TView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");

            View = view;
            View.Initialize += OnViewInitialize;
            View.Load += OnViewLoad;
        }

        protected virtual void OnViewInitialize(object sender, EventArgs e) { }

        protected virtual void OnViewLoad(object sender, EventArgs e) { }
    }
}
