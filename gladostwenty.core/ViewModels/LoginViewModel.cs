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
    public class LoginViewModel : MvxViewModel {

        private string username;
        public string Username {
            get {
                return username;
            }
            set {
                username = value;
            }
        }

        private string password;
        public string Password {
            get {
                return password;
            }
            set {
                password = value;
            }

        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel() {
            LoginCommand = new MvxCommand(() => {
                if (Login()) {
                    if (Username.ToLower().Equals("alex")) {
                        CurrentUser.id = "bdd58606-09d3-4aea-b9da-e957e2b24c0d";
                    }

                    CurrentUser.Authenticated = true;

                    //Mvx.Resolve<IAzureAuthenticationService>().Authenticate();

                    ShowViewModel<TabViewModel>();
                } else {
                    Reset();
                }
            });
        }

        private bool Login() {
            return true;
        }

        private void Reset() {
            Password = "";
        }
    }
}
