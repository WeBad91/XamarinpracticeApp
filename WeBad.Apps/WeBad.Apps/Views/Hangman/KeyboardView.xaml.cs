using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeBad.Apps.Views.Hangman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KeyboardView : ContentView
    {
        private readonly string[] keys = { "A", "B", "V", "G", "D", "Đ", "E", "Ž", "Z", "I"
                                         , "J", "K", "L", "LJ", "M", "N", "NJ", "O", "P"
                                         , "R", "S", "T", "Ć", "U", "F", "H", "C", "Č", "DŽ", "Š"};
        private List<Button> keyButtons;

        #region Bindable Properties
        
        public static readonly BindableProperty KeyCommandProperty =
            BindableProperty.Create(nameof(KeyCommand), typeof(ICommand), typeof(KeyboardView), null);

        public static readonly BindableProperty SelectedKeysProperty =
            BindableProperty.Create(nameof(SelectedKeys), typeof(ObservableCollection<string>), typeof(KeyboardView), new ObservableCollection<string>(), BindingMode.TwoWay);

        public static readonly BindableProperty KeyColorProperty =
            BindableProperty.Create(nameof(KeyColor), typeof(Color), typeof(KeyboardView), Color.White);

        public static readonly BindableProperty KeyTextColorProperty =
           BindableProperty.Create(nameof(KeyTextColor), typeof(Color), typeof(KeyboardView), Color.Black);

        public ICommand KeyCommand
        {
            get { return (ICommand)GetValue(KeyCommandProperty); }
            set { SetValue(KeyCommandProperty, value); }
        }
      
        public ObservableCollection<string> SelectedKeys
        {
            get { return (ObservableCollection<string>)GetValue(SelectedKeysProperty); }
            set { SetValue(SelectedKeysProperty, value); }
        }

        public Color KeyColor
        {
            get { return (Color)GetValue(KeyColorProperty); }
            set { SetValue(KeyColorProperty, value); }
        }

        public Color KeyTextColor
        {
            get { return (Color)GetValue(KeyTextColorProperty); }
            set { SetValue(KeyTextColorProperty, value); }
        }
        #endregion

        public KeyboardView()
        {
            InitializeComponent();
            CreateUI();
        }


        private async ValueTask CreateUI()
        {
            await InitKeys();
            foreach (var key in keyButtons)
            {
                KeyboardContainer.Children.Add(key);
            }
        }

        private async ValueTask InitKeys()
        {

            keyButtons = new List<Button>();

            await Task.Run(() =>
            {
                for (int i = 0; i < keys.LongLength; i++)
                {

                    Button button = new Button
                    {
                        Text = keys[i],
                        BackgroundColor = Color.White,
                        TextColor = Color.Black,
                        FontSize = keys[i].Length == 1 ? 14 : 12,
                        WidthRequest = 40,
                        Margin = 2
                    };

                    button.Clicked += delegate
                    {
                        if (KeyCommand == null) return;
                        if (KeyCommand.CanExecute(null))
                        {
                            if (SelectedKeys == null)
                                SelectedKeys = new ObservableCollection<string>();

                            button.IsEnabled = false;
                            SelectedKeys.Add(button.Text);
                            KeyCommand.Execute(button.Text);
                        }
                    };


                    keyButtons.Add(button);

                }
                
            });


        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(SelectedKeys))
            {
                foreach (var key in keyButtons)
                {
                    key.IsEnabled = SelectedKeys == null || !SelectedKeys.Any(x => x.Equals(key.Text));

                }
            }
            else if (propertyName == nameof(KeyColor))
            {
                foreach (var key in keyButtons)
                {
                    key.BackgroundColor = KeyColor;

                }
            }
            else if (propertyName == nameof(KeyTextColor))
            {
                foreach (var key in keyButtons)
                {
                    key.TextColor = KeyTextColor;
                }
            }

        }

      
    }
}