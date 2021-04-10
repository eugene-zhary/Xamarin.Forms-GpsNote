using System;
using Prism.Behaviors;
using Prism.Common;
using Prism.Navigation;
using Xamarin.Forms;

namespace GpsNote.Behaviors
{
    public class TabbedPageNavigationBehavior : BehaviorBase<TabbedPage>
    {
        private Page _currentPage;

        #region -- Protected implementation --

        protected override void OnAttachedTo(TabbedPage bindable)
        {
            bindable.CurrentPageChanged += this.OnCurrentPageChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(TabbedPage bindable)
        {
            bindable.CurrentPageChanged -= this.OnCurrentPageChanged;
            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region -- Private helpers --
        
        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            var newPage = this.AssociatedObject.CurrentPage;

            if (this._currentPage != null)
            {
                var parameters = new NavigationParameters();
                PageUtilities.OnNavigatedFrom(this._currentPage, parameters);
                PageUtilities.OnNavigatedTo(newPage, parameters);
            }

            this._currentPage = newPage;
        }

        #endregion
    }
}
