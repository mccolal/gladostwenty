using gladostwenty.core.Models;
using gladostwenty.core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gladostwenty.core.ViewModels {
    public class NotificationListViewModel : MvxViewModel{

        private ObservableCollection<Status> statuses;

        public ObservableCollection<Status> Statuses {
            get {
                return statuses;
            }
            set {
                SetProperty(ref statuses, value);
                UpdateStatusList();
            }
        }

        private ObservableCollection<StatusListItem> statusList;
        public ObservableCollection<StatusListItem> StatusList {
            get {
                return statusList;
            }
            set {
                SetProperty(ref statusList, value);
            }
        }


        public NotificationListViewModel() {
            Initialize();
        }

        private async void Initialize() {

            var dataService = Mvx.Resolve<IAzureDataService>();

            Statuses = new ObservableCollection<Status>(await dataService.GetStatusTable());
        }

        private async void UpdateStatusList() {
            StatusList = new ObservableCollection<StatusListItem>();
            
            foreach (Status status in Statuses) {
                StatusList.Add(new StatusListItem { Status = status, Contact = await Mvx.Resolve<IAzureDataService>().GetUser(status.FromId) });
            }
        }

        public class StatusListItem{
            public User Contact { get; set; }

            public Status Status { get; set; }
        }
    }
}
