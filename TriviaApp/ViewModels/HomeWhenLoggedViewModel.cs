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
        public ICommand Edit => new Command(edit); 

        void edit()
        {
            Page p = new Questions();
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);
        }
        public ICommand LogOut => new Command(logOut); 

        void logOut()
        {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            App app = (App)App.Current;
            app.CurrentUser = null;
            SecureStorage.RemoveAll();
            Page p = new Home();
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);

        }
        public ICommand Play => new Command(play);

        async void play()
        {
            //TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            //AmericanQuestion a = await proxy.GetRandomQuestion();
            //string[] options = new string[4];
            //Random r = new Random();
            //int num = r.Next(0, 4);
            //options[num] = a.CorrectAnswer;
            //for(int i=0, optionNum =0; i < options.Length; i++)
            //{
            //    if(options[i] == null)
            //    {
            //        options[i] = a.OtherAnswers[optionNum];
            //        optionNum++;
            //    }
            //}

            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            AmericanQuestion amricanQuestion = await proxy.GetRandomQuestion();
            Page p = new Game(amricanQuestion, 0);
            //GameViewModel game = (GameViewModel)p.BindingContext;
            //game.Options = options;
            //game.Question = a;
            //game.QuestionText = a.QText;
            //game.Score = 0; 
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);


        }
        public Action<Page> NavigateToPageEvent;

    }
}
