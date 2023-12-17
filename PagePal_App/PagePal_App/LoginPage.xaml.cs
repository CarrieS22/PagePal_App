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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }


        //public void Button_Clicked(object sender, EventArgs e)
        //{
        //if(txtUsername.Text == "Username123" && txtPassword.Text == "123456")
        //{
        //Navigation.PushAsync(new MainPage());
        //}
        //else
        //{
        //DisplayAlert("Oops..", "Username/Password incorrect.", "OK");
        //}
        //}

        async void Button_Clicked(object sender, EventArgs e)
        {
            var user = new BookTables.Users
            {
                UUsername = txtUsername.Text,
                UPassword = txtPassword.Text
            };

            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Error", "Username and Password cannot be empty", "OK");
                return;
            }

            var isValid = await AreCredentialsCorrect(user);
            if (isValid)
            {
                App.UserName = txtUsername.Text;
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
        }
        {
        }


        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignUp());
        }
    }
}