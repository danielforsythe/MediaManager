using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;

using Xamarin.Forms;

namespace cd_db
{
    public class addCDPage : ContentPage
    {
        private Entry artistEntry;
        private Entry albumEntry;
        private Entry genreEntry;
        private Entry yearEntry;
        private Button saveButton;

        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ITS440.db3");
        public addCDPage()
        {
            Title = "Add CD";

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

            saveButton = new Button();
            saveButton.Text = "Save CD";
            saveButton.FontAttributes = FontAttributes.Bold;
            saveButton.Clicked += saveButton_Clicked;

            Content = new StackLayout
            {
                Children = { artistEntry, albumEntry, genreEntry, yearEntry, saveButton }
            };
        }
        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<CD>();
            var maxPK = db.Table<CD>().OrderByDescending(c => c.ID).FirstOrDefault();
            if (string.IsNullOrEmpty(artistEntry.Text) == true || string.IsNullOrEmpty(albumEntry.Text) == true ||
                string.IsNullOrEmpty(albumEntry.Text) == true || string.IsNullOrEmpty(yearEntry.Text) == true)
            {
                await DisplayAlert("Failed", "Please fill out all entry fields.", "OK");
            }
            else
            {
                CD cd = new CD()
                {
                    ID = (maxPK == null ? 1 : maxPK.ID + 1),
                    Artist = artistEntry.Text,
                    Album = albumEntry.Text,
                    Genre = genreEntry.Text,
                    Year = int.Parse(yearEntry.Text)
                };
                db.Insert(cd);
                await DisplayAlert("CD Added", "Artist: " + cd.Artist + "\nAlbum: " + cd.Album +
                                "\nGenre: " + cd.Genre + "\nYear: " + cd.Year, "OK");
                artistEntry.Text = "";
                albumEntry.Text = "";
                genreEntry.Text = "";
                yearEntry.Text = "";
            }    
        }
    }
}