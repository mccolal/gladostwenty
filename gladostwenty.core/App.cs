using gladostwenty.core.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

namespace gladostwenty.core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<IAzureAuthenticationService>(new AzureAuthenticationService());

            RegisterAppStart<ViewModels.MapViewModel>();
        }
    }
}
