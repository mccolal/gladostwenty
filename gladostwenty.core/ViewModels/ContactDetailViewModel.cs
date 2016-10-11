using gladostwenty.core.Models;
using gladostwenty.core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gladostwenty.core.ViewModels {
    public class ContactDetailViewModel : MvxViewModel{

        public class NavParameters {
            public string Id { get; set; }
        }

        private User contact { get; set; }


        public User Contact {
            get {
                return contact;
            }
            set {
                contact = value;
                RaisePropertyChanged(() => Contact);
            }
        }


        public void Init(NavParameters contact) {
            LoadContact(contact.Id);
        }

        private async void LoadContact(string id) {

            var dataService = Mvx.Resolve<IAzureDataService>();
            await dataService.Initialize();
            Contact = await dataService.GetUser(id);
            var a = Contact;
        }
    }
}
