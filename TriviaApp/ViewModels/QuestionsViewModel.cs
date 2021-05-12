using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using TriviaApp.Models;
using TriviaApp.Views;
using TriviaApp.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace TriviaApp.ViewModels
{
    class QuestionsViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        //Properties
      
        public ObservableCollection<AmericanQuestion> QuestionList { get; }
        private int c;
        public int Counter
        {
            get
            {
                return this.c;
            }
            set
            {
                this.c = value;
                OnPropertyChanged("Counter");
            }
        }

        private bool able;
        public bool Able
        {
            get
            {
                return this.able;
            }
            set
            {
                this.able = value;
                OnPropertyChanged("Able");
            }
        }

        #endregion
        //Constructor
        public QuestionsViewModel()
        {
            App a = (App)App.Current;
            User u = a.CurrentUser;
            QuestionList = new ObservableCollection<AmericanQuestion>();
            for(int i =0; i <  u.Questions.Count; i++)
            {
                QuestionList.Add(u.Questions[i]);
            }
            Counter = 0;
            Able = false; 
        }
       

        #region Commands
        //Commands and methods connected to the commands

        //Selection changed 
        public ICommand SelctionChanged => new Command<Object>(OnSelectionChanged);
        public void OnSelectionChanged(Object obj)
        {
            if (obj is AmericanQuestion)
            {
                AmericanQuestion chosenQuestion = (AmericanQuestion)obj;
                Page questionPage = new ShowQuestion();
                ShowQuestionViewModel qContext = new ShowQuestionViewModel
                {
                    QText = chosenQuestion.QText,
                    QAnswer = chosenQuestion.CorrectAnswer,
                    QNotAnswers = chosenQuestion.OtherAnswers
                };
                questionPage.BindingContext = qContext;
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(questionPage);
            }
        }
        //Delete question
        public ICommand DeleteCommand => new Command<AmericanQuestion>(RemoveQuestion);
        async void RemoveQuestion(AmericanQuestion a)
        {
            if (QuestionList.Contains(a))
            {
                try
                {

                    TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                    await proxy.DeleteQuestion(a);
                    QuestionList.Remove(a);
                }
                catch (Exception e) { }
                
                
            }
            Counter++;
            Able = true; 

        }

        //The add button will be displayed only if the counter is bigger than 0
        public ICommand Add => new Command(AddQ);
        void AddQ()
        {

            Counter--;
            if (Counter <= 0)
                Able = false;
            Page p = new AddQuestion();
            AddQuestionViewModel a = (AddQuestionViewModel)p.BindingContext;
            a.NextPage = new Questions(); 
           
            
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);

        }
        

        #endregion

        #region Events
        //Events
        //This event is used to navigate to the question page
        public Action<Page> NavigateToPageEvent;
        #endregion
    }


}

