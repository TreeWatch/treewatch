using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ObjCRuntime;
using TreeWatch;

using UIKit;
using Xamarin;
using Xamarin.Forms;

namespace TreeWatch.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		static readonly IntPtr setAccessibilityIdentifier_Handle = Selector.GetHandle("setAccessibilityIdentifier:");
			
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			// http://forums.xamarin.com/discussion/21148/calabash-and-xamarin-forms-what-am-i-missing
			Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) => {

				// http://developer.xamarin.com/recipes/testcloud/set-accessibilityidentifier-ios/
				if (null != e.View.StyleId) {
					e.NativeView.AccessibilityIdentifier = e.View.StyleId;
						Console.WriteLine("Set AccessibilityIdentifier: " + e.View.StyleId);
				}
			};

			FormsMaps.Init ();


			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start ();
			#endif

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}