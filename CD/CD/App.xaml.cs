using Xamarin.Forms;
using CD.ViewModel.Auth;
using CD.Helper;
using Autofac;

namespace CD
{
    public partial class App : Application
    {
        public IContainer Container { get; }
        public string AuthToken { get; set; }
        public App(Module module)
        {
            InitializeComponent();
            Container = BuildContainer(module);
            MainPage = new NavigationPage (new MainPage());
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
