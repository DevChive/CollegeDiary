using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CD.Models;
using CD.Pages;
using CD.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CD.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class SubjectSelected : ContentPage
    {
        public Subject _subject;

        public SubjectSelected(Subject subject)
        {
            _subject = subject;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = _subject;
            statusCA.ProgressColor = Color.Green; // depending on student's progree
            await statusCA.ProgressTo(0.2, 500, Easing.Linear);
            await statusFinalExam.ProgressTo(0.7, 500, Easing.Linear);

            // add delete button and update button
        }
    }

}