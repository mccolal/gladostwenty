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
using System.Windows.Input;

namespace gladostwenty.core.ViewModels
{
    public class NotificationListViewModel : MvxViewModel
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

        private ObservableCollection<Status> statuses;

        public ObservableCollection<Status> Statuses
        {
            get
            {
                return statuses;
            }
            set
            {
                SetProperty(ref statuses, value);
                UpdateStatusList();
            }
        }

        private ObservableCollection<StatusListItem> statusList;
        public ObservableCollection<StatusListItem> StatusList
        {
            get
            {
                return statusList;
            }
            set
            {
                statusList = value; RaisePropertyChanged(() => StatusList);
            
            }
        }

        public ICommand OpenInbox { get; set; }
        
        public NotificationListViewModel()
        {
            Initialize();

            OpenInbox = new MvxCommand<StatusListItem>((s) =>
            {
                ShowViewModel<RequestStatusViewModel>(
                    new OnRequestViewModel.StatusInfo
                    {
                        id = s.Status.id,
                        ToId = s.Status.ToId,
                        FromId = s.Status.FromId,
                        Message = s.Status.Message,
                        Seen = s.Status.Seen,
                        Lat = s.Status.Lat,
                        Long = s.Status.Long,
                        Request = s.Status.Request,
                        Name = s.Contact.FullName
                    });
            });
            
        }

        public async void Initialize()
        {

            var dataService = Mvx.Resolve<IAzureDataService>();

            Statuses = new ObservableCollection<Status>(await dataService.GetStatusTable());

        }

        private async void UpdateStatusList()
        {
            StatusList = new ObservableCollection<StatusListItem>();
            var stats = new List<StatusListItem>();
            imgURL ImageUrl = new imgURL();

            foreach (Status status in Statuses)
            {
                if (!status.Request)
                {
                    if (status.Seen)
                    {
                        ImageUrl.imgurl = "https://s3.amazonaws.com/kinlane-productions/bw-icons/bw-open.png";
                    } else
                    {
                        ImageUrl.imgurl = "https://cdn2.iconfinder.com/data/icons/ui-1/57/18-512.png";
                    }
                    stats.Add(new StatusListItem { Status = status, Contact = await Mvx.Resolve<IAzureDataService>().GetUser(status.FromId), imgUrl = ImageUrl });
                }
            }
            StatusList = new ObservableCollection<StatusListItem>(stats);
            Loading = false;
        }
    }

    public class StatusListItem
    {
        public User Contact { get; set; }

        public Status Status { get; set; }

        public imgURL imgUrl { get; set; }
    }

    
}
