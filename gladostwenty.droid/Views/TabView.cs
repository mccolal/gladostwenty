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
using MvvmCross.Droid.Views;
using gladostwenty.core.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using Gcm.Client;
using Android.Util;
using Android.Gms.Maps;
using gladostwenty.core.Models;
using Android.Gms.Maps.Model;

namespace gladostwenty.droid.Views
{
    [Activity(Label = "View for TabViewModel")]
    public class TabView : MvxTabActivity
    {
        protected TabViewModel TabViewModel
        {
            get { return base.ViewModel as TabViewModel; }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.TabView);

            TabHost.TabSpec spec;
            Intent intent;

            spec = TabHost.NewTabSpec("first");
            spec.SetIndicator("Contacts");
            spec.SetContent(this.CreateIntentFor(TabViewModel.First));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("map");
            spec.SetIndicator("Map");
            spec.SetContent(this.CreateIntentFor(TabViewModel.Map));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("child3");
            spec.SetIndicator("3");
            spec.SetContent(this.CreateIntentFor(TabViewModel.Child3));
            TabHost.AddTab(spec);
        }
    }

    [Activity(Label = "View for FirstViewModel")]
    public class FirstView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.FirstView);
            RegisterWithGCM();
        }

        public static MobileServiceClient client =
           new MobileServiceClient("https://gladostelstra.azurewebsites.net");

        // Create a new instance field for this activity.
        public static FirstView instance = new FirstView();

        private void RegisterWithGCM()
        {
            // Check to ensure everything's set up right
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            // Register for push notifications
            Log.Info("MainActivity", "Registering...");
            GcmClient.Register(this, Constants.SenderID);
        }

        // Return the current activity instance.
        public static FirstView CurrentActivity
        {
            get
            {
                return instance;
            }
        }
        // Return the Mobile Services client.
        public MobileServiceClient CurrentClient
        {
            get
            {
                return client;
            }
        }
    }

    [Activity(Label = "MapView")]
    public class MapView : MvxActivity, IOnMapReadyCallback
    {
        private delegate IOnMapReadyCallback OnMapReadyCallback();
        private GoogleMap map;
        MapViewModel vm;
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
            map.MapLongClick += Map_MapClick;
        }

        private void Map_MapClick(object sender, GoogleMap.MapLongClickEventArgs e)
        {
            AddPin(new GeoLocation(e.Point.Latitude, e.Point.Longitude));
        }

        private void Map_MyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            map.MyLocationChange -= Map_MyLocationChange;
            var location = new GeoLocation(e.Location.Latitude, e.Location.Longitude, e.Location.Altitude);
            MoveToLocation(location);
            vm.OnMyLocationChanged(location);
        }

        private void MoveToLocation(GeoLocation geoLocation, float zoom = 18)
        {
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(new LatLng(geoLocation.Latitude, geoLocation.Longitude));
            builder.Zoom(zoom);
            var cameraPosition = builder.Build();
            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            map.MoveCamera(cameraUpdate);
        }

        private void AddPin(GeoLocation location)
        {
            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(location.Latitude, location.Longitude));
            markerOptions.SetSnippet(string.Format("Testing!!"));
            markerOptions.SetTitle(string.Format("Pin!!"));
            map.AddMarker(markerOptions);
        }
    }

    [Activity(Label = "View for Child3ViewModel")]
    public class Child3View : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Child3View);
        }
    }
}