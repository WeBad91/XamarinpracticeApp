using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeBad.Apps.LocalDatabase.Hangman.Repositories;
using WeBad.Apps.Models.Hangman;
using Xamarin.Forms;

namespace WeBad.Apps.ViewModels
{
    public class HangmanViewModel : BaseViewModel
    {
        private TermRepository termRepository;
        private TermCategoryModel selectedCategory;
        private string selectedCategoryName;
        private IEnumerable<TermCategoryModel> categoryModels;
        private string term;
        private string discoveredLetters;
        private string wrongLetters;
        private int noOfWrongAttemps;
        private ObservableCollection<string> selectedKeys;

        public string SelectedCategoryName
        {
            get { return selectedCategoryName; }
            set { SetProperty(ref selectedCategoryName, value); }
        }
        public string Term
        {
            get { return term; }
            set { SetProperty(ref term, value); }
        }
        public string DiscoveredLetters
        {
            get { return discoveredLetters; }
            set { SetProperty(ref discoveredLetters, value); }
        }

        public string WrongLetters
        {
            get { return wrongLetters; }
            set { SetProperty(ref wrongLetters, value); }
        }

        public int NoOfWrongAttemps
        {
            get { return noOfWrongAttemps; }
            set { SetProperty(ref noOfWrongAttemps, value); }
        }

        public ObservableCollection<string> SelectedKeys
        {
            get { return selectedKeys; }
            set { SetProperty(ref selectedKeys, value); }
        }

        public ICommand NewGameCommand { get; }
        public ICommand KeyCommand { get; }
        public ICommand SelectCategoryCommand { get; }


        public HangmanViewModel()
        {
            SelectedCategoryName = string.Empty;
            termRepository = new TermRepository();
            InitData();

            NewGameCommand = new Command(async() => await StartNewGame());

            KeyCommand = new Command((letter) =>
            {
                if (!Term.ToUpper().Contains(letter.ToString()))
                {
                    
                    NoOfWrongAttemps++;
                    WrongLetters += string.IsNullOrEmpty(WrongLetters) ? letter : $",{letter}";
                                      
                }

                DiscoveredLetters = string.Join(";", SelectedKeys);

                if (NoOfWrongAttemps >= 6 || !Term.ToUpper().Any(x => !string.IsNullOrWhiteSpace(x.ToString()) && !SelectedKeys.Contains(x.ToString())))
                {
                    OnFinishGame(NoOfWrongAttemps < 6);
                }
            });

            SelectCategoryCommand = new Command(async () =>
            {
                TermCategoryModel currentCategory = selectedCategory;
                await GetTermCategory();
                if (currentCategory.Id != selectedCategory.Id)
                {
                    await StartNewGame();
                }

            });
        }

        private void InitData()
        {
            categoryModels = termRepository.GetAllCategories();       
        }

        private async void OnFinishGame(bool isWon)
        {
            string msg;

            if (isWon)
            {
                msg = $"Čestitamo! \"{Term}\" je tačan pojam.";
            }
            else
            {
                msg = $"Game over! \"{Term}\" je bio tačan pojam.";
            }

            bool playAgain = await App.Current.MainPage.DisplayAlert("Kraj igre", msg, "Igrajte ponovo", "Otkaži");
            if (playAgain)
            {
                await StartNewGame();
            }
            else
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async ValueTask StartNewGame()
        {
            if (selectedCategory == null)
            {
                await GetTermCategory();
            }

            NoOfWrongAttemps = 0;
            Term = termRepository.GetTermByCategory(selectedCategory.Id).Value;
            SelectedKeys = new ObservableCollection<string>();
            DiscoveredLetters = string.Empty;
            NoOfWrongAttemps = 0;
            WrongLetters = string.Empty;
        }

        private async Task GetTermCategory()
        {
            var options = categoryModels.Select(x => x.Name).ToArray();
            string result = await App.Current.MainPage.DisplayActionSheet("Izaberite kategoriju", "Nasumično", null, options);

            if (result == null || result.Equals("Nasumično"))
            {
                Random random = new Random();
                int index = random.Next(0, categoryModels.Count());
                selectedCategory = categoryModels.ElementAt(index);
            }
            else
            {
                selectedCategory = categoryModels.FirstOrDefault(x => x.Name.Equals(result));
            }

            SelectedCategoryName = selectedCategory.Name;
        }

    }
}
