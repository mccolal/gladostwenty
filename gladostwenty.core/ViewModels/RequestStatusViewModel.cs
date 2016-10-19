using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Init(StatusInfo statusInfo) {
            Info = statusInfo;

        }
    }
}
