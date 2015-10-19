using SQLite;

namespace XLabsCamera
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection ();
	}
}
