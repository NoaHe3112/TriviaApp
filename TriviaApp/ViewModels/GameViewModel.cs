using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaApp.Models;
using TriviaApp.Services;
using System.ComponentModel;

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
        public string[] Options { get; set; }
   



        public GameViewModel()
        {
           
 
        }

        public ICommand OptionClicked => new Command<Object>(optionClicked);

        async void optionClicked(Object o)
        {
            if(o is Button)
            if(this.Equals(Question.CorrectAnswer))
            {
                this.BackgroundColor = new Color(52, 212, 100); 
            }
            else
            {
                
            }
        }
    }
}
