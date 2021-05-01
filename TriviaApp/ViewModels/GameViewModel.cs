﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaApp.Models;
using TriviaApp.Services;
using System.ComponentModel;
using TriviaApp.Views;

namespace TriviaApp.ViewModels
{
    class GameViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private AmericanQuestion a;
        public AmericanQuestion Question
        {
            get
            {
                return this.a;
            }
            set
            {
                this.a = value;
                OnPropertyChanged("Question");
            }
        }
        private Color c;
        public Color BackgroundColor
        {
            get
            {
                return this.c;
            }
            set
            {
                this.c = value;
                OnPropertyChanged("BackgroundColor");
            }
        }
        private int s; 
        public int Score
        {
            get
            {
                return this.s;
            }
            set
            {
                this.s = value;
                OnPropertyChanged("Score");
            }
        }

        private string q; 
        public string QuestionText
        {
            get
            {
                return this.q; 
            }
            set
            {
                this.q = value;
                OnPropertyChanged("QuestionText");

            }
        }
        private string[] arr; 
        public string[] Options
        {
            get
            {
                return this.arr;
            }
            set
            {
                this.arr = value;
                OnPropertyChanged("Options");

            }
        }


        public GameViewModel()
        {
           

            

        }

        public ICommand OptionClicked => new Command<Object>(optionClicked);

        async void optionClicked(Object o)
        {
            if(o is string)
            {
                if(((string)o).Equals(Question.CorrectAnswer)) {
                    Score++;
                    //this.BackgroundColor = new Color(52, 212, 100);
                }

            }
            
        
            if (Score >= 3)
            {
                bool isLoggedIn = App.Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(App.Current.Properties["IsLoggedIn"]) : false;
                Page p;
                if (!isLoggedIn)
                {
                    p = new LogIn(); 
                }
                else
                {
                    p = new AddQuestion();
                }
                
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);
            }
            else
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
                game.Score = this.Score;
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);

            }


        }
        public Action<Page> NavigateToPageEvent;

    }
}
