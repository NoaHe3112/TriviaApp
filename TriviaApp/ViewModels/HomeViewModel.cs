using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Windows.Input;

namespace TriviaApp.ViewModels
{
    class HomeViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public Action<Page> NavigateToPageEvent;
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
    }
}
