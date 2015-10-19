using System;
using UIKit;
using Foundation;
using System.IO;
using XLabsCamera.Photos;
using Xamarin.Forms;

namespace XLabsCamera
{
	public class SavePhotoToSecretGallery :ISavePhotoToSecretGallery
	{
		private IPhotoService _dataBaseService;

		public string CopyPhotoTo (string photoPath)
		{
			_dataBaseService = DependencyService.Get<IPhotoService> ();

			var imgData = new UIImage (photoPath).AsJPEG ();

			var ID = _dataBaseService.GetID ();

			var ExtensionsFile = ".jpg";

			string jpgFilename = System.IO.Path.Combine (GetPath (), ID + ExtensionsFile);

			NSError err = null;

			if (imgData.Save (jpgFilename, false, out err)) {
				Console.WriteLine ("saved as " + jpgFilename);

			} else {
				Console.WriteLine ("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
			}
			return ID + ExtensionsFile;
		}

		public string GetPath ()
		{
			var root = Environment.GetFolderPath (Environment.SpecialFolder.Personal);

			var documentsDirectory = Path.Combine (root, "SavedPhotos/.sercet");

			if (!Directory.Exists (documentsDirectory))
				Directory.CreateDirectory (documentsDirectory);
			
			return documentsDirectory;
		}
	}
}

