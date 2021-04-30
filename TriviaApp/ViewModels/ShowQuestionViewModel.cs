using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TriviaApp.ViewModels
{
    public class ShowQuestionViewModel : INotifyPropertyChanged
    {
        public string QText { get; set; }
        public string QAnswer { get; set; }
        public string[] QNotAnswers { get; set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }







}
