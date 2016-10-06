using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace gladostwenty.core.ViewModels
{
    class OnRequestViewModel : MvxViewModel
    {



        private string _message = "Hello test";
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

        public ICommand ButtonCommand { get; private set; }

        public OnRequestViewModel()
        {
            ButtonCommand = new MvxCommand(() =>
            {
                //do stuff
                //String test = SpinNumBusy.GetZ
            });
        }
    }
}
