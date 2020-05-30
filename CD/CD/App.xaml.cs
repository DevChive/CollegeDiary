using Xamarin.Forms;
using CD.ViewModel.Auth;
using CD.Helper;
using Autofac;
using CD.Views;
using com.sun.org.apache.xml.@internal.security.signature;
using System;
using CD.ViewModel;

namespace CD
{
    public partial class App : Application
    {
        public IContainer Container { get; }
        public string AuthToken { get; set; }
        static public string UserUID { get; set; } = "";

        public App(Module module)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjY1MDQyQDMxMzgyZTMxMmUzMEhmRHZYL1VQM0pFQjQvOHRuRmtaYTRtYkFCa3NHYmR2Qk9uNWtqQ0lSUHM9");
            InitializeComponent();

            //#if DEBUG
            //            var @event = new Models.Calendar.EventModel()
            //            {
            //                Name = "Test",
            //                Description = "Foo bar is a good thing!",
            //                EventDate = DateTime.Now,
            //                EventEndDate = DateTime.Now.AddHours(5)
            //            };

            //            ZZZ.Instance.Events.Add(@event);
            //#endif

            Container = BuildContainer(module);
            if (string.IsNullOrEmpty(App.UserUID))
            {
                MainPage = new NavigationPage(new LogIn());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        IContainer BuildContainer(Module module)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<HomeViewModel>().AsSelf();
            builder.RegisterType<NavigationService>().AsSelf().SingleInstance();
            builder.RegisterModule(module);
            return builder.Build();
        }
    }
}
