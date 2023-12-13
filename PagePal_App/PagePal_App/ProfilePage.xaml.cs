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
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
            UserProfile.Text = App.UserName + " Profile";
        }
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                myCollectionView.ItemsSource = await App.Database.GetUsers();
            }
            catch { }
        }

        async void ToolbarItem_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }
        async void SwipeItem_Invoked(Object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var emp = item.CommandParameter as BookTables.Users;
            await Navigation.PushAsync(new SignUp(emp));
        }
        async void SwipeItem_Invoked_1(Object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var use = item.CommandParameter as BookTables.Users;
            var result = await DisplayAlert("Delete", $"Delete {use.UUsername} from the database", "Yes", "No");
            if (result)
            {
                await App.Database.DeleteUser(use);
                myCollectionView.ItemsSource = await App.Database.GetUsers();
            }
        }

        async void Handle_T(object sender, System.EventArgs e)
        {
            var item = sender as TapGestureRecognizer;
            var emp = item.CommandParameter as BookTables.Users;
            await Navigation.PushAsync(new SignUp(emp));
        }
    }
}