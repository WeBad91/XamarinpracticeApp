using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBad.Apps.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeBad.Apps.Views.Hangman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HangmanPage : ContentPage, IDisposable
    {
        private readonly ControlTemplate vericalView = new HangmanVerticalView();
        private readonly ControlTemplate horizontalView = new HangmanHorizontalView();

        private HangmanViewModel viewModel;
        public HangmanPage()
        {            
            InitializeComponent();
            viewModel = new HangmanViewModel();
            BindingContext = viewModel;
                        
            SetTemplate(DeviceDisplay.MainDisplayInfo);
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;

        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            SetTemplate(e.DisplayInfo);
        }

        private void SetTemplate(DisplayInfo displayInfo)
        {
            if (displayInfo.Orientation == DisplayOrientation.Landscape)
            {
                ControlTemplate = horizontalView;
            }
            else
            {
                ControlTemplate = vericalView;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.NewGameCommand.Execute(null);
        }

        public void Dispose()
        {
            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
        }
    }
}