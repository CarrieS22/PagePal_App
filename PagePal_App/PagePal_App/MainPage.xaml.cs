using System;
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
        }

        private void Button_Clicked(object sender, EventArgs e)
        
        {

            Navigation.PushAsync(new BookPage());
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
