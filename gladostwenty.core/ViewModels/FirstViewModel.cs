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

        public ICommand ContactSelectCommand { get; private set; }

        public FirstViewModel() {

            InitDataStorage();
            ContactSelectCommand = new MvxCommand<User>((u) => {
                ShowViewModel<ContactDetailViewModel>(
                    new ContactDetailViewModel.NavParameters {
                        Id = u.id, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email
                    });
            });
        }

        private async void InitDataStorage() {
            var dataService = Mvx.Resolve<IAzureDataService>();
            await dataService.Initialize();
            Users = new ObservableCollection<User>(await dataService.GetUserTable());
            Loading = false;
        }
    }
}
