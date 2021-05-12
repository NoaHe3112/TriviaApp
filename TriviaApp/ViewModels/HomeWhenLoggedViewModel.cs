using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaApp.Services;
using TriviaApp.Models;
using TriviaApp.Views;
using Xamarin.Essentials;

namespace TriviaApp.ViewModels
{
    class HomeWhenLoggedViewModel
    {
        public ICommand Edit => new Command(EditQ); 

        void EditQ()
        {
            Page p = new Questions();
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);
        }
        public ICommand LogOut => new Command(Out); 

        void Out()
        {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            App app = (App)App.Current;
            app.CurrentUser = null;
            SecureStorage.RemoveAll();
            Page p = new Home();
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);

        }
        public ICommand Play => new Command(PlayGame);

        async void PlayGame()
        {
           
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            AmericanQuestion amricanQuestion = await proxy.GetRandomQuestion();
            Page p = new Game(amricanQuestion, 0);
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);


        }
        public Action<Page> NavigateToPageEvent;

    }
}
