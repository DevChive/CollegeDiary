using Xamarin.Forms;
using CD.ViewModel.Auth;
using CD.Helper;
using Autofac;
using CD.Views.Login;
using CD.Models;
using Newtonsoft.Json;
using System.IO;

namespace CD
{
    public partial class App : Application
    {
        public IContainer Container { get; }
        public string AuthToken { get; set; }
        static public string UserUID { get; set; } = "";
        static public CD_Configuration conf;

        public App(Module module)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(conf.syncfusion);
            InitializeComponent();
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
        [System.Obsolete]
        void LoadJson()
        {
            string jsonFileName = "CD.config.json";

            var gh = System.Reflection.Assembly.GetAssembly(typeof(CD_Configuration)).GetManifestResourceStream(jsonFileName);
            if (gh == null)
                return;
            using (var reader = new System.IO.StreamReader(gh))
            {
                var jsonString = reader.ReadToEnd();
                conf = JsonConvert.DeserializeObject<CD_Configuration>(jsonString);
            }
        }
    }
}
