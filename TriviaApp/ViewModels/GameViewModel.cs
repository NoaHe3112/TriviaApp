using System;
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
        public string[] Colors { get; set; }
        public int CorrectAnswerIndex { get; set; }


        public GameViewModel(AmericanQuestion question)
        {
            try
            {
                //TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                //Task<AmericanQuestion> taskA = proxy.GetRandomQuestion();
                //taskA.Wait();
                AmericanQuestion a = question;
                string[] options = new string[4];
                string[] color = new string[4];
                Random r = new Random();
                int num = r.Next(0, 4);
                options[num] = a.CorrectAnswer;
                color[num] = "#0DFC34";
                for (int i = 0, optionNum = 0; i < options.Length; i++)
                {
                    if (options[i] == null)
                    {
                        options[i] = a.OtherAnswers[optionNum];
                        color[i] = "#FC0D0D";
                        optionNum++;
                    }
                }
              

                Page p = new Game(question);
                GameViewModel game = (GameViewModel)p.BindingContext;
                game.Options = options;
                game.Question = a;
                game.QuestionText = a.QText;
                game.CorrectAnswerIndex = num;

            }
            catch (Exception e) { }


        }

        public ICommand OptionClicked => new Command<Object>(optionClicked);

        async void optionClicked(Object o)
        {
            if(o is string)
            {
                if(((string)o).Equals(Question.CorrectAnswer)) {
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
                    add.NextPage = new Game(amricanQuestion); 
                }
                
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);
            }
            else
            {
                //TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                //AmericanQuestion a = await proxy.GetRandomQuestion();
                //string[] options = new string[4];
                //Random r = new Random();
                //int num = r.Next(0, 4);
                //options[num] = a.CorrectAnswer;
                //for (int i = 0, optionNum = 0; i < options.Length; i++)
                //{
                //    if (options[i] == null)
                //    {
                //        options[i] = a.OtherAnswers[optionNum];
                //        optionNum++;
                //    }
                //}
                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                AmericanQuestion amricanQuestion = await proxy.GetRandomQuestion();
                Page p = new Game(amricanQuestion);
                //GameViewModel game = (GameViewModel)p.BindingContext;
                //game.Options = options;
                //game.Question = a;
                //game.QuestionText = a.QText;
                //game.Score = this.Score;
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);

            }


        }
        public Action<Page> NavigateToPageEvent;

    }
}
