using Xamarin.Forms;

namespace GpsNote.Views
{
    public partial class PinsPage : BaseContentPage
    {
        public PinsPage()
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
