using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using MvvmCross.Droid.Views;
using gladostwenty.core.ViewModels;
using Android.Gms.Maps.Model;
using gladostwenty.core.Models;
using System.Collections.ObjectModel;
using static gladostwenty.core.ViewModels.NotificationListViewModel;
using gladostwenty.core.Services;
using MvvmCross.Platform;
using static gladostwenty.core.ViewModels.StatusMapViewModel;

namespace gladostwenty.droid.Views
{

    [Activity(Label = "Status Location", ParentActivity = typeof(OutBoundReplyView))]
    public class StatusMapView : MvxActivity, IOnMapReadyCallback
    {
        private delegate IOnMapReadyCallback OnMapReadyCallback();
        private GoogleMap map;
        StatusMapViewModel vm;
        private Dictionary<string, StatusListItem> markers = new Dictionary<string, StatusListItem>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MapView);
            vm = ViewModel as StatusMapViewModel;
            var mapFragment = FragmentManager.FindFragmentById(Resource.Id.map) as MapFragment;
            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            GeoLocation location = new GeoLocation(double.Parse(vm.Location.Lat), double.Parse(vm.Location.Long));
            vm.OnMapSetup(MoveToLocation);
            map = googleMap;
            map.MyLocationEnabled = true;
            map.MyLocationChange += Map_MyLocationChange;
            MoveToLocation(location);
            AddPin(vm.Location);
            //map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(map.Location.Latitude, e.Location.Longitude), 16));

        }

        private void Map_MyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            map.MyLocationChange -= Map_MyLocationChange;
            var location = new GeoLocation(e.Location.Latitude, e.Location.Longitude, e.Location.Altitude);
            vm.OnMyLocationChanged(location);

        }

        private void MoveToLocation(GeoLocation geoLocation, float zoom = 16)
        {
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(new LatLng(geoLocation.Latitude, geoLocation.Longitude));
            builder.Zoom(zoom);
            var cameraPosition = builder.Build();
            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            map.MoveCamera(cameraUpdate);
        }

        private Marker AddPin(StatusLocation status)
        {
            if (null != status.Lat || null != status.Lat
                && status.Lat != string.Empty && status.Long != string.Empty)
            {
                var markerOptions = new MarkerOptions();
                markerOptions.SetPosition(new LatLng(double.Parse(status.Lat), double.Parse(status.Long)));
                markerOptions.SetTitle(string.Format(status.Name));
                markerOptions.SetSnippet(string.Format(status.Message));
                markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.TelstraMarker));
                Marker m = map.AddMarker(markerOptions);
                return m;
            }
            return null;
        }        
    }
}