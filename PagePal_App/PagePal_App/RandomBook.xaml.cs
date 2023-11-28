using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PagePal_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RandomBook : ContentPage
    {
        public ObservableCollection<Books> BookCollection { get; set; }

        public RandomBook()
        {
            InitializeComponent();
            BookCollection = new ObservableCollection<Books>();
            collectionView.ItemsSource = BookCollection;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var books = await App.Database.GetBooksAsync();
            foreach (var book in books)
            {
                BookCollection.Add(book);
            }
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (BookCollection.Count > 0)
            {
                // Generate a random index to pick a book
                Random random = new Random();
                int randomIndex = random.Next(0, BookCollection.Count);

                // Get the randomly selected book
                var randomBook = BookCollection[randomIndex];

                // Display the randomly selected book's information
                DisplayAlert("Random Book", $"Title: {randomBook.BookTitle}\nAuthor: {randomBook.AuthorLastName} {randomBook.AuthorFirstName}\nGenre: {randomBook.Genre}", "OK");
            }
            else
            {
                DisplayAlert("Error", "No books available in the database.", "OK");
            }
        }


    private async void OnButtonDelete(object sender, EventArgs e)
        {
            await App.Database.DeleteAllItems<Books>();
            BookCollection.Clear();
        }
    }
}
