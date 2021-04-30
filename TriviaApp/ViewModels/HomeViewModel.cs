using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Windows.Input;
using TriviaApp.Services;
using TriviaApp.Models;
using TriviaApp.Views;

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
            GameViewModel game = new GameViewModel
            {
                Options = options,
                Question = a,
                QuestionText = a.QText,
                Score = 0,
            };
            Page p = new Game(); 
            p.BindingContext = game;
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);

        }
    }
}
