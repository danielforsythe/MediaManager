using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;

using Xamarin.Forms;

namespace cd_db
{
    public class WelcomePage : ContentPage
    {
        private Button addBtn;
        private Button removeBtn;
        private Button editBtn;
        private Button viewBtn;
        private Image coverPhoto;
        public WelcomePage()
        {
            Title = "ITS440 CD Organizer";

            addBtn = new Button();
            addBtn.Text = "Add CD";
            addBtn.FontAttributes = FontAttributes.Bold;
            addBtn.BackgroundColor = Color.White;

            removeBtn = new Button();
            removeBtn.Text = "Remove CD";
            removeBtn.FontAttributes = FontAttributes.Bold;
            removeBtn.BackgroundColor = Color.White;

            editBtn = new Button();
            editBtn.Text = "Edit CD";
            editBtn.FontAttributes = FontAttributes.Bold;
            editBtn.BackgroundColor = Color.White;

            viewBtn = new Button();
            viewBtn.Text = "View Collection";
            viewBtn.FontAttributes = FontAttributes.Bold;
            viewBtn.BackgroundColor = Color.White;

            addBtn.Clicked += async (sender, args) =>
            await Navigation.PushAsync(new addCDPage());

            removeBtn.Clicked += async (sender, args) =>
            await Navigation.PushAsync(new removeCDPage());

            editBtn.Clicked += async (sender, args) =>
            await Navigation.PushAsync(new editCDPage());

            viewBtn.Clicked += async (sender, args) =>
            await Navigation.PushAsync(new viewCDPage());

            coverPhoto = new Image();
            coverPhoto.Source = "CDCover.jpg";
            coverPhoto.Aspect = Aspect.AspectFill;
            coverPhoto.HorizontalOptions = LayoutOptions.End;
            coverPhoto.VerticalOptions = LayoutOptions.Fill;

            Content = new StackLayout
            {
                BackgroundColor = Color.LightGray,

                Children = { coverPhoto, addBtn, editBtn, removeBtn, viewBtn }
            };
        }
    }
}