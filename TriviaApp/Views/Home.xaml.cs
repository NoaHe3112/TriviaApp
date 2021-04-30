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
    public partial class Home : ContentPage
    {
        public Home()
        {
            this.BindingContext = new HomeViewModel(); 
            InitializeComponent();

        }


        private async void Play_Clicked(object sender, EventArgs e)
        {
            Page p = new Game();
            await Navigation.PushAsync(p);
        }

        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            Page p = new SignUp();
            await Navigation.PushAsync(p);
        }

        private async void LogIn_Clicked(object sender, EventArgs e)
        {
            Page p = new LogIn();
            await Navigation.PushAsync(p);
        }
        public async void NavigateToAsync(Page p)
        {
            await Navigation.PushAsync(p);
        }

    }
}