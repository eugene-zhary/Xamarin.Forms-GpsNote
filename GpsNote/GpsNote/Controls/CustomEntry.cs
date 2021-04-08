using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNote.Controls
{
    public class CustomEntry : Entry
    {
        public CustomEntry()
        {
            TextChanged += CustomEntry_TextChanged;
        }

        #region -- Public properties --

        public static readonly BindableProperty TextChangedCommandProperty
          = BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(CustomEntry), null);

        public ICommand TextChangedCommand
        {
            get => (ICommand)GetValue(TextChangedCommandProperty);
            set => SetValue(TextChangedCommandProperty, value);
        }

        #endregion

        #region -- Public properties --

        private void CustomEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChangedCommand.Execute(e);
        }

        #endregion
    }
}
