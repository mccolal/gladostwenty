using MvvmCross.Core.ViewModels;
using gladostwenty.core.Interfaces;
using gladostwenty.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platform;
using gladostwenty.core.Services;

namespace gladostwenty.core.ViewModels
{
    public class StatusMapViewModel : MvxViewModel
    {
        private GeoLocation myLocation;
        private IGeoCoder geocoder;
        private Action<GeoLocation, float> moveToLocation;
        public GeoLocation MyLocation
        {
            get { return myLocation; }
            set { myLocation = value; }
        }

        public StatusMapViewModel(IGeoCoder geocoder)
        {
            this.geocoder = geocoder;
        }
        public void OnMyLocationChanged(GeoLocation location)
        {
            MyLocation = location;
        }

        public void OnMapSetup(Action<GeoLocation, float> MoveToLocation)
        {
            moveToLocation = MoveToLocation;
        }

        public void Init(StatusLocation statusLocation)
        {
            Location = statusLocation;
        }
        public class StatusLocation
        {

            public string Name { get; set; }

            public string Message { get; set; }

            public string Lat { get; set; }

            public string Long { get; set; }

        }
        private StatusLocation location { get; set; }

        public StatusLocation Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                RaisePropertyChanged(() => Location);
            }
        }

    }

}