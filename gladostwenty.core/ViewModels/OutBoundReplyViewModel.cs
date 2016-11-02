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
    public class OutBoundReplyViewModel : MvxViewModel
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
                SetProperty(ref statusList, value);
            }
        }

        public ICommand ReplyToRequest { get; set; }

        public OutBoundReplyViewModel()
        {

            Initialize();

            ReplyToRequest = new MvxCommand<StatusListItem>((s) =>
            {
                ShowViewModel<OnRequestViewModel>(
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

            foreach (Status status in Statuses)
            {
                imgURL ImageUrl = new imgURL();
                ImageUrl.imgurl = "";
                if (status.Request)
                {
                    if (status.Seen)
                    {
                        ImageUrl.imgurl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Small-dark-grey-circle.svg/2000px-Small-dark-grey-circle.svg.png";
                    }
                    else
                    {
                        ImageUrl.imgurl = "http://bluedot.ca/wp-content/themes/dsf-blue-dot-campaign-theme/src/images/marker-circle.png";
                    }
                    stats.Add(new StatusListItem { Status = status, Contact = await Mvx.Resolve<IAzureDataService>().GetUser(status.FromId), imgUrl = ImageUrl });
                }
            }
            StatusList = new ObservableCollection<StatusListItem>(stats);
            Loading = false;
        }
    }

}