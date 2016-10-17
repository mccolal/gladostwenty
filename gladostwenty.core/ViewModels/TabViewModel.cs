using gladostwenty.core.Interfaces;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gladostwenty.core.ViewModels {
    public class TabViewModel : MvxViewModel{

        private IGeoCoder geocoder;
        public TabViewModel() {
            Contacts = new FirstViewModel();
            Map = new MapViewModel(geocoder);
            Notifications = new NotificationListViewModel();
        }
        private FirstViewModel contacts;
        public FirstViewModel Contacts {
            get { return contacts; }
            set { contacts = value; RaisePropertyChanged(() => Contacts); }
        }

        private MapViewModel map;
        public MapViewModel Map {
            get { return map; }
            set { map = value; RaisePropertyChanged(() => Map); }
        }

        private NotificationListViewModel notifications;
        public NotificationListViewModel Notifications {
            get { return notifications; }
            set { notifications = value; RaisePropertyChanged(() => Notifications); }
        }
    }
}
