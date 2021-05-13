using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaApp.Services;
using TriviaApp.Models;
using TriviaApp.Views;
using System.Text.Json;
using Xamarin.Essentials;

namespace TriviaApp.ViewModels
{
    class LogInViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
                OnPropertyChanged("Email");
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

        private string pass;
        public string Password
        {
            get
            {
                return this.pass;
            }
            set
            {
                this.pass = value;
                OnPropertyChanged("Password");
            }
        }
        public Page NextPage { get; set; } 
        
        public ICommand LogIn => new Command(Log);
        public LogInViewModel()
        {
            
        }
        public LogInViewModel(string email, string password)
        {
            Email = email;
            Password = password;
            Log(); 
        }

        async void Log()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User u = await proxy.LoginAsync(Email, Password);

            if(u != null)
            {
                App a = (App)App.Current;
                a.CurrentUser = u;
                try
                {
                    await SecureStorage.SetAsync("email", Email);
                    await SecureStorage.SetAsync("password", Password);
                }
                catch { }
              
                Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;

              
                Page p = null; 
                if(NextPage != null)
                {
                    if(NextPage is AddQuestion)
                    {
                        AddQuestionViewModel add = (AddQuestionViewModel)NextPage.BindingContext;
                        AmericanQuestion amricanQuestion = await proxy.GetRandomQuestion();
                        add.NextPage = new Game(amricanQuestion, 0);
                        p = NextPage;
                    }
                     
                }
                else
                {
                    p = new HomeWhenLogged();

                }

                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);
            }
            else
            {
                Label = "Email or password is incorrect. Please try again";
            }
        }
        public Action<Page> NavigateToPageEvent;


    }
}
