using System;
using System.Linq;
using System.Threading.Tasks;
using CD.Models;
using CD.Helper;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace CD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFinalExamToSubject
    {
        private Subject _subject;
        readonly FireBaseHelperMark fireBaseHelper = new FireBaseHelperMark();
        public AddFinalExamToSubject(Subject subject)
        {
            _subject = subject;
            InitializeComponent();
        }
        public async Task<bool> Check_FinalExam_Weight(Subject subject)
        {

            var marks_belonging_to_subject = await fireBaseHelper.GetMarksForSubject(subject.SubjectID);
            foreach (Mark m in marks_belonging_to_subject)
            {
                if (m.Category.Equals("Final Exam"))
                {
                    return false;
                }
            }
            return true;
        }
        private async void Save_Exam(object sender, EventArgs e)
        {
            bool validate = true;
            if (string.IsNullOrEmpty(this.result.Text)) { validate = false; }

            if(validate) { validate = await Check_FinalExam_Weight(_subject); }
            

            if (validate)
            {
                decimal result = Decimal.Parse(this.result.Text);
                await DisplayAlert("Success", "Your final exam result had been recorded", "OK");
                await fireBaseHelper.AddMark(_subject.SubjectID, "Final Exam", result, _subject.FinalExam, "Final Exam");
                await Navigation.PushAsync(new SubjectSelected(_subject), false);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                await PopupNavigation.RemovePageAsync(this);
            }
            else
            {
                await DisplayAlert("Result not added", "A final exam result already recorded", "OK");
            }
        }

        private void Cancel_Exam(object sender, EventArgs e)
        {
            PopupNavigation.RemovePageAsync(this);
        }
    }
}