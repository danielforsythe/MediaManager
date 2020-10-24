using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;

using Xamarin.Forms;

namespace cd_db
{
    public class viewCDPage : ContentPage
    {
        private ListView CDView;

        CD cd = new CD();
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ITS440.db3");
        public viewCDPage()
        {
            Title = "View CD";

            var db = new SQLiteConnection(dbPath);

            CDView = new ListView();
            CDView.ItemsSource = db.Table<CD>().OrderBy(x => x.Artist).ThenByDescending(x => x.Year);
            CDView.ItemSelected += CDView_Selected;

            Content = new StackLayout
            {
                Children = { CDView }
            };
        }
        private async void CDView_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            cd = (CD)e.SelectedItem;
            await DisplayAlert("CD Selected", "Artist: " + cd.Artist + "\nAlbum: " + cd.Album +
                                "\nGenre: " + cd.Genre + "\nYear: " + cd.Year, "OK");
        }
    }
}