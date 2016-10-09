using gladostwenty.core.Models;
using gladostwenty.core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace gladostwenty.core.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {

        private bool loading = true;

        public bool Loading {
            get {
                return loading;
            }
            set {
                loading = value;
                RaisePropertyChanged(() => Loading);
            }
        }

        private ObservableCollection<User> users;

        public ObservableCollection<User> Users {
            get {
                return users;
            }
            set {
                SetProperty(ref users, value);
            }
        }

        private string _hello = "Hello MvvmCross";
        public string Hello
        { 
            get { return _hello; }
            set { SetProperty (ref _hello, value); }
        }

        public ICommand ContactSelectCommand { get; private set; }

        public FirstViewModel() {

            var dataService = Mvx.Resolve<IAzureDataService>();
            dataService.Initialize();
            getTable();
            ContactSelectCommand = new MvxCommand<User>((user) => {
                ShowViewModel<RequestStatusViewModel>(user.id);
            });
        }

        private async void getTable() {
            var dataService = Mvx.Resolve<IAzureDataService>();
            var c = await dataService.GetUserTable();
            c = c.Where(x => x.id != CurrentUser.id).ToList<User>();
            Users = new ObservableCollection<User>(c);
            Loading = false;
        }
    }
}
