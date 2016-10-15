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
            First = new FirstViewModel();
            Map = new MapViewModel(geocoder);
        }
        private FirstViewModel _first;
        public FirstViewModel First {
            get { return _first; }
            set { _first = value; RaisePropertyChanged(() => First); }
        }

        private MapViewModel _map;
        public MapViewModel Map {
            get { return _map; }
            set { _map = value; RaisePropertyChanged(() => Map); }
        }

    }
}
