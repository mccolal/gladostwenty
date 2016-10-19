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
                if (value != null && value != _message)
                {
                    _message = value;
                    RaisePropertyChanged(() => Message);
                }
            }
        }

        public ICommand SendReply { get; set; }

        public OnRequestViewModel()
        {
            SendReply = new MvxCommand(() => {
                Mvx.Resolve<IAzureDataService>().SendStatus(Info.FromId, CurrentUser.id, Message, false);
                
            });
        }
    }
}
