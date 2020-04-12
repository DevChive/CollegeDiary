using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CD.Helper;
using CD.Pages;
using CD.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace CD.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMarkToSubject
    {
        private Subject _subject;
        readonly FireBaseHelperMarks fireBaseHelper = new FireBaseHelperMarks();
        public AddMarkToSubject(Subject subject)
        {
            _subject = subject;
            InitializeComponent();
        }

        [Obsolete]
        private async void Save_Mark(object sender, EventArgs e)
        {
            bool validate = true;
            if (string.IsNullOrEmpty(this.categoryPicker.Items[categoryPicker.SelectedIndex]) ||
                string.IsNullOrEmpty(this.mark_name.Text) || string.IsNullOrEmpty(this.weight.Text)
                || string.IsNullOrEmpty(this.result.Text))
            {
                validate = false;
            }
            if (validate)
            {
                int result = Int32.Parse(this.result.Text);
                int weight = Int32.Parse(this.weight.Text);
                var mark = await fireBaseHelper.GetMark(mark_name.Text);
                await fireBaseHelper.AddMark(_subject.SubjectID, mark_name.Text, result, weight, categoryPicker.Items[categoryPicker.SelectedIndex]);
                await DisplayAlert("Mark Added", "Something", "OK");
                await PopupNavigation.RemovePageAsync(this);
            }
        }

        [Obsolete]
        private void Cancel_Mark(object sender, EventArgs e)
        {
            PopupNavigation.RemovePageAsync(this);
        }
    }
}