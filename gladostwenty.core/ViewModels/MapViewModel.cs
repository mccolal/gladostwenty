using MvvmCross.Core.ViewModels;
using gladostwenty.core.Interfaces;
using gladostwenty.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gladostwenty.core.ViewModels
{
    public class MapViewModel : MvxViewModel
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
}