﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaApp.Models;
using TriviaApp.Services;
using System.ComponentModel;
using TriviaApp.Views;
using System.Threading.Tasks;

namespace TriviaApp.ViewModels
{
    class AnswerViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private string s;
        public string Answer
        {
            get
            {
                return this.s;
            }
            set
            {
                this.s = value;
                OnPropertyChanged("Answer");
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

    }
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
        private Answer[] arr; 
        public Answer[] Options
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
        public string[] Colors { get; set; }
        public int CorrectAnswerIndex { get; set; }


        public GameViewModel(AmericanQuestion question, int score)
        {
            try
            {
                AmericanQuestion a = question;
                Answer[] options = new Answer[4];
                Random r = new Random();
                int num = r.Next(0, 4);
                options[num] = new Answer
                {
                    text = a.CorrectAnswer,
                    color = new Color(33,205,47), 
                }; 
                for (int i = 0, optionNum = 0; i < options.Length; i++)
                {
                    if (options[i] == null)
                    {
                        options[i] = new Answer
                        {
                            text = a.OtherAnswers[optionNum],
                            color = new Color(252, 13, 13),
                        };
                        optionNum++;
                    }
                }
              

                this.Options = options;
                this.Question = a;
                this.QuestionText = a.QText;
                this.CorrectAnswerIndex = num;
                this.Score = score; 

            }
            catch (Exception e) { }


        }

        public ICommand OptionClicked => new Command<Object>(OptionClick);

        async void OptionClick(Object o)
        {
            if(o is Answer)
            {
                if(((Answer)o).text.Equals(Question.CorrectAnswer)) {
                    Score++;
                    
                }

            }
            
        
            if (Score >= 3)
            {
                bool isLoggedIn = App.Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(App.Current.Properties["IsLoggedIn"]) : false;
                Page p;
                if (!isLoggedIn)
                {
                    p = new LogIn();
                    LogInViewModel log = (LogInViewModel)p.BindingContext;
                    log.NextPage = new AddQuestion(); 
                    
                }
                else
                {
                    p = new AddQuestion();
                    AddQuestionViewModel add = (AddQuestionViewModel)p.BindingContext;
                    TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                    AmericanQuestion amricanQuestion = await proxy.GetRandomQuestion();
                    add.NextPage = new Game(amricanQuestion, 0); 
                }
                
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);
            }
            else
            {
              
                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                AmericanQuestion amricanQuestion = await proxy.GetRandomQuestion();
                Page p = new Game(amricanQuestion, Score);             
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);

            }


        }

        public ICommand Home => new Command(GoHome); 
        void GoHome()
        {
            Page p; 
            if((string)App.Current.Properties["IsLoggedIn"] == Boolean.TrueString)
            {
                p = new HomeWhenLogged(); 
            }
            else
            {
                p = new Home();
            }
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);
        }
        public Action<Page> NavigateToPageEvent;


    }
}
