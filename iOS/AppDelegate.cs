// <copyright file="AppDelegate.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
#region Copyright
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#endregion
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", MessageId = "Ctl", Scope = "namespace", Target = "Assembly name", Justification = "Auto generated name")]

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    using Foundation;
    using TreeWatch;
    using UIKit;
    using Xamarin;
    using Xamarin.Forms;

    /// <summary>
    /// App delegate.
    /// </summary>
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        /// <summary>
        /// Finisheds the launching.
        /// </summary>
        /// <returns><c>true</c>, if launching was finisheded, <c>false</c> otherwise.</returns>
        /// <param name="uiApplication">User interface application.</param>
        /// <param name="launchOptions">Launch options.</param>
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            Forms.Init();

            // http://forums.xamarin.com/discussion/21148/calabash-and-xamarin-forms-what-am-i-missing
            Forms.ViewInitialized += (sender, e) =>
            {
                // http://developer.xamarin.com/recipes/testcloud/set-accessibilityidentifier-ios/
                if (null != e.View.StyleId)
                {
                    e.NativeView.AccessibilityIdentifier = e.View.StyleId;
                }
            };

            FormsMaps.Init();

            // Code for starting up the Xamarin Test Cloud Agent
            #if ENABLE_TEST_CLOUD
            Calabash.Start();
            #endif
            CrossGeofence.Initialize<CrossGeofenceListener>();
            this.LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}
