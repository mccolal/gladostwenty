using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace gladostwenty.droid
{
    [Activity(
        Label = "Glados"
        , MainLauncher = true
        , Theme = "@style/Theme.Splash"
        , Icon ="@drawable/Icon"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
