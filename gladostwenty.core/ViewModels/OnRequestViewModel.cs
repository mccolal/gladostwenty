using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using gladostwenty.core.Models;
using gladostwenty.core.Services;
using MvvmCross.Platform;
using System.Windows.Input;

namespace gladostwenty.core.ViewModels
{
    public class OnRequestViewModel : MvxViewModel
    {

        private GeoLocation myLocation;
        public GeoLocation MyLocation
        {
            get { return myLocation; }
            set { myLocation = value; }
        }

        public void OnMyLocationChanged(double Lati, double Longi)
        {
            string _lat = Lati.ToString();
            string _long = Longi.ToString();
            Lat = _lat;
            Long = _long;
        }

        public class StatusInfo
        {

            public string id { get; set; }

            public string ToId { get; set; }

            public string FromId { get; set; }

            public string Message { get; set; }

            public bool Seen { get; set; }

            public string Lat { get; set; }

            public string Long { get; set; }

            public bool Request { get; set; }

            public string Name { get; set; }
        }

        private StatusInfo info { get; set; }

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


        public void Init(StatusInfo sender)
        {
            Info = sender;
            Name = Info.Name.ToString();
        }

        private string _lat = "";
        public string Lat
        {
            get { return _lat; }
            set
            {
                if (value != null && value != _lat)
                {
                    _lat = value;
                    RaisePropertyChanged(() => Lat);
                }
            }
        }

        private string _long = "";
        public string Long
        {
            get { return _long; }
            set
            {
                if (value != null && value != _long)
                {
                    _long = value;
                    RaisePropertyChanged(() => Long);
                }
            }
        }

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value != _name)
                {
                    _name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }


        private string _message = "";
        public string Message
        {
            get { return _message; }
            set
            {
                if (value != _message)
                {
                    _message = value;
                    RaisePropertyChanged(() => Message);
                }
                else if (value == null)
                {
                    _message = " ";
                }
            }
        }

        public ICommand SendReply { get; set; }


        public OnRequestViewModel()
        {
            SendReply = new MvxCommand(() => {
                AttemptStatusSend();
            });
        }

        private bool _sendLocation = true;

        public bool SendLocation
        {
            get { return _sendLocation; }
            set
            {
                _sendLocation = value;
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy); }
        }

        private async void AttemptStatusSend() {
            try {
                if (!SendLocation)
                {
                    Lat = null;
                    Long = null;
                }
                IsBusy = true;
                //await Task.Delay(1000);
                await Mvx.Resolve<IAzureDataService>().SendStatus(Info.FromId, CurrentUser.id, Message, false, Lat, Long);
                TabViewModel.TabHolder.Notifications.Initialize();
            } catch (Exception e) {
            }
            ShowViewModel<TabViewModel>();
        }
    }

}
