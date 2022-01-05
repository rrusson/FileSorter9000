using System.Collections.Generic;
using System.Threading.Tasks;

using Mp3Mangler;

namespace FileSorter9000.Core.Services
{
	public static class Mp3DataService
	{
		/// <summary>
		/// Creates a .CSV file with a dump of all tags in an MP3 file
		/// </summary>
		/// <param name="directory"></param>
		/// <returns></returns>
		public static async Task DumpMp3DataAsync(string directory)
		//public static async Task<IEnumerable<Mp3TagAndFileInfo>> DumpMp3DataAsync(string directory)
		{
			var infoExtractor = new Mp3InfoExtractor();
			await infoExtractor.CreateMp3DataFileAsync(directory);

			//var files = infoExtractor.GetMp3InfoCollection(directory);

			//return (IEnumerable<Mp3TagAndFileInfo>)Task.FromResult(files);
		}
	}
}
