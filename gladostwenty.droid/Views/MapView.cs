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

namespace gladostwenty.droid.Views
{

    [Activity(Label = "MapView")]
    public class MapView : MvxActivity, IOnMapReadyCallback
    {
        private delegate IOnMapReadyCallback OnMapReadyCallback();
        private GoogleMap map;
        MapViewModel vm;
        private Dictionary<string, StatusListItem> markers = new Dictionary<string, StatusListItem>(); 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MapView);
            vm = ViewModel as MapViewModel;
            var mapFragment = FragmentManager.FindFragmentById(Resource.Id.map) as MapFragment;
            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            vm.OnMapSetup(MoveToLocation);
            map = googleMap;
            map.MyLocationEnabled = true;
            map.MyLocationChange += Map_MyLocationChange;
            map.InfoWindowClick += Map_OnInfoWindowClick;
            //map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(map.Location.Latitude, e.Location.Longitude), 16));
            UpdateStatusList();
        }

        private void Map_MyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            map.MyLocationChange -= Map_MyLocationChange;
            var location = new GeoLocation(e.Location.Latitude, e.Location.Longitude, e.Location.Altitude);
            MoveToLocation(location);
            vm.OnMyLocationChanged(location);
            
        }

        private void MoveToLocation(GeoLocation geoLocation, float zoom = 12)
        {
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(new LatLng(geoLocation.Latitude, geoLocation.Longitude));
            builder.Zoom(zoom);
            var cameraPosition = builder.Build();
            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            map.MoveCamera(cameraUpdate);
        }

        private void Map_OnInfoWindowClick (object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            Marker marker = e.Marker;
            vm.OpenStatus(markers[marker.Id]);
        }

        private void AddPin(GeoLocation location)
        {
            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(location.Latitude, location.Longitude));
            markerOptions.SetSnippet(string.Format("Alex!!"));
            markerOptions.SetTitle(string.Format("Pin!!"));
            map.AddMarker(markerOptions);
        }

        private Marker AddPin(StatusListItem status)
        {
            if (null != status.Status.Lat || null != status.Status.Lat 
                && status.Status.Lat != string.Empty && status.Status.Long != string.Empty)
            {
                var markerOptions = new MarkerOptions();
                markerOptions.SetPosition(new LatLng(double.Parse(status.Status.Lat), double.Parse(status.Status.Long)));
                markerOptions.SetTitle(string.Format(status.Contact.FullName));
                markerOptions.SetSnippet(string.Format(status.Status.Message));
                markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.TelstraMarker));
                Marker m = map.AddMarker(markerOptions);
                return m;
            }
            return null;
        }

        private async void UpdateStatusList()
        {
            var dataService = Mvx.Resolve<IAzureDataService>();
            var Statuses = new ObservableCollection<Status>(await dataService.GetStatusTable());
            var StatusList = new ObservableCollection<StatusListItem>();

            if (Statuses != null)
            {
                foreach (Status status in Statuses)
                {
                    var statusItem = new StatusListItem { Status = status, Contact = await Mvx.Resolve<IAzureDataService>().GetUser(status.FromId) };
                    if (statusItem != null)
                    {
                        StatusList.Add(statusItem);
                        if (statusItem.Status.Lat != null && statusItem.Status.Long != null 
                            && statusItem.Status.Lat != string.Empty && statusItem.Status.Long != string.Empty)
                        {
                            markers.Add(AddPin(statusItem).Id, statusItem);
                        }
                    }
                }
            }
        }
    }
}