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

    [Activity(Label = "View for LoginViewModel", Theme = "@style/LoginViewTheme"
        , WindowSoftInputMode = SoftInput.AdjustResize,NoHistory = true)]
    public class LoginView : MvxActivity{

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginView);
        }
    }
}