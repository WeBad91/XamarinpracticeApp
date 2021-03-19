using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeBad.Apps.Views.Hangman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermView : ContentView
    {
        private List<LetterView> letterCollection;

        #region Bindable Properties

        public static readonly BindableProperty TermProperty = BindableProperty.Create(nameof(Term), typeof(string), typeof(string));
        public static readonly BindableProperty DiscoveredLettersProperty = BindableProperty.Create(nameof(DiscoveredLetters), typeof(string), typeof(string));

        public string Term
        {
            get { return (string)GetValue(TermProperty); }
            set { SetValue(TermProperty, value); }
        }

        public string DiscoveredLetters
        {
            get { return (string)GetValue(DiscoveredLettersProperty); }
            set { SetValue(DiscoveredLettersProperty, value); }
        }

        #endregion

        public TermView()
        {
            InitializeComponent();
        }

        private async ValueTask InitUi()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var density = mainDisplayInfo.Density;
            var width = mainDisplayInfo.Width;

            if (width < 0)
                return;
            TermContainer.Children.Clear();

            int letterInARow = (int)(width / (50* density));

            letterCollection = new List<LetterView>();
            if (!string.IsNullOrEmpty(Term))
            {
                List<View> views = new List<View>();

                var words = Term.ToUpper().Split(' ').Select(x => x.Trim());

                for (int i = 0; i < words.Count(); i++)
                {
                    string word = words.ElementAt(i);

                    Grid stack = new Grid
                    {
                        Margin = new Thickness(0, 2, 0, 2)
                    };
                    int col = 0;
                    int row = 0;

                    for (int j = 0; j < word.Length; j++)
                    {
                        var letter = word[j].ToString();

                        if (j < word.Length - 1)
                        {
                            if ((letter.ToString().Equals("N") || letter.ToString().Equals("L")))
                            {
                                string nextLetter = word[j + 1].ToString();
                                if (nextLetter.Equals("J"))
                                {
                                    letter = letter + nextLetter;
                                    j++;
                                }
                            }
                            else if ((letter.ToString().Equals("D") || letter.ToString().Equals("L")))
                            {
                                string nextLetter = word[j + 1].ToString();
                                if (nextLetter.Equals("Ž"))
                                {
                                    letter = letter + nextLetter;
                                    j++;
                                }
                            }
                        }

                        LetterView lv = new LetterView
                        {
                            Letter = letter,
                            ScaleX = 0
                        };
                        letterCollection.Add(lv);
                        stack.Children.Add(lv, col, row);
                        col++;
                        if (col >= letterInARow)
                        {
                            col = 0;
                            row++;
                        }
                    }

                    TermContainer.Children.Add(stack);

                    //Add a space
                    if (words.Count() > i + 1)
                    {
                        LetterView ws = new LetterView
                        {
                            IsWhiteSpace = true
                        };
                        TermContainer.Children.Add(ws);
                    }
                }

            }

            foreach (var letterView in letterCollection)
            {
                await letterView.ScaleXTo(1, 200);
            }
        }

        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(nameof(Term)))
            {
                await InitUi();

            }
            else if (propertyName.Equals(nameof(DiscoveredLetters)))
            {
                foreach (var letter in letterCollection)
                {
                    var letters = DiscoveredLetters.Split(';');
                    letter.IsShown = letters.Any(x => x.ToLower().Equals(letter.Letter.ToString().ToLower()));
                }
            }


        }       
    }
}