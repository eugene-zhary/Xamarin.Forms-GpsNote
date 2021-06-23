using Xamarin.Forms;

namespace GpsNote.Views
{
    public partial class MapPage : BaseContentPage
    {
        public MapPage()
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
