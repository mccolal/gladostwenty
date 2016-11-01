using gladostwenty.core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gladostwenty.core.ViewModels {
    public class RequestStatusViewModel : MvxViewModel {

        public class StatusInfo
        {

            public string id { get; set; }

            public string ToId { get; set; }

            public string FromId { get; set; }

            public string Message { get; set; }

            public bool Seen { get; set; }

            public string Lat { get; set; }

            public string Long { get; set; }

            public string Location { get; set; }

            public bool Request { get; set; }

            public string Name { get; set; }
        }

        private StatusInfo info { get; set;}

        public StatusInfo Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                RaisePropertyChanged(() => Info);
            }
        }

        public void setLocation(string location)
        {
            Info.Location = location;
            RaisePropertyChanged(() => Info);
        }
        public void Init(StatusInfo statusInfo)
        {
            Info = statusInfo;

            Mvx.Resolve<IAzureDataService>().UpdateSeen(Info.id, "true");
        }

        public ICommand GoToMap { get; set; }
        public RequestStatusViewModel()
        {
            GoToMap = new MvxCommand(() => {     
                ShowViewModel<MapViewModel>();             
            });
        }
    }
}
