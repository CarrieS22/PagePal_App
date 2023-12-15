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
    public partial class SignUp : ContentPage
    {
        BookTables.Users _Users;

        public SignUp()
        {
            InitializeComponent();
        }

        public SignUp(BookTables.Users emp)
        {
            InitializeComponent();
            Title = "Edit User Information";
            _Users = emp;
            PopulateUserData(emp);
        }

        private void PopulateUserData(BookTables.Users emp)
        {
            username.Text = emp.UUsername;
            email.Text = emp.email;
            firstname.Text = emp.UFirstName;
            lastname.Text = emp.ULastName;
            password.Text = emp.UPassword;
            username.Focus();
        }

        private async void Signup_Clicked(object sender, EventArgs e)
        {
            var validationError = ValidateForm();
            if (!string.IsNullOrEmpty(validationError))
            {
                await DisplayAlert("Error", validationError, "OK");
                return;
            }

            try
            {
                if (_Users != null)
                {
                    await UpdateUser();
                }
                else
                {
                    await CreateUser();
                }
            }
            catch
            {
                await DisplayAlert("Error", "An error occurred. Please try again.", "OK");
            }
        }

        private async Task CreateUser()
        {
            var existingUser = await App.Database.GetUserByEmailAsync(email.Text);
            if (existingUser != null)
            {
                await DisplayAlert("Error", "Email already in use. Please use a different email.", "OK");
                return;
            }

            var newUser = new BookTables.Users
            {
                // User details initialization
            };

            await App.Database.SaveUserAsync(newUser);
            await DisplayAlert("Success", "Registration Successful!", "OK");
            await NavigateToMainPage();
        }

        private async Task UpdateUser()
        {
            _Users.UUsername = username.Text;
            _Users.email = email.Text;
            _Users.UFirstName = firstname.Text;
            _Users.ULastName = lastname.Text;
            _Users.UPassword = password.Text;
            await App.Database.UpdateUser(_Users);
            await DisplayAlert("Success", "User Updated Successfully", "OK");

            // Navigate to the MainPage
            await NavigateToMainPage();
        }

        private async Task NavigateToMainPage()
        {
            await Navigation.PushAsync(new LoginPage());
        }

        //This checks all required fields have input
        private string ValidateForm()
        {
            if (string.IsNullOrEmpty(username.Text))
                return "Username is required.";
            if (string.IsNullOrEmpty(email.Text) || !IsValidEmail(email.Text))
                return "A valid email is required.";
            if (string.IsNullOrEmpty(firstname.Text))
                return "First name is required.";
            if (string.IsNullOrEmpty(lastname.Text))
                return "Last name is required.";
            if (string.IsNullOrEmpty(password.Text))
                return "Password is required.";
            if (password.Text != confirmPassword.Text)
                return "Passwords do not match.";

            return string.Empty; // No error
        }
        //This is to check for a valid email address
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}