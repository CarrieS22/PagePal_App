using DnsClient.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PagePal_App;

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
            var isValid = AreCredentialsCorrect(user);
            if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtUsername.Text))
            {
                messageLabel.Text = "Login failed";
                txtPassword.Text = string.Empty;
            }
            else if (isValid)
            {
                App.UserName = txtUsername.Text.ToString();
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
        }

        public bool AreCredentialsCorrect(BookTables.Users user)
        {
            return txtUsername.Text == user.UUsername && txtPassword.Text == user.UPassword;
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignUp());
        }
    }
}