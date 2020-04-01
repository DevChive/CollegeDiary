using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CD.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSubject : ContentPage
    {
        public AddSubject()
        {
            InitializeComponent();
        }

        private void Save_Subject(object sender, EventArgs e)
        {
            DisplayAlert("Subject Added", $"{this.subjectName.Text}\n{this.lecturerName.Text}","OK");
        }
    }
}