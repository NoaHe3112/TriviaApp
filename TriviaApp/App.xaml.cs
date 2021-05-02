using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaApp.Views;
using TriviaApp.Models;

namespace TriviaApp
{
    public partial class App : Application
    {
        public User CurrentUser { get; set; }
        public App()
        {
            CurrentUser = null; 
                MainPage = new NavigationPage(new Home());
            
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
