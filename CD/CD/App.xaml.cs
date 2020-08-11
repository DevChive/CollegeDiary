﻿using Xamarin.Forms;
using CD.Helper;
using Autofac;
using CD.Views.Login;
using CD.Models;
using Newtonsoft.Json;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;

namespace CD
{
    public partial class App : Xamarin.Forms.Application
    {
        public IContainer Container { get; }
        public string AuthToken { get; set; }
        static public string UserUID { get; set; }
        static public CD_Configuration conf;
        public static bool loggedInNow = false;

        public App(Module module)
        {
            checkPreviousInstall();
            LoadJson();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(conf.syncfusion);
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();
            Container = BuildContainer(module);
            checkUserLogInAsync();

        }
        protected override void OnStart()
        {
            checkUserLogInAsync();
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
        async Task checkUserLogInAsync()
        {
            App.UserUID = App.Current.Properties.ContainsKey("App.UserUID") ? App.Current.Properties["App.UserUID"] as string : "";
            if (!string.IsNullOrEmpty(App.UserUID) && !string.IsNullOrWhiteSpace(App.UserUID))
            {
                bool userExist = await userExists(UserUID);
                if (userExist)
                {
                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    notSignedIn();
                }
            }
            else
            {
                notSignedIn();
            }
        }
        void checkPreviousInstall()
        {
            if (!App.Current.Properties.ContainsKey("PreviosInstalled"))
            {
                App.Current.Properties.Clear();
                App.Current.Properties["PreviosInstalled"] = "true";
                App.Current.SavePropertiesAsync();
            }
        }
        public static async Task<bool> userExists(string StudentID)
        {
            FirebaseClient firebase = new FirebaseClient(App.conf.firebase);
            var user = (await firebase.Child(UserUID)
                .OnceAsync<Student>())
                .FirstOrDefault
                (a => a.Object.StudentID == StudentID);
            return (user != null);
        }
        public static void notSignedIn()
        {
            App.Current.MainPage = new NavigationPage(new LogIn());

            App.Current.Properties.Remove("App.UserUID");
            App.Current.SavePropertiesAsync();
            App.UserUID = "";
        }
    }
}
