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

    [Activity(Label ="Contact Details", ParentActivity = typeof(FirstView))]
    public class ContactDetailView : MvxActivity {

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ContactDetailView);
        }
    }
}