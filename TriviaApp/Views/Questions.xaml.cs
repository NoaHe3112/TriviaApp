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
    public partial class Questions : ContentPage
    {
        public Questions()
        {
            QuestionsViewModel context = new QuestionsViewModel();
            //Register to the event so the view model will be able to navigate to the monkeypage
            context.NavigateToPageEvent += NavigateToAsync;
            this.BindingContext = context;
            InitializeComponent();
        }
        public async void NavigateToAsync(Page p)
        {
            await Navigation.PushAsync(p);
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            Page p = new AddQuestion();
            await Navigation.PushAsync(p); 
        }
    }
}