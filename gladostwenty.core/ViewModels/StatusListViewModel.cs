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
    public class StatusListViewModel : MvxViewModel{


        private ObservableCollection<Status> statuses;

        public ObservableCollection<Status> Statuses {
            get {
                return statuses;
            }
            set {
                SetProperty(ref statuses, value);
            }
        }

        public StatusListViewModel() {
            Initialize();
        }

        private async void Initialize() {

            var dataService = Mvx.Resolve<IAzureDataService>();

            //var a = await dataService.GetStatusTable();

            //Statuses = new ObservableCollection<Status>(a);

            Statuses = new ObservableCollection<Status>(await dataService.GetStatusTable());
        }
    }
}
