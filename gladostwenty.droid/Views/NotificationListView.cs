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
            //listView = FindViewById<ListView>(Resource.Id.lstvwNotifications); // get reference to the ListView in the layout
            //                                                                   // populate the listview with data
            //tableItems = new List<TableItem>();
            //tableItems.Add(new TableItem { ContactName = "test", test1 = "test1", test2 = "test2" });
            //listView.Adapter = new HomeScreenAdapter(this, tableItems);
            ////listView.ItemClick += OnListItemClick;  // to be defined
            SetContentView(Resource.Layout.NotificationListView);
        }
    }
}