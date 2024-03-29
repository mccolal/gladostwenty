using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gladostwenty.droid;
using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;
using Android.Widget;
using gladostwenty.droid.Views;
using Android.Locations;
using System.Threading.Tasks;
using Android.Util;
using gladostwenty.core.ViewModels;
using MvvmCross.Binding.BindingContext;

namespace gladostwenty.Droid.Views
{
    [Activity(Label = "Status Request", ParentActivity = typeof(NotificationListView))]
    public class OnRequestView : MvxActivity, ILocationListener
    {

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }

        static readonly string TAG = "X:" + typeof(RequestStatusView).Name;
        Location _currentLocation;
        LocationManager _locationManager;
        OnRequestViewModel vm;
        BindableProgress _bindableProgress;

        string _locationProvider;
        TextView _locationText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "Main" layout resource
            SetContentView(Resource.Layout.OnRequestView);
            vm = ViewModel as OnRequestViewModel;

            Spinner SpinNumBusy = FindViewById<Spinner>(Resource.Id.snrNumberBusy);
            Spinner SpinReason = FindViewById<Spinner>(Resource.Id.snrBusy);

            SpinNumBusy.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapterNumBusy = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.number_array, Android.Resource.Layout.SimpleSpinnerItem);
            SpinReason.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapterReason = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.busy_reason_list, Android.Resource.Layout.SimpleSpinnerItem);

            adapterNumBusy.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            SpinNumBusy.Adapter = adapterNumBusy;
            adapterReason.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            SpinReason.Adapter = adapterReason;

            // Location stuff
            InitialiseLocationManager();

            _bindableProgress = new BindableProgress(this);

            var set = this.CreateBindingSet<OnRequestView, OnRequestViewModel>();
            set.Bind(_bindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();

            // Create your application here
        }

        public void OnLocationChanged(Location location)
        {
            _currentLocation = location;
        }

        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }


        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        void InitialiseLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + _locationProvider + ".");
            _currentLocation = _locationManager.GetLastKnownLocation(LocationManager.GpsProvider);
            try
            {
                vm.OnMyLocationChanged(_currentLocation.Latitude, _currentLocation.Longitude);
            }
            catch (Exception e)
            {
                Log.Debug(TAG, e.ToString());
            }
            
        }



        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            
            Spinner spinner = (Spinner)sender;
            TextView txtView;
            
            if (spinner.Id == Resource.Id.snrNumberBusy)
            {
                 txtView = FindViewById<TextView>(Resource.Id.lblAvailableInPicker);
            } else
            {
                txtView = FindViewById<TextView>(Resource.Id.lblReasonPicker);
            }
            txtView.Text = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            //string toast = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

    }
}