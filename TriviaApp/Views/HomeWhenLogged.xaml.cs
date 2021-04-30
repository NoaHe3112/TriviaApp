using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaApp.ViewModels;

namespace TriviaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeWhenLogged : ContentPage
    {
        public HomeWhenLogged()
        {
            HomeWhenLoggedViewModel context= new HomeWhenLoggedViewModel();
            //Register to the event so the view model will be able to navigate to the monkeypage
            context.NavigateToPageEvent += NavigateToAsync;
            this.BindingContext = context;
            InitializeComponent();
        }
        public async void NavigateToAsync(Page p)
        {
            await Navigation.PushAsync(p);
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