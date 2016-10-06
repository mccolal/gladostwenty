using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using Android.Util;
using Gcm.Client;
using MvvmCross.Droid.Views;

namespace gladostwenty.droid.Views
{
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
}
