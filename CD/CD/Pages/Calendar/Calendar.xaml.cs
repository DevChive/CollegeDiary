using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidSpecific = Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CD.Pages.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : TabbedPage
    {
        public Calendar()
        {
            InitializeComponent();
            AndroidSpecific.TabbedPage.SetToolbarPlacement(this, AndroidSpecific.ToolbarPlacement.Bottom);
            AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false);
        }
    }
}