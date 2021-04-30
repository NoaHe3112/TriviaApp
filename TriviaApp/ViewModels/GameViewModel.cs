using System;
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

        public AmericanQuestion Question { get; set; }
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

        public string[] Options { get; set; }
        public string QuestionText { get; set; }




        public GameViewModel()
        {
 
        }

        public ICommand OptionClicked => new Command<Object>(optionClicked);

        async void optionClicked(Object o)
        {
            
            if(this.Equals(Question.CorrectAnswer))
            {
                this.BackgroundColor = new Color(52, 212, 100);
                Score++; 
            }
            else
            {
                
            }
            if (Score >= 3)
            {
                Page p = new AddQuestion();
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
        public Action<Page> NavigateToPageEvent;

    }
}
