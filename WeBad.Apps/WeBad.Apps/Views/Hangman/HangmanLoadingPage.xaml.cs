using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeBad.Apps.Views.Hangman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HangmanLoadingPage : ContentPage
    {
        private bool isGameLoaded;
        public HangmanLoadingPage()
        {
            InitializeComponent();
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!isGameLoaded)
            {
                await Task.Delay(1000);
                await Navigation.PushAsync(new HangmanPage());
                isGameLoaded = true;
                Navigation.RemovePage(this);
            }

        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}