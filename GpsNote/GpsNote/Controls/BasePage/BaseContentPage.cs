using Xamarin.Forms;

namespace GpsNote.Controls
{
    public class BaseContentPage : ContentPage
    {
        #region -- Protected implementation --
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IViewActionsHandler actionsHandler)
            {
                actionsHandler.OnAppearing();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IViewActionsHandler actionsHandler)
            {
                actionsHandler.OnDisappearing();
            }
        }

        #endregion
    }
}