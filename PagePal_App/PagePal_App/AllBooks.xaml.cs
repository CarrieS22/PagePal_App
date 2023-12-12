using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PagePal_App;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PagePal_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllBooks : ContentPage
    {
        public AllBooks()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                myCollectionView.ItemsSource = await App.Database.GetBooksAsync();
            }
            catch { }
        }

        async void ToolbarItem_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddBookPage());
        }
        async void SwipeItem_Invoked(Object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var emp = item.CommandParameter as BookTables.Books;
            await Navigation.PushAsync(new AddBookPage(emp));
        }
        async void SwipeItem_Invoked_1(Object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var boo = item.CommandParameter as BookTables.Books;
            var result = await DisplayAlert("Delete", $"Delete {boo.BookTitle} from the database", "Yes", "No");
            if(result)
            {
                await App.Database.DeleteBook(boo);
                myCollectionView.ItemsSource = await App.Database.GetBooksAsync();
            }
        }
    }
}