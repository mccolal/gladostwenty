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
                    if (null != Username) {
                        if (Username != string.Empty && Username.ToLower().Equals("alex")) {
                            CurrentUser.id = "bdd58606-09d3-4aea-b9da-e957e2b24c0d";
                        }else if (Username != string.Empty && Username.ToLower().Equals("josh")) {
                            CurrentUser.id = "a5696044-34f5-4b0a-ad33-289fa5ad8399";
                        }else if (Username != string.Empty && Username.ToLower().Equals("jeremy")) {
                            CurrentUser.id = "BD9612BB-9935-42C6-BB16-A391FD72C042";
                        }else if (Username != string.Empty && Username.ToLower().Equals("ashleigh")) {
                            CurrentUser.id = "D8AA894E-4785-4446-B7A5-B54BF341B6D8";
                        }else if (Username != string.Empty && Username.ToLower().Equals("haiden")) {
                            CurrentUser.id = "68192CEF-E895-46C6-AFB9-FFCDF40F7D57";
                        }

                    } else {
                        CurrentUser.id = "107e1121-2bdc-47b5-9acc-df72ea6b5a20";
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
