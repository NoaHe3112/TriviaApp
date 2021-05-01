using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaApp.Services;
using TriviaApp.Models;
using System.Text.Json;
using TriviaApp.Views;

namespace TriviaApp.ViewModels
{
    class AddQuestionViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public void AddQuestion()
        {
            Label = ""; 
        }
        public Page LastPage { get; set; }
        private string ques;
        public string Question
        {
            get
            {
                return this.ques;
            }
            set
            {
                this.ques = value;
                OnPropertyChanged("Question");
            }
        }
        private string l;
        public string Label
        {
            get
            {
                return this.l;
            }
            set
            {
                this.l = value;
                OnPropertyChanged("Label");
            }
        }

        private string correct;
        public string CorrectAnswer
        {
            get
            {
                return this.correct;
            }
            set
            {
                this.correct = value;
                OnPropertyChanged("CorrectAnswer");
            }

        }
        private string option1;
        public string Option1
        {
            get
            {
                return this.option1;
            }
            set
            {
                this.option1 = value;
                OnPropertyChanged("Option1");
            }


        }
        private string option2;
        public string Option2
        {
            get
            {
                return this.option2;
            }
            set
            {
                this.option2 = value;
                OnPropertyChanged("Option2");
            }


        }
        private string option3;
        public string Option3
        {
            get
            {
                return this.option3;
            }
            set
            {
                this.option3 = value;
                OnPropertyChanged("Option3");
            }


        }
        public ICommand Add => new Command(add);

        async void add()
        {
            string[] arr = new string[3];
            arr[0] = Option1;
            arr[1] = Option2;
            arr[2] = Option3;
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            //string content = ReadAsStringAsync(App.Current.Properties["UserDetail"]); 


            //User u = JsonSerializer.Deserialize<User>(content, serializerOptions);
            User u = (User)App.Current.Properties["User"];
            AmericanQuestion a = new AmericanQuestion
            {
                CorrectAnswer = CorrectAnswer,
                QText = Question,
                OtherAnswers = arr,
                CreatorNickName = u.NickName,

            };
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            bool b = await proxy.PostNewQuestion(a);
            u.Questions.Add(a);
            if (b)
            {
                AmericanQuestion q = await proxy.GetRandomQuestion();
                string[] options = new string[4];
                Random r = new Random();
                int num = r.Next(0, 4);
                options[num] = q.CorrectAnswer;
                for (int i = 0, optionNum = 0; i < options.Length; i++)
                {
                    if (options[i] == null)
                    {
                        options[i] = q.OtherAnswers[optionNum];
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
            else
            {
                Label = "Something went wrong! Please try again";
            }
        }
        public Action<Page> NavigateToPageEvent;

    }
}
