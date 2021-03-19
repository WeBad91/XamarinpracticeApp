using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeBad.Apps.Views.WeatherForecast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayView : ContentView
    {
        #region Bindable properties
        

        public static readonly BindableProperty DayNameProperty =
            BindableProperty.Create(nameof(DayName), typeof(string), typeof(DayView), string.Empty);

        public static readonly BindableProperty TemperatureProperty =
            BindableProperty.Create(nameof(Temperature), typeof(string), typeof(DayView), string.Empty);
        
        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon), typeof(string), typeof(DayView), string.Empty);

        public string DayName
        {
            get { return (string)GetValue(DayNameProperty); }
            set { SetValue(DayNameProperty, value); }
        }        

        public string Temperature
        {
            get { return (string)GetValue(TemperatureProperty); }
            set { SetValue(TemperatureProperty, value); }
        }        

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        #endregion




        public DayView()
        {
            InitializeComponent();
        }
    }
}