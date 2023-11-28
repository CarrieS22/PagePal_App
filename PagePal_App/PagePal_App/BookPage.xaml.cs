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
    public partial class BookPage : ContentPage
    {
        public BookPage(Books selectedBook)
        {
            InitializeComponent();

            // Populate the BookPage with the selected book's details
            titleLabel.Text = selectedBook.BookTitle;
            authorLabel.Text = $"{selectedBook.AuthorFirstName} {selectedBook.AuthorLastName}";
            genreLabel.Text = selectedBook.Genre;
        }

    }
}