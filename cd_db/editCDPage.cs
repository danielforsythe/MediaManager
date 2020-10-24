using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;

using Xamarin.Forms;

namespace cd_db
{
    public class editCDPage : ContentPage
    {
        private ListView CDView;
        private Entry idEntry;
        private Entry artistEntry;
        private Entry albumEntry;
        private Entry genreEntry;
        private Entry yearEntry;
        private Button updateBtn;

        CD cd = new CD();
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ITS440.db3");
        public editCDPage()
        {
            Title = "Edit CD";
            var db = new SQLiteConnection(dbPath);

            CDView = new ListView();
            CDView.ItemsSource = db.Table<CD>().OrderBy(x => x.Artist).ThenByDescending(x => x.Year);
            CDView.ItemSelected += CDView_Selected;

            idEntry = new Entry();
            idEntry.Placeholder = "ID";
            idEntry.IsVisible = false;

            artistEntry = new Entry();
            artistEntry.Keyboard = Keyboard.Text;
            artistEntry.Placeholder = "Artist Name...";

            albumEntry = new Entry();
            albumEntry.Keyboard = Keyboard.Text;
            albumEntry.Placeholder = "Album Name...";

            genreEntry = new Entry();
            genreEntry.Keyboard = Keyboard.Text;
            genreEntry.Placeholder = "Album genre...";

            yearEntry = new Entry();
            yearEntry.Keyboard = Keyboard.Numeric;
            yearEntry.Placeholder = "Album Release Year...";

            updateBtn = new Button();
            updateBtn.Text = "Edit CD";
            updateBtn.FontAttributes = FontAttributes.Bold;
            updateBtn.Clicked += updateBtn_Clicked;

            Content = new StackLayout
            {
                Children = { CDView, artistEntry, albumEntry, genreEntry, yearEntry, updateBtn, idEntry }
            };
        }

        private async void updateBtn_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(dbPath);
            if (string.IsNullOrEmpty(artistEntry.Text) == true || string.IsNullOrEmpty(albumEntry.Text) == true ||
                string.IsNullOrEmpty(albumEntry.Text) == true || string.IsNullOrEmpty(yearEntry.Text) == true)
            {
                await DisplayAlert("Failed", "Please fill out all entry fields.", "OK");
            }
            else
            {
                CD cd = new CD()
                 {
                    ID = Convert.ToInt32(idEntry.Text),
                    Artist = artistEntry.Text,                              
                    Album = albumEntry.Text,                              
                    Genre = genreEntry.Text,                              
                    Year = int.Parse(yearEntry.Text)
                };                   
                db.Update(cd);
                await DisplayAlert("CD Edited", "CD #: " + cd.ID + " is now:" + "\nArtist: " + cd.Artist + "\nAlbum: " + cd.Album +
                                "\nGenre: " + cd.Genre + "\nYear: " + cd.Year, "OK");
                await Navigation.PopAsync();
            }           
        }
        private void CDView_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            cd = (CD)e.SelectedItem;
            idEntry.Text = cd.ID.ToString();
            artistEntry.Text = cd.Artist.ToString();
            albumEntry.Text = cd.Album.ToString();
            genreEntry.Text = cd.Genre.ToString();
            yearEntry.Text = cd.Year.ToString();
        }
    }
}