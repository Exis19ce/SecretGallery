using System.Collections.Generic;
using System.Linq;
using XLabsCamera;
using SQLite;
using Xamarin.Forms;

namespace XLabsCamera.Photos
{

	public class PhotoService:IPhotoService
	{
		private readonly SQLiteConnection _connection;

		public PhotoService ()
		{
			_connection = DependencyService.Get<ISQLite> ().GetConnection ();
			_connection.CreateTable<PhotoItem> ();
		}

		public IEnumerable<PhotoItem> GetThings ()
		{
			var photos = _connection.Table<PhotoItem> ().ToList ();
			return photos;
		}

		public void Add (PhotoItem item)
		{
			_connection.Insert (item);
		}

		public int GetID ()
		{
			var id = _connection.Table<PhotoItem> ().Count ();
			return id;
		}

		public void RemoveAll ()
		{
			_connection.DeleteAll<PhotoItem> ();
			_connection.UpdateAll (_connection.Table<PhotoItem> ());
		}
	}
}
