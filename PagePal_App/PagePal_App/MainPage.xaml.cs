﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using System.Windows;


namespace PagePal_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        DbConnection database;
        public MainPage()
        {
            InitializeComponent();

            LoadAuthors();
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

            // Retrieve books based on filters
            var filteredBooks = await App.Database.GetFilteredBooks(selectedGenre, selectedAuthor);

            // Check if there are books that match the filters
            if (filteredBooks.Any())
            {
                // Select a random book from the filtered list
                Random random = new Random();
                var randomBook = filteredBooks[random.Next(filteredBooks.Count)];

                // Display information about the randomly selected book (you can modify this as needed)
                await DisplayAlert("Random Book", $"Title: {randomBook.BookTitle}\nAuthor: {randomBook.AuthorLastName} {randomBook.AuthorFirstName}\nGenre: {randomBook.Genre}\nPublication: {randomBook.Publication}\nYear: {randomBook.yearEntry.ToShortDateString()}", "OK");
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

    }
}
