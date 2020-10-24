using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;

using Xamarin.Forms;

namespace cd_db
{
    public class removeCDPage : ContentPage
    {
        private ListView CDView;
        private Button removeButton;
        CD cd = new CD();
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ITS440.db3");
        public removeCDPage()
        {
            Title = "Remove CD";

            var db = new SQLiteConnection(dbPath);

            CDView = new ListView();
            CDView.ItemsSource = db.Table<CD>().OrderBy(x => x.Artist).ThenByDescending(x => x.Year);
            CDView.ItemSelected += CDView_ItemSelected;

            removeButton = new Button();
            removeButton.Text = "Remove CD";
            removeButton.FontAttributes = FontAttributes.Bold;
            removeButton.Clicked += removeButton_Clicked;

            Content = new StackLayout
            {
                Children = { CDView, removeButton }
            };
        }
        private async void removeButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(dbPath);
            db.Table<CD>().Delete(x => x.ID == cd.ID);
            await DisplayAlert("CD Removed", "CD #: " + cd.ID + "\nArtist: " + cd.Artist + "\nAlbum: " + cd.Album +
                                "\nGenre: " + cd.Genre + "\nYear: " + cd.Year, "OK");
            await Navigation.PopAsync();
        }

        private void CDView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            cd = (CD)e.SelectedItem;
        }
    }
}