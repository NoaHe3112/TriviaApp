using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Windows.Input;
using TriviaApp.Services;
using TriviaApp.Models;
using TriviaApp.Views;
using TriviaApp.ViewModels;
using System.Text.Json;


namespace TriviaApp.ViewModels
{
    class HomeViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public Action<Page> NavigateToPageEvent;

        //Properties
        private bool sign;
        public bool Sign
        {
            get
            {
                return this.sign;
            }
            set
            {
                this.sign = value;
                OnPropertyChanged("Sign");
            }
        }

        private bool log;
        public bool Log
        {
            get
            {
                return this.log;
            }
            set
            {
                this.log = value;
                OnPropertyChanged("Log");
            }
        }

        //Constructor 
        public HomeViewModel()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            User u = JsonSerializer.Deserialize<User>(App.Current.Properties["UserDetail"].ToString(), options);
            if (u != null)
            {
                Page p = new HomeWhenLogged(); 
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);
            }
        }

        //Commands
        public ICommand Play => new Command(play);

        async void play()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            AmericanQuestion a = await proxy.GetRandomQuestion();
            string[] options = new string[4];
            Random r = new Random();
            int num = r.Next(0, 4);
            options[num] = a.CorrectAnswer;
            for (int i = 0, optionNum = 0; i < options.Length; i++)
            {
                if (options[i] == null)
                {
                    options[i] = a.OtherAnswers[optionNum];
                    optionNum++;
                }
            }
            Page p = new Game(); 
            GameViewModel game = (GameViewModel)p.BindingContext;
            game.Options = options;
            game.Question = a;
            game.QuestionText = a.QText;
            game.Score = 0;
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);

        }
    }
}
