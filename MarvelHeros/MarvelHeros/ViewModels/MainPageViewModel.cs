using MarvelHeros.Models;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using Xamarin.Forms;
using Prism.Services;

namespace MarvelHeros.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<Result> _herosList;

        public ObservableCollection<Result> HerosList
        {
            get { return _herosList; }
            set { SetProperty(ref _herosList, value); }
        }

        private Result _selectedHero;

        public Result SelectedHero
        {
            get { return _selectedHero; }
            set
            {
                SetProperty(ref _selectedHero, value);
                if (_selectedHero != null)
                {
                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("hero", _selectedHero);

                    NavigationService.NavigateAsync("HeroPage", navigationParameters);
                }
            }
        }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private string _filterText;

        public string FilterText
        {
            get { return _filterText; }
            set { SetProperty(ref _filterText, value); }
        }

        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand FilterCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            HerosList = new ObservableCollection<Result>();

            FilterText = "A";
            GetCharacters("A");

            FilterCommand = new DelegateCommand(async () =>
            {
                string[] filtros = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                var filter = await PageDialogService.DisplayActionSheetAsync("Select a letter", "Cancel", "", filtros);
                if (!filter.Equals("Cancel"))
                {
                    GetCharacters(filter);
                    FilterText = filter;
                }             

            });

            SearchCommand = new DelegateCommand(() =>
            {
                GetCharacters(SearchText);
                FilterText = SearchText;
            });

        }

        private async void GetCharacters(string nameStarts)
        {   
            var progress = UserDialogs.Instance.Loading("Loading ...", null, null, true, MaskType.Clear);

            try
            {

                const string token = "178d19e32d8201c1fcdb398d720d3474";

                var httpClient = new HttpClient();
                var url = $"https://gateway.marvel.com:443/v1/public/characters?ts=1&nameStartsWith={nameStarts}&apikey={token}&hash=73f8ccaa220af748c9d0a847bc179cda&limit=100";
                                
                var result = await httpClient.GetAsync(url);
                var content = result.Content.ReadAsStringAsync().Result;

                var marvel = JsonConvert.DeserializeObject<Marvel>(content);

                HerosList.Clear();
                SearchText = "";
                foreach (var item in marvel.Data.Results)
                {
                    if (item.Description.Equals(""))
                        item.Description = "Character without description!";

                    if (!item.Thumbnail.Path.Contains("image_not_available"))
                    {
                        item.Thumbnail.Path = $"{item.Thumbnail.Path}.{item.Thumbnail.Extension}";
                    }
                    else
                    {
                        item.Thumbnail.Path = "noimage.png";
                    }

                    HerosList.Add(item);
                }

            }
            catch (Exception)
            {
                await PageDialogService.DisplayAlertAsync("Opps!", "Network Error!", "Ok");
            }
            finally
            {
                progress.Dispose();

            }
        }
    }
}
