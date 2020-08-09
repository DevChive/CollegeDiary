using System;
using System.Threading.Tasks;
using CD.Helper;
using CD.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Views.List;
using Rg.Plugins.Popup.Services;
using CD.Views.SelectedSubjectView;

namespace CD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ListViewSubjects : ContentPage
    {
        readonly FireBaseHelperSubject fireBaseHelper = new FireBaseHelperSubject();

        public ListViewSubjects()
        {
            InitializeComponent();
            //TODO: scale the images
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await FetchAllSubjects();
        }
        private async Task FetchAllSubjects()
        {
            try
            {
                var allSubjects = await fireBaseHelper.GetAllSubjects();
                LstSubjects.ItemsSource = allSubjects;
                if (allSubjects.Count == 0)
                {
                    Subject_text.IsVisible = true;
                    //add_Subject_Arrow.IsVisible = true;
                }
                else
                {
                    Subject_text.IsVisible = false;
                    //add_Subject_Arrow.IsVisible = false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void LstSubjects_ItemSelected(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            LstSubjects.IsEnabled = false;
            add_subject.IsEnabled = false;
            Subject subject = await fireBaseHelper.GetSubject( (e.ItemData as Subject).SubjectID);
            await Navigation.PushAsync(new SubjectSelected(subject));
            LstSubjects.IsEnabled = true;
            add_subject.IsEnabled = true;
        }

        private void BackToTitle_Clicked(object sender, EventArgs e)
        {
            this.SearchButton.IsVisible = true;
            add_subject.IsVisible = true;
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
            add_subject.IsVisible = false;
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

        private async void load_add_subject(object sender, EventArgs e)
        {
            add_subject.IsEnabled = false;
            LstSubjects.IsEnabled = false;
            await Navigation.PushAsync(new AddSubject());
            add_subject.IsEnabled = true;
            LstSubjects.IsEnabled = true;
        }

        private void BackgroundGradient_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            MyAccount.setGradientWallpaper(e);
        }
    }
}
