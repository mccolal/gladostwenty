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

namespace gladostwenty.droid.Views {

    [Activity(Label = "StatusListView")]
    public class NotificationListView : MvxActivity {


        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            var contactsAdapter = new ContactsAdapter(this);
            var contactsListView = FindViewById<ListView>(Resource.Id.ContactsListView);
            contactsListView.Adapter = contactsAdapter;
            SetContentView(Resource.Layout.NotificationListView);
        }
    }
}