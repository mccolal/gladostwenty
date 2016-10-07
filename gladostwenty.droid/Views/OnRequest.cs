using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gladostwenty.droid;
using Android.App;
using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;
using Android.Widget;


namespace gladostwenty.Droid.Views
{
    [Activity(Label = "OnRequest")]
    public class OnRequest : MvxActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Set our view from the "Main" layout resource
            SetContentView(Resource.Layout.OnRequest);
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
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

    }
}