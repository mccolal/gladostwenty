using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using gladostwenty.core.Services;
using gladostwenty.droid.Services;
using gladostwenty.core.Interfaces;
using gladostwenty.droid.Maps;

namespace gladostwenty.droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
            //Mvx.RegisterSingleton<IAzureDataService>(() => new AzureDataServiceDroid());
        }

        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterSingleton<IAzureDataService>(() => new AzureDataServiceDroid());
            return new gladostwenty.core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
        protected override void InitializeFirstChance()
        {
            Mvx.LazyConstructAndRegisterSingleton<IGeoCoder, GeoCoder>();
            base.InitializeFirstChance();
        }
    }
}
