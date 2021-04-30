using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaApp.Views; 

namespace TriviaApp
{
    public partial class App : Application
    {
        public App()
        {
            bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"]) : false;
            if (!isLoggedIn)
            {
                //Load if Not Logged In
                MainPage = new NavigationPage(new Home());
            }
            else
            {
                //Load if Logged In
                MainPage = new NavigationPage(new HomeWhenLogged());
            }
            InitializeComponent();
            
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
