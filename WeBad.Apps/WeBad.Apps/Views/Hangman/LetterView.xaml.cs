using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeBad.Apps.Views.Hangman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LetterView : ContentView
    {
        #region Bindable Properties

        public static readonly BindableProperty LetterProperty = BindableProperty.Create(nameof(Letter), typeof(string), typeof(char));
        public static readonly BindableProperty IsShownProperty = BindableProperty.Create(nameof(IsShown), typeof(bool), typeof(bool), false);
        public static readonly BindableProperty IsWhiteSpaceProperty = BindableProperty.Create(nameof(IsWhiteSpace), typeof(bool), typeof(bool), false);

        public string Letter
        {
            get { return (string)GetValue(LetterProperty); }
            set { SetValue(LetterProperty, value); }
        }

        public bool IsShown
        {
            get { return (bool)GetValue(IsShownProperty); }
            set { SetValue(IsShownProperty, value); }
        }

        public bool IsWhiteSpace
        {
            get { return (bool)GetValue(IsWhiteSpaceProperty); }
            set { SetValue(IsWhiteSpaceProperty, value); }
        }

        #endregion


        public LetterView()
        {
            InitializeComponent();
        }

        private void InitUI()
        {

            if (IsWhiteSpace)
            {
                ContentHolder.Opacity = 0;
            }
            else
            {
                LetterContainer.Text = "?";
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName.Equals(nameof(Letter)) || propertyName.Equals(nameof(IsWhiteSpace)))
            {
                InitUI();
            }
            else if (propertyName.Equals(nameof(IsShown)))
            {

                if (IsShown)
                {
                    LetterContainer.Text = Letter.ToString().ToUpper();
                    ContentHolder.Border.Color = Color.FromHex("#5c5CAB86");
                }
            }

            base.OnPropertyChanged(propertyName);
        }
    }
}