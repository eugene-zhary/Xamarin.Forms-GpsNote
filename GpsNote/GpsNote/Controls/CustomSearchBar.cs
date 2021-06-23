using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNote.Controls
{
    public class CustomSearchBar : SearchBar
    {
        public CustomSearchBar()
        {
            Unfocused += CustomSearchBar_Unfocused;
        }

        #region -- Public properties --

        public static readonly BindableProperty UnfocusedCommandProperty = BindableProperty.Create(
            propertyName: nameof(UnfocusedCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(BindableMap));

        public ICommand UnfocusedCommand
        {
            get => (ICommand)GetValue(UnfocusedCommandProperty);
            set => SetValue(UnfocusedCommandProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private void CustomSearchBar_Unfocused(object sender, FocusEventArgs e)
        {
            UnfocusedCommand?.Execute(e);
        }

        #endregion
    }
}
