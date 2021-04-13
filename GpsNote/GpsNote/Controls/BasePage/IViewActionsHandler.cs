using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNote.Controls
{
    public interface IViewActionsHandler
    {
        void OnAppearing();
        void OnDisappearing();
    }
}
