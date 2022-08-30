using System;

namespace Mp3Mangler
{
	public class Mp3TagAndFileInfo
	{
		public TagLib.Tag TagInfo { get; set; }

		public string FilePath { get; set; }

		public long FileSize { get; set; }

		public DateTime CreateDate { get; set; }
	}
}
