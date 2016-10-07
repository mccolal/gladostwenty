using gladostwenty.core.Models;
using gladostwenty.core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.Generic;
using System.Windows.Input;

namespace gladostwenty.core.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {

        private List<user> users { get; set; }

        private string _hello = "Hello MvvmCross";
        public string Hello
        { 
            get { return _hello; }
            set { SetProperty (ref _hello, value); }
        }

        public ICommand ButtonCommand { get; set; }

        public FirstViewModel() {

            ButtonCommand = new MvxCommand(() => {
                var dataService = Mvx.Resolve<IAzureDataService>();
                dataService.Initialize();
                getTable();
            });
        }

        private async void getTable() {
            var dataService = Mvx.Resolve<IAzureDataService>();
            var c = await dataService.GetUserTable();
            users = c;
        }
    }
}
