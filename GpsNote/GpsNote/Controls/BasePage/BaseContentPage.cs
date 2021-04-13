using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace GpsNote.Controls
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {

        }

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
    }
}