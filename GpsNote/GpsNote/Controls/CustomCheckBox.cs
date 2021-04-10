using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNote.Controls
{
    public class CustomCheckBox : CheckBox
    {
        #region -- Public properties --

        public static readonly BindableProperty CheckedCommandProperty
           = BindableProperty.Create(nameof(CheckedCommand), typeof(ICommand), typeof(CustomCheckBox), null);

        public static readonly BindableProperty CheckedCommandParameterProperty
           = BindableProperty.Create(nameof(CheckedCommandParameter), typeof(object), typeof(CustomCheckBox), null);


        public ICommand CheckedCommand
        {
            get => (ICommand)GetValue(CheckedCommandProperty);
            set => SetValue(CheckedCommandProperty, value);
        }

        public object CheckedCommandParameter
        {
            get => GetValue(CheckedCommandParameterProperty);
            set => SetValue(CheckedCommandParameterProperty, value);
        }

        #endregion

        #region -- Protected implementation --

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName.Equals("Renderer"))
            {
                CheckedChanged -= CustomCheckBox_CheckedChanged;
                CheckedChanged += CustomCheckBox_CheckedChanged;
            }
        }

        #endregion

        #region -- Private helpers --

        private void CustomCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckedCommand?.Execute(CheckedCommandParameter);
        }

        #endregion
    }
}
