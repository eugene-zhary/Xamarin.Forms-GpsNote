﻿using GpsNote.Controls;
using Xamarin.Forms;

namespace GpsNote.Views.Pins
{
    public partial class PinsView : BaseContentPage
    {
        public PinsView()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var list_view = sender as ListView;
            list_view.SelectedItem = null;
        }
    }
}