using System.Collections.Generic;
using System.Linq;
using XLabsCamera;
using SQLite;
using Xamarin.Forms;

namespace XLabsCamera.Photos
{
	public interface IPhotoService
	{
		void Add (PhotoItem item);

		void RemoveAll ();

		IEnumerable<PhotoItem> GetThings ();

		int GetID ();
	}
}
