using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagePal_App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PagePal_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        //private Entry searchEntry;

        public MainPage()
        {
            InitializeComponent();
            LoadAuthors();
            //string name = App.UserName;
            LoggedIn.Text = "Welcome to PagePal";
            UserIN.Text = "User: " + App.UserName;
        }

        private void LoadAuthors()
        {
            // Retrieve distinct authors from the database
            var authors = App.Database.GetDistinctAuthorsAsync().Result;

            // Check if there are authors in the database
            if (authors.Any())
            {
                // Populate the Author Picker with the retrieved authors
                foreach (var author in authors)
                {
                    authorPicker.Items.Add(string.Format("{0} {1}", author.AuthorFirstName, author.AuthorLastName));
                }
            }
            else
            {
                // If no authors in the database, provide a default placeholder or handle it as needed
                authorPicker.Items.Add("No Authors Found");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Get selected filters
            string selectedGenre = genrePicker.SelectedItem as string;
            string selectedAuthor = authorPicker.SelectedItem as string;

            // Split the selected author into first and last name
            string[] authorNames = selectedAuthor?.Split(' ');

            // Retrieve books based on filters
            var filteredBooks = await App.Database.GetBooksBasedOnFiltersAsync(selectedGenre, authorNames);

            // Check if there are books that match the filters
            if (filteredBooks.Any())
            {
                // Select a random book from the filtered list
                Random random = new Random();
                var randomBook = filteredBooks[random.Next(filteredBooks.Count)];

                // Display information about the randomly selected book (you can modify this as needed)
                await DisplayAlert("Random Book", $"Title: {randomBook.BookTitle}\nAuthor: {randomBook.AuthorLastName} {randomBook.AuthorFirstName}\nGenre: {randomBook.Genre}\n", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No books found with the specified filters.", "OK");
            }
        }

        private void Button_Clicked1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddBookPage());
        }

        private void Button_Clicked2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RandomBook());
        }

        private async void Button_Clicked_AllBooks(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new AllBooks());
        }

        private void Button_Clicked_Login(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
        async void Profile_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }
        async void Signout_Clicked(Object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

    }
}
