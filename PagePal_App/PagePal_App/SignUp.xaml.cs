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
		public SignUp()
		{
			InitializeComponent();
		}

        BookTables.Users _Users;
        public SignUp(BookTables.Users emp)
        {
            InitializeComponent();
            Title = "Edit User Information";
            _Users = emp;
            username.Text = emp.UUsername;
            email.Text = emp.email;
            firstname.Text = emp.UFirstName;
            lastname.Text = emp.ULastName;
            password.Text = emp.UPassword;
            username.Focus();
        }

        private async void Signup_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(username.Text) || string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(firstname.Text) || string.IsNullOrEmpty(lastname.Text) || string.IsNullOrEmpty(password.Text))
            {
                await DisplayAlert("Error", "Please fill in all required fields.", "OK");
            }
            else if (_Users != null)
            {
                UpdateUser();
            }
            else
            {
                var newUser = new BookTables.Users
                {
                    UUsername = username.Text,
                    email = email.Text,
                    UFirstName = firstname.Text,
                    ULastName = lastname.Text,
                    UPassword = password.Text,
                };
                // Save the new user to the database using the SaveUserAsync method
                await App.Database.SaveUserAsync(newUser);

                // Display a success message
                await DisplayAlert("Success", "Registration Successful!", "OK");
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

        async void UpdateUser()
        {
            _Users.UUsername = username.Text;
            _Users.email = email.Text;
            _Users.UFirstName = firstname.Text;
            _Users.ULastName = lastname.Text;
            _Users.UPassword = password.Text;
            await App.Database.UpdateUser(_Users);
            await Navigation.PopAsync();
        }
    }
}