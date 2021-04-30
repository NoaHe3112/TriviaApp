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
        public ICommand LogIn => new Command(logIn);


        async void logIn()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User u = await proxy.LoginAsync(Email, Password);
            if(u != null)
            {
                Page p = new HomeWhenLogged();
                Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;

                App.Current.Properties["UserDetail"] = u; 
                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);
            }
            else
            {
                Label = "The email or nickname is already exsits. Please try another one";
            }
        }
        public Action<Page> NavigateToPageEvent;


    }
}
