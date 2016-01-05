# Handoverinformation
## Credentials
To continue working on this project you need the following credentials:
* Access to the Github repository
* License for Xamarin 
* *Optional: Google Account that contains the API Key*

If you want to checkout the project read access to the repository and a trial xamarin license is fine.

**!!! Important !!!**
If you do not have access to the Google account that belongs to the API key used, then you need to exchange the GoogleMaps API Key in the `AndroidManifest.xml`, which is explained in detail later on.

## Getting the project running
* Install Xamarin Studio 
  * [Get it here] (https://xamarin.com/download)
* Install Xcode and the latest iOS SDK
* Install Xamarin Android Player 
  *  [Get it here] (https://xamarin.com/android-player) 
*  Download an android device in Xamarin Android Player
*  Install Google Play Services for the emulator
  * [Follow this guide] (https://university.xamarin.com/resources/how-to-install-google-play-on-android-emulator)
* Checkout git repository
* Open project in Xamarin Studio
* Add your android app fingerprint to the Google Maps API key:
 * [How to determine your MD5 or SHA1 signature] (https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/MD5_SHA1/)
 * This [guide](https://developers.google.com/maps/documentation/android-api/signup#get_an_android_api_key) explains how you can add a fingerprint to an existing key or how to create a new key and add it to the app.
 * If you want to exchange the key, the android manifest is located at `Droid/Properties/AndroidManifest.xml`
*  You can now run the project from Xamarin Studio on an emulator
 
## Running on a Device

### iOS
* Get a valid development certificate for iOS development
* Get a valid provisioning profile for the app including the device you want to run on
* Connect the device
* Select the device inside of Xamarin Studio
* Deploy the app to the device

### Android
* Connect the device
* Select the device inside of Xamarin Studio
* Deploy the app to the device
