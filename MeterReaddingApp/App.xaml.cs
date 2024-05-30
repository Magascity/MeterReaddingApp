
using MeterReaddingApp.Pages;

namespace MeterReaddingApp
{
    public partial class App : Application
    {
        //public App()
        //{
        //    InitializeComponent();

        //    MainPage = new AppShell();
        //}


        public App()
        {
            InitializeComponent();

            var accessToken = Preferences.Get("accesstoken", string.Empty);
            if (string.IsNullOrEmpty(accessToken))
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new HomePage();
            }
        }
    }
}
