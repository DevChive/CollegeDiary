using System;
using CD.Views.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Models;
using CD.Helper;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using CD.Models.Calendar;
using System.Collections.Generic;
using CD.ViewModel;
using sun.util.resources.cldr.ewo;
using System.Linq;
using Syncfusion.SfSchedule.XForms;
using CD.Views.Calendar;
using com.sun.jdi.@event;

namespace CD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccount : ContentPage
    {
        string userID = "";
        readonly FireBaseHelperStudent fireBaseHelperStudent = new FireBaseHelperStudent();
        readonly FireBaseHelperCalendarEvents fireBaseHelperCalendar = new FireBaseHelperCalendarEvents();
        
        IFirebaseDeleteAccount authDeleteAccount;
        IFirebaseSignOut authSignOut;
        protected override bool OnBackButtonPressed() => false;
        public MyAccount()
        {
            InitializeComponent();
            userID = App.UserUID;
            authDeleteAccount = DependencyService.Get<IFirebaseDeleteAccount>();
            authSignOut = DependencyService.Get<IFirebaseSignOut>();
            RunHourlyTasks();
        }
        protected override async void OnAppearing()
        {       
            await fireBaseHelperStudent.AddGPA(userID);

            Student user = await fireBaseHelperStudent.GetStudent(userID);
            List<EventModel> allEvents = await fireBaseHelperCalendar.GetAllEvents();
            List<EventModel> listEvents = next7DaysEvents(allEvents);

            // creating a new model from 2 classes - student and calendar
            StudentCalendar studentCalendar = new StudentCalendar(user, listEvents);
            this.BindingContext = studentCalendar;

            // progress bar
            totalGPA.Progress = Convert.ToDouble(user.FinalGPA);
        }
        private async void load_subject_list(object sender, EventArgs e)
        {
            your_subjects.IsEnabled = false;
            await Navigation.PushAsync(new ListViewSubjects());
            your_subjects.IsEnabled = true;
        }

        private async void load_add_subject(object sender, EventArgs e)
        {
            add_subject.IsEnabled = false;
            await Navigation.PushAsync(new AddSubject());
            add_subject.IsEnabled = true;
        }
        private void GPA_Changed(object sender, Syncfusion.XForms.ProgressBar.ProgressValueEventArgs e)
        {
            if (e.Progress < 40)
            {
                totalGPA.ProgressColor = Color.Red;
            }
            else if (e.Progress >= 40 && e.Progress < 70)
            {
                totalGPA.ProgressColor = Color.Orange;
            }
            else if (e.Progress > 70)
            {
                totalGPA.ProgressColor = Color.Green;
            }
        }

        [Obsolete]
        private async void edit_account(object sender, EventArgs e)
        {
            Student student = await fireBaseHelperStudent.GetStudent(userID);
            await PopupNavigation.PushAsync(new MyAccountUpdate(student));
        }

        private async void delete_account(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Are you sure you want to delete your account?",
                "If you delete your account all your information will be permanently deleted.", "Yes", "No");
            if (result) // if it's equal to Yes
            {
                try
                {
                    await fireBaseHelperStudent.DeleteStudent(App.UserUID);
                    await authDeleteAccount.DeleteAccount();
                    App.UserUID = "";
                    App.Current.MainPage = new NavigationPage(new LogIn());
                    await DisplayAlert("Account deleted", "To use the application again please sign up", "ok");
                    OnBackButtonPressed();
                }
                catch (Exception)
                {
                    await DisplayAlert("Failed", "Please try again later", "ok");
                }
            }
        }

        private async void sign_out(object sender, EventArgs e)
        {
            App.UserUID = "";
            App.Current.Properties.Clear();
            await authSignOut.SignOut();
            App.Current.MainPage = new NavigationPage(new LogIn());
            // back button disabled
            OnBackButtonPressed();
        }

        private void help(object sender, EventArgs e)
        {
            if (helpMyAccount.IsVisible)
            {
                helpMyAccount.IsVisible = false;
            }
            else 
            {
                helpMyAccount.IsVisible = true;
            }
        }

        public static async void RunHourlyTasks(params Action[] tasks)
        {
            DateTime runHour = DateTime.Now.AddHours(1.0);
            TimeSpan ts = new TimeSpan(runHour.Hour, 0, 0);
            runHour = runHour.Date + ts;


            Console.WriteLine("next run will be at: {0} and current hour is: {1}", runHour, DateTime.Now);
            while (true)
            {
                TimeSpan duration = runHour.Subtract(DateTime.Now);
                if (duration.TotalMilliseconds <= 0.0)
                {
                    Parallel.Invoke(tasks);
                    Console.WriteLine("It is the run time as shown before to be: {0} confirmed with system time, that is: {1}", runHour, DateTime.Now);
                    runHour = DateTime.Now.AddHours(1.0);
                    Console.WriteLine("next run will be at: {0} and current hour is: {1}", runHour, DateTime.Now);
                    await App.Current.MainPage.DisplayAlert("ONE HOUR!", DateTime.Now.ToLongDateString(), "ok");
                    continue;
                }
                int delay = (int)(duration.TotalMilliseconds / 2);
                await Task.Delay(30000);  // 30 seconds
            }
        }
        private List<EventModel> next7DaysEvents(List<EventModel> listEvents)
        {
            List<EventModel> next7Days = new List<EventModel>();
            DateTime today = DateTime.Today;
            DateTime timeWeek = today.AddDays(8);
            if (listEvents.Count > 0)
            {
                Calendar_View_Text.IsVisible = false;
                foreach (EventModel e in listEvents)
                {
                    DateTime startDate = Convert.ToDateTime(e.StartEventDate.ToString());
                    int laterThanToday = DateTime.Compare(startDate, today);
                    int inThisWeek = DateTime.Compare(startDate, timeWeek);
                    if ((laterThanToday >= 0) && inThisWeek <= 0)
                    {
                        next7Days.Add(e);
                    }
                }
            }
            // order the list by dates
            next7Days = next7Days.OrderBy(x => x.StartEventDate).ToList();
            if (next7Days.Count == 0)
            {
                Calendar_View_Text.IsVisible = true;
            }
            else 
            {
                Calendar_View_Text.IsVisible = false;
            }
            return next7Days;
        }

        [Obsolete]
        private async void view_event(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            LstEvents.IsEnabled = false;
            if (e != null)
            {
                ScheduleAppointment appointment = new ScheduleAppointment();
                appointment.Subject = (e.ItemData as EventModel).Name;
                appointment.Notes = (e.ItemData as EventModel).Description;
                appointment.StartTime = Convert.ToDateTime((e.ItemData as EventModel).StartEventDate);
                appointment.EndTime = Convert.ToDateTime((e.ItemData as EventModel).EndEventDate);
                appointment.Color = (e.ItemData as EventModel).Color;

                await PopupNavigation.PushAsync(new EventSelected(appointment, "MyAccount"));
            }
            LstEvents.IsEnabled = true;          
        }
    }
}