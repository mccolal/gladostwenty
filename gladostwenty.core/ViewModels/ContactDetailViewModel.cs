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

        public void Init(NavParameters contact) {
            Contact = contact;
        }

        public ContactDetailViewModel() {
            SendRequestCommand = new MvxCommand(() => {
                Mvx.Resolve<IAzureDataService>().SendStatusRequest(Contact.Id, CurrentUser.id, "Test");
            });
        }
    }
}
