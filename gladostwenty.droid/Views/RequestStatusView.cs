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
using Android.Locations;
using gladostwenty.core.ViewModels;

namespace gladostwenty.droid.Views {
    [Activity(Label = "Status Response", ParentActivity = typeof(OutBoundReplyView))]
    public class RequestStatusView : MvxActivity {

        RequestStatusViewModel vm;
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RequestStatusView);
            geocode();
           
        }

        public async void geocode()
        {
            vm = ViewModel as RequestStatusViewModel;
            if (vm.Info.Lat != null && vm.Info.Long != null)
            {
                var geo = new Geocoder(Application.Context);
                var address = await geo.GetFromLocationAsync(double.Parse(vm.Info.Lat), double.Parse(vm.Info.Long), 1);
                vm.setLocation(string.Format("{0}, {1}", address[0].GetAddressLine(0), address[0].GetAddressLine(1)));
            }
            else
            {
                vm.setLocation("No Location Attached");
            }
        }

       

    }
}