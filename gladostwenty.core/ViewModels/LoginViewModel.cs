using gladostwenty.core.Models;
using MvvmCross.Core.ViewModels;
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
                    CurrentUser.Authenticated = true;
                    ShowViewModel<FirstViewModel>();
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
