using System;
using System.Collections.Generic;
using System.Linq;
using FlashSample.XForms.iOS.Services;
using FlashSample.XForms.Services;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace FlashSample.XForms.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            DependencyService.Register<ILocalHostUrlService, LocalHostUrlService>();

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
