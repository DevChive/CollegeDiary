﻿using CD.Helper;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using CD.Models;
using Xamarin.Forms.Xaml;
using System;

namespace CD.Views.SelectedSubjectView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteMark 
    {
        Mark _mark;
        Subject _subject;
        readonly FireBaseHelperMark fireBaseHelperMark = new FireBaseHelperMark();
        public DeleteMark(Mark thisMark, Subject thisSubject)
        {
            InitializeComponent();
            _mark = thisMark;
            _subject = thisSubject;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundColor = System.Drawing.Color.FromArgb(200, 0, 0, 0);
            MarkName.Text = _mark.MarkName;
            MarkWeight.Text = _mark.Weight + "%";
            MarkResult.Text = _mark.Result + "%";
        }

        private async void delete_mark(object sender, System.EventArgs e)
        {
            try
            {
                await fireBaseHelperMark.DeleteMark(_mark.MarkID);
                await Navigation.PushAsync(new SubjectSelected(_subject), false);
                DependencyService.Get<IToastMessage>().Show(_mark.Category + " was deleted");
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                await PopupNavigation.RemovePageAsync(this);
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Please try again", "Ok");
            }
        }

        [Obsolete]
        private async void cancel(object sender, System.EventArgs e)
        {
            await PopupNavigation.RemovePageAsync(this);
        }
    }
}