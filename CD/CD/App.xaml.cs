using Xamarin.Forms;
using CD.Helper;
using Autofac;
using CD.Views.Login;
using CD.Models;
using Newtonsoft.Json;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Essentials;
using Plugin.Connectivity;

namespace CD
{
    public partial class App : Xamarin.Forms.Application
    {
        public IContainer Container { get; }
        public string AuthToken { get; set; }
        static public string UserUID { get; set; }
        static public CD_Configuration conf;

        public App(Module module)
        {
            LoadJson();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(conf.syncfusion);
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();
            Container = BuildContainer(module);
            checkUserLogIn();
        }

        protected override void OnStart()
        {
            checkUserLogIn();
        }

        protected override void OnSleep()
        {
            //checkUserLogIn();
        }

        protected override void OnResume()
        {
            //checkUserLogIn();
        }
        IContainer BuildContainer(Module module)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NavigationService>().AsSelf().SingleInstance();
            builder.RegisterModule(module);
            return builder.Build();
        }
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
        void checkUserLogIn()
        {
            App.UserUID = App.Current.Properties.ContainsKey("App.UserUID") ? App.Current.Properties["App.UserUID"] as string : "";

            if (string.IsNullOrEmpty(App.UserUID) || string.IsNullOrWhiteSpace(App.UserUID))
            {
                MainPage = new NavigationPage(new LogIn());
                App.Current.Properties.Remove("App.UserUID");
                App.Current.SavePropertiesAsync();
                App.UserUID = "";
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }     
    }
}
