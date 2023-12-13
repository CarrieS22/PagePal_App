using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PagePal_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBookPage : ContentPage
    {
        public AddBookPage()
        {
            InitializeComponent();
        }

        BookTables.Books _bewks;
        public AddBookPage(BookTables.Books emp)
        {
            InitializeComponent();
            Title = "Edit Information";
            _bewks = emp;
            BookTitle.Text = emp.BookTitle;
            AuthorLastName.Text = emp.AuthorLastName;
            AuthorFirstName.Text = emp.AuthorFirstName;
            genrePicker.SelectedItem = emp.Genre;
            BookTitle.Focus();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BookTitle.Text) || string.IsNullOrEmpty(AuthorLastName.Text) || string.IsNullOrEmpty(AuthorFirstName.Text) || IsRequired(genrePicker))
            {
                await DisplayAlert("Error", "Please fill in all required fields.", "OK");
            }
            else if(_bewks != null)
            {
                UpdateBook();
            }
            else
            {
                var newBook = new BookTables.Books
                {
                    BookTitle = BookTitle.Text,
                    AuthorLastName = AuthorLastName.Text,
                    AuthorFirstName = AuthorFirstName.Text,
                    Genre = genrePicker.SelectedItem?.ToString(),
                };
                // Save the new book to the database using the SaveBookAsync method
                await App.Database.SaveBookAsync(newBook);

                // Display a success message
                await DisplayAlert("Success", "Book saved successfully!", "OK");
            }
        }

        private bool IsRequired(View view)
        {
            // This is just to check if the required fields have input or not.
            if (view is Entry entry && entry.Placeholder != null && entry.Placeholder.Contains("Enter") && string.IsNullOrEmpty(entry.Text))
                return false;
            else if (view is Picker picker && picker.Title != null && picker.Title.Contains("Select") && picker.SelectedItem == null)
                return true;
            else if (view is DatePicker datePicker && datePicker.Date == DateTime.MinValue)
                return false;

            return false;
        }

        async void UpdateBook()
        {
            _bewks.BookTitle = BookTitle.Text;
            _bewks.AuthorLastName = AuthorLastName.Text;
            _bewks.AuthorFirstName = AuthorFirstName.Text;
            _bewks.Genre = genrePicker.SelectedItem?.ToString();
            await App.Database.UpdateBook(_bewks);
            await Navigation.PopAsync();
        }
    }
}