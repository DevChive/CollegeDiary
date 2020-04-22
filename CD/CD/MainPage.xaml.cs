using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Pages;

namespace CD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void load_subject_list(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListViewSubjects());
        }

        private void load_add_subject(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddSubject());
            //TODO: make the add subject, after submission to load the home page
        }
        private void load_create_student(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddStudent());
        }
        private async void load_login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogIn());
        }
    }
}