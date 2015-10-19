using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XLabs.Forms;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;
using XLabsCamera.Photos;
using ThingsILove.iOS.Services.Database;

namespace XLabsCamera.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : XFormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start ();
			#endif

			DependencyService.Register<MediaPicker> ();
			DependencyService.Register<PhotoService> ();
			DependencyService.Register<SQLiteTouch> ();
			DependencyService.Register<SavePhotoToSecretGallery> ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		public async void SavePictureToDisk (String photoPath)
		{
			UIImage someImage = new UIImage (photoPath);
			someImage.SaveToPhotosAlbum ((image, error) => {
				var o = image as UIImage;
				//				Console.WriteLine ("Photo saved.");
			});
		}
	}
}

