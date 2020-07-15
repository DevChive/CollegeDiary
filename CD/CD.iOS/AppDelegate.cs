using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Syncfusion.XForms.PopupLayout;
using Syncfusion.SfSchedule.XForms.iOS;
using UIKit;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.ProgressBar;
using Syncfusion.XForms.iOS.Buttons;
using Xamarin;
using Syncfusion.SfBusyIndicator.XForms.iOS;

namespace CD.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            SfScheduleRenderer.Init();
            SfAvatarViewRenderer.Init();
            SfCircularProgressBarRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();
            SfSegmentedControlRenderer.Init();
            IQKeyboardManager.SharedManager.Enable = true;
            new SfBusyIndicatorRenderer();
            LoadApplication(new App(new IOSModule()));

            return base.FinishedLaunching(app, options);
        }
    }
}
