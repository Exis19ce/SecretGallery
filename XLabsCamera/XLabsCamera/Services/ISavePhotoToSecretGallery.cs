using System;

namespace XLabsCamera
{
	public interface ISavePhotoToSecretGallery
	{
		string CopyPhotoTo (string photoPath);

		string GetPath ();
	}
}

