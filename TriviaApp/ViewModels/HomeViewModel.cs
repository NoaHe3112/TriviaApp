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
using System.Threading.Tasks;
using Xamarin.Essentials;

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
            //User u = null; 
            //try
            //{
                
            //}
            //catch
            //{ }
          
            //if (u != null)
            //{
            //    Page p = new HomeWhenLogged(); 
            //    if (NavigateToPageEvent != null)
            //        NavigateToPageEvent(p);
            //}
        }

        //Commands

        public ICommand SignUp => new Command(Signup); 
        void Signup()
        {
            Page p = new SignUp();
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);
        }
        public ICommand LogIn => new Command(UserLogIn); 
        void UserLogIn()
        {
            Page p = new LogIn();
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);
        }


           
        public ICommand Play => new Command(PlayGame);


        async void PlayGame()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            AmericanQuestion amricanQuestion = await proxy.GetRandomQuestion();
            Page p = new Game(amricanQuestion, 0); 
            GameViewModel game = (GameViewModel)p.BindingContext;
          
            game.Score = 0;
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);

        }
    }
}
