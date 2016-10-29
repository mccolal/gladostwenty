using gladostwenty.core.Models;
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
    public class ContactDetailViewModel : MvxViewModel{

        public class NavParameters {
            public string Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public string Phone { get; set; }

        }

        private NavParameters contact { get; set; }

        public NavParameters Contact {
            get {
                return contact;
            }
            set {
                contact = value;
                RaisePropertyChanged(() => Contact);
            }
        }

        public ICommand SendRequestCommand { get; set; }

        private Status status { get; set; }

        public Status Status { get {
                return status;
            }
            set {
                status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public void Init(NavParameters contact) {
            Contact = contact;
            LoadLatestStatus();
        }
        private bool _awaitingRequest;
        public bool AwaitingRequest
        {
            get { return _awaitingRequest; }
            set { _awaitingRequest = value; RaisePropertyChanged(() => AwaitingRequest); }
        }


        public ContactDetailViewModel() {
            SendRequestCommand = new MvxCommand(async () =>
            {
                AwaitingRequest = true;
                await Mvx.Resolve<IAzureDataService>().SendStatus(Contact.Id, CurrentUser.id, "Test", true, null, null);
                AwaitingRequest = false;
            });
            
        }

        private async void LoadLatestStatus() {
            Status status = await Mvx.Resolve<IAzureDataService>().GetUserStatus(Contact.Id);

            if(status == null) {
                Status = new Status { FromId = Contact.Id, ToId = CurrentUser.id, Message = "No current status" };
            } else {
                Status = status;
            }
        }
    }
}
