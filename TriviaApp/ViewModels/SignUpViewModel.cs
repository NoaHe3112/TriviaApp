using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaApp.Services; 
using TriviaApp.Models;
using System.Threading.Tasks;
using TriviaApp.Views;
using System.Text.Json;

namespace TriviaApp.ViewModels
{
    class SignUpViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        
        public SignUpViewModel()
        {

            Label = ""; 
        }
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
        private string nickname;
        public string NickName
        {
            get
            {
                return this.nickname;
            }
            set
            {
                this.nickname = value;
                OnPropertyChanged("NickName");
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
        private string l;
        public string Label
        {
            get
            {
                return this.l ;
            }
            set
            {
                this.l = value;
                OnPropertyChanged("Label");
            }
        }
        public ICommand Register => new Command(register);

        async void register()
        {
            User u = new User
            {
                Email = email,
                Password = pass,
                NickName = nickname,

            };
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            bool b =  await proxy.RegisterUser(u);
            if(b)
            {
                Label = "successfully registered! please wait 5 seconds";
                //wait 5 seconds - to learn how
                await Task.Delay(50000);
                User us = await proxy.LoginAsync(Email, Password);
                App.Current.Properties["UserDetail"] = JsonSerializer.Serialize(u);
                Page p = new HomeWhenLogged();

                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);
            }
            else
            {
                Label = "The email or nickname is already exsits. Please try another one";
                Page p = new Home();

                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);

            }
            
        }
        public Action<Page> NavigateToPageEvent;

    }
}
