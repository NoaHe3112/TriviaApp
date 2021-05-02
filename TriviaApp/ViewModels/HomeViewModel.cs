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
            User u = null; 
            try
            {
                Task<string> TaskEmail = SecureStorage.GetAsync("email");
                Task<string> TaskPassword = SecureStorage.GetAsync("password");
                TaskEmail.Wait();
                TaskPassword.Wait();
                string email = TaskEmail.Result; 
                string password = TaskPassword.Result;
                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();

                Task<User> taskUser = proxy.LoginAsync(email, password);
                taskUser.Wait();
                u = taskUser.Result; 
            }
            catch
            { }
          
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
          
            Page p = new Game(); 
            GameViewModel game = (GameViewModel)p.BindingContext;
          
            game.Score = 0;
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);

        }
    }
}
