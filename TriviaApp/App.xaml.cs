using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaApp.Views;
using TriviaApp.Models;
using System.Threading.Tasks;
using TriviaApp.Services;
using Xamarin.Essentials;

namespace TriviaApp
{
    public partial class App : Application
    {
        public User CurrentUser { get; set; }
        public App()
        {
            InitializeComponent();

            Task<string> TaskEmail = SecureStorage.GetAsync("email");
            Task<string> TaskPassword = SecureStorage.GetAsync("password");
            TaskEmail.Wait();
            TaskPassword.Wait();
            string email = TaskEmail.Result;
            string password = TaskPassword.Result;
            //if (email == null)
            //{
                CurrentUser = null; 
                MainPage = new NavigationPage(new Home());

            //}
            //else
            //{
            //    TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            //    Task<User> taskUser = proxy.LoginAsync(email, password);
            //    taskUser.Wait();
            //    CurrentUser = taskUser.Result;
            //    MainPage = new NavigationPage(new HomeWhenLogged());

            //}





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
