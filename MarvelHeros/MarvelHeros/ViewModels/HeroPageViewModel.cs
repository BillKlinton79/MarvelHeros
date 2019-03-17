using MarvelHeros.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MarvelHeros.ViewModels
{
	public class HeroPageViewModel : ViewModelBase
	{
        private Result _hero;

        public Result Hero
        {
            get { return _hero; }
            set {SetProperty(ref _hero, value); }
        }

        private bool _wikiVisibility;

        public bool WikiVisibility
        {
            get { return _wikiVisibility; }
            set { SetProperty(ref _wikiVisibility, value); }
        }

        private string _link;

        public string Link
        {
            get { return _link; }
            set { SetProperty(ref _link, value); }
        }

        public Command LinkCommand { get; set; }

        public HeroPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            :base(navigationService, pageDialogService)
        {
            WikiVisibility = false;
            Hero = new Result();

            LinkCommand = new Command(() =>
            {
                var navigationParameters = new NavigationParameters();
                navigationParameters.Add("url", Link);
                NavigationService.NavigateAsync("WebViewPage",navigationParameters,useModalNavigation: true);
            });

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {            

            if(parameters.Count > 0)
            {
                Hero = parameters.GetValue<Result>("hero");

                foreach (var item in Hero.Urls)
                {
                    if (item.Type.Equals("wiki"))
                    {
                        WikiVisibility = true;
                        Link = item.Url;
                    }
                }
            } 
        }
    }
}
