using System;
using System.Threading.Tasks;
using CD.Helper;
using CD.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Views.List;
using Rg.Plugins.Popup.Services;


namespace CD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ListViewSubjects : ContentPage
    {
        readonly FireBaseHelperSubject fireBaseHelper = new FireBaseHelperSubject();

        public ListViewSubjects()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await FetchAllSubjects();
        }
        private async Task FetchAllSubjects()
        {
            var allSubjects = await fireBaseHelper.GetAllSubjects();
            LstSubjects.ItemsSource = allSubjects;
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void LstSubjects_ItemSelected(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            Subject subject = await fireBaseHelper.GetSubject( (e.ItemData as Subject).SubjectID);
            await Navigation.PushAsync(new SubjectSelected(subject));
        }

        private void BackToTitle_Clicked(object sender, EventArgs e)
        {
            this.SearchButton.IsVisible = true;
            if (this.TitleView != null)
            {
                double opacity;

                // Animating Width of the search box, from full width to 0 before it removed from view.
                var shrinkAnimation = new Animation(property =>
                {
                    Search.WidthRequest = property;
                    opacity = property / TitleView.Width;
                    Search.Opacity = opacity;
                },
                TitleView.Width, 0, Easing.Linear);
                shrinkAnimation.Commit(Search, "Shrink", 16, 250, Easing.Linear, (p, q) => this.SearchBoxAnimationCompleted());
            }

            SearchEntry.Text = string.Empty;
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            this.Search.IsVisible = true;
            this.Title.IsVisible = false;
            this.SearchButton.IsVisible = false;

            if (this.TitleView != null)
            {
                double opacity;

                // Animating Width of the search box, from 0 to full width when it added to the view.
                var expandAnimation = new Animation(
                    property =>
                    {
                        Search.WidthRequest = property;
                        opacity = property / TitleView.Width;
                        Search.Opacity = opacity;
                    }, 0, TitleView.Width, Easing.Linear);
                expandAnimation.Commit(Search, "Expand", 16, 250, Easing.Linear, (p, q) => this.SearchExpandAnimationCompleted());
            }

        }
        private void SearchBoxAnimationCompleted()
        {
            this.Search.IsVisible = false;
            this.Title.IsVisible = true;
        }

        
        private void SearchExpandAnimationCompleted()
        {
            this.SearchEntry.Focus();
        }
    }
}
