using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaApp.ViewModels;
using TriviaApp.Services;
using TriviaApp.Models;
using System;

namespace TriviaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Game : ContentPage
    {
        public Game(AmericanQuestion a)
        {
            GameViewModel context = new GameViewModel(a);
            //Register to the event so the view model will be able to navigate
            context.NavigateToPageEvent += NavigateToAsync;
            this.BindingContext = context;
            InitializeComponent();
        }
       
        public async void NavigateToAsync(Page p)
        {
            
            await Navigation.PushAsync(p);

        }


    }
}