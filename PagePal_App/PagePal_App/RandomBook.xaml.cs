using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PagePal_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RandomBook : ContentPage
	{
        DbConnection database;
        public RandomBook ()
		{
            InitializeComponent();

		}
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetBooksAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {

                await App.Database.SaveBookAsync(new Books
                {
                    BookTitle = BookTitle.Text,
                    AuthorLastName = AuthorLastName.Text,
                    AuthorFirstName = AuthorFirstName.Text,
                    Genre = Genre.Text,
                    Publication = Publication.Text,
                });
                BookTitle.Text = AuthorLastName.Text = AuthorFirstName.Text = Genre.Text = Publication.Text = string.Empty;
                collectionView.ItemsSource = await App.Database.GetBooksAsync();
        }
        async void OnButtonDelete(Object sender, EventArgs e)
        {
            await App.Database.DeleteAllItems<Books>();
        }

    }

}