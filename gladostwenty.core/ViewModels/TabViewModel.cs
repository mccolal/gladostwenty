using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using MvvmCross.Core.ViewModels;
using gladostwenty.core.Models;
using System.Collections.ObjectModel;
using MvvmCross.Platform;
using gladostwenty.core.Services;
using gladostwenty.core.Interfaces;

namespace gladostwenty.core.ViewModels
{
    public class TabViewModel : MvxViewModel
    {
        private IGeoCoder geocoder;
        public TabViewModel()
        {
            First = new FirstViewModel();
            Map = new MapViewModel(geocoder);
            Child3 = new Child3ViewModel();
        }
        private FirstViewModel _first;
        public FirstViewModel First
        {
            get { return _first; }
            set { _first = value; RaisePropertyChanged(() => First); }
        }

        private MapViewModel _map;
        public MapViewModel Map
        {
            get { return _map; }
            set { _map = value; RaisePropertyChanged(() => Map); }
        }

        private Child3ViewModel _child3;
        public Child3ViewModel Child3
        {
            get { return _child3; }
            set { _child3 = value; RaisePropertyChanged(() => Child3); }
        }
    }

    public class FirstViewModel
    : MvxViewModel
    {
        private bool loading = true;

        public bool Loading
        {
            get
            {
                return loading;
            }
            set
            {
                loading = value;
                RaisePropertyChanged(() => Loading);
            }
        }

        private ObservableCollection<User> users;

        public ObservableCollection<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                SetProperty(ref users, value);
            }
        }

        public ICommand ContactSelectCommand { get; private set; }

        public FirstViewModel()
        {

            InitDataStorage();
            ContactSelectCommand = new MvxCommand<User>((u) => {
                ShowViewModel<ContactDetailViewModel>(
                    new ContactDetailViewModel.NavParameters
                    {
                        Id = u.id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email
                    });
            });
        }

        private async void InitDataStorage()
        {
            var dataService = Mvx.Resolve<IAzureDataService>();
            await dataService.Initialize();
            Users = new ObservableCollection<User>(await dataService.GetUserTable());
            Loading = false;
        }
    }
    public class MapViewModel
    : MvxViewModel
    {
        private GeoLocation myLocation;
        private IGeoCoder geocoder;
        private Action<GeoLocation, float> moveToLocation;
        public GeoLocation MyLocation
        {
            get { return myLocation; }
            set { myLocation = value; }
        }

        public MapViewModel(IGeoCoder geocoder)
        {
            this.geocoder = geocoder;
        }
        public void OnMyLocationChanged(GeoLocation location)
        {
            MyLocation = location;
        }

        public void MapTapped(GeoLocation location)
        {
            MyLocation = location;

        }

        public void OnMapSetup(Action<GeoLocation, float> MoveToLocation)
        {
            moveToLocation = MoveToLocation;
        }

    }
    public class Child3ViewModel
    : MvxViewModel
    {
        private string _oink = "42";
        public string Oink
        {
            get { return _oink; }
            set { _oink = value; RaisePropertyChanged(() => Oink); }
        }

    }
    public class GrandChildViewModel
        : MvxViewModel
    {
        private string _life = "heidi";
        public string Life
        {
            get { return _life; }
            set { _life = value; RaisePropertyChanged(() => Life); }
        }
    }
}
