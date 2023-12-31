﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PagePal_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateBook : ContentPage
    {
        public UpdateBook()
        {
            InitializeComponent();
        }
        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            // Input verification
            if (IsRequired(BookTitle) && IsRequired(AuthorLastName) && IsRequired(genrePicker) && IsRequired(AuthorFirstName))
            {
                // Create a new Books object with the data from the input fields
                var newBook = new BookTables.Books
                {
                    BookTitle = BookTitle.Text,
                    AuthorLastName = AuthorLastName.Text,
                    AuthorFirstName = AuthorFirstName.Text,
                    Genre = genrePicker.SelectedItem?.ToString(),
                };

                // Update the new book to the database using the UpdateBook method
                await App.Database.UpdateBook(newBook);
                await Navigation.PopAsync();

                // Display a success message
                await DisplayAlert("Success", "Book updated successfully!", "OK");

                // Clear the input fields
                BookTitle.Text = AuthorLastName.Text = AuthorFirstName.Text = string.Empty;
                genrePicker.SelectedItem = null;
            }
            else
            {
                await DisplayAlert("Error", "Please fill in all required fields.", "OK");
            }
        }
        private bool IsRequired(View view)
        {
            // This is just to check if the required fields have input or not.
            if (view is Entry entry && entry.Placeholder != null && entry.Placeholder.Contains("Enter") && string.IsNullOrEmpty(entry.Text))
                return false;
            else if (view is Picker picker && picker.Title != null && picker.Title.Contains("Select") && picker.SelectedItem == null)
                return false;
            else if (view is DatePicker datePicker && datePicker.Date == DateTime.MinValue)
                return false;

            return true;
        }
    }

}