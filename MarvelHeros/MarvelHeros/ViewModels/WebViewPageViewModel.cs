using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarvelHeros.ViewModels
{
	public class WebViewPageViewModel : ViewModelBase
	{
        private string _url;

        public string Url
        {
            get { return _url; }
            set {SetProperty(ref _url, value); }
        }


        public WebViewPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : 
            base(navigationService, pageDialogService)
        {

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Url = parameters.GetValue<string>("url");
        }
    }
}
