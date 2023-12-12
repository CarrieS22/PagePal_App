using PagePal_App;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: Dependency(typeof(App))]
namespace PagePal_App
{
    public partial class App : Application
    {

        public static bool IsUserLoggedIn { get; set; }
        public static string UserName { get; set; }
        private static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new
                    Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "book.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
