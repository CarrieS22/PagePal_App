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
    public partial class AddBookPage : ContentPage
    {
        public AddBookPage()
        {
            InitializeComponent();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            //Input verification
            if (IsRequired(bookTitleEntry) && IsRequired(authorEntry) && IsRequired(genrePicker) && IsRequired(publicationDatePicker))
            {
                // Here is where we will save the data to our database once Eddie gets that sorted. Or Carrie will just have to host and run and connect lol
          
            }
            else
            {
                DisplayAlert("Error", "Please fill in all required fields.", "OK");
            }
        }

        private bool IsRequired(View view)
        {
            // This is just to check if the x:Required fields have input or not.
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