using System;
using SQLite;

namespace XLabsCamera
{
	[Table ("PhotoItem")]
	public class PhotoItem
	{
		[PrimaryKey, AutoIncrement, Column ("Id")]
		private Guid ID { get; set; }

		[Column ("Source")]
		public string Source { get; set; }
	}
}

