using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriviaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeWhenLogged : ContentPage
    {
        public HomeWhenLogged()
        {
            InitializeComponent();
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            App.Current.Properties["UserDetail"] = null; 
            Page p = new Home();
            Navigation.PushAsync(p);
        }
    }
}