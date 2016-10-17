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

namespace gladostwenty.droid.Views {
    [Activity(Label = "View for TabViewModel")]
    public class TabView : MvxTabActivity {
        protected TabViewModel TabViewModel {
            get { return base.ViewModel as TabViewModel; }
        }
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.TabView);

            TabHost.TabSpec spec;
            Intent intent;

            spec = TabHost.NewTabSpec("contacts");
            spec.SetIndicator("", GetDrawable(Resource.Drawable.ic_contacts));
            spec.SetContent(this.CreateIntentFor(TabViewModel.Contacts));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("map");
            spec.SetIndicator("", GetDrawable(Resource.Drawable.ic_person_pin));
            spec.SetContent(this.CreateIntentFor(TabViewModel.Map));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("notifications");
            spec.SetIndicator("", GetDrawable(Resource.Drawable.ic_sms));
            spec.SetContent(this.CreateIntentFor(TabViewModel.Notifications));
            TabHost.AddTab(spec);
        }
    }
}