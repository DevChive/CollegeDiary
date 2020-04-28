using Rg.Plugins.Popup.Pages;
using CD.Models.Calendar;
using CD.ViewModel.Calendar;
using System;
using Xamarin.Forms.Xaml;

namespace CD.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPickerPopup : PopupPage
    {
        private readonly Action<CalendarPickerResult> _onClosedPopup;

        public CalendarPickerPopup(Action<CalendarPickerResult> onClosedPopup)
        {
            _onClosedPopup = onClosedPopup;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is CalendarPickerPopupViewModel vm)
                vm.Closed += _onClosedPopup;
        }

        protected override void OnDisappearing()
        {
            if (BindingContext is CalendarPickerPopupViewModel vm)
                vm.Closed -= _onClosedPopup;

            base.OnDisappearing();
        }
    }
}