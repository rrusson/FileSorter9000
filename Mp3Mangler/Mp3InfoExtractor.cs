using CsvHelper;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mp3Mangler
{
	public class Mp3InfoExtractor
	{
		private FileReader _reader = new FileReader();


		public async Task CreateMp3DataFileAsync(string directory)
		{
			var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
			{
				HasHeaderRecord = true,
				Delimiter = ",",
				LineBreakInQuotedFieldIsBadData = true,
				DetectColumnCountChanges = true,
				TrimOptions = CsvHelper.Configuration.TrimOptions.Trim,
				Quote = '"',
				ShouldQuote = IsText,
				BadDataFound = LogBadData
			};

			//var folder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("ExampleFolderToken");

			//using (var csv = new CsvWriter(new StreamWriter($@"{directory}\mp3list.csv"), config))
			using (var csv = new CsvWriter(new StreamWriter($@"{directory}\mp3list.csv"), config))
			{
				var mp3Info = GetMp3InfoCollection(directory).ToList();
				await csv.WriteRecordsAsync(mp3Info);
			}
		}

		private bool IsText(ShouldQuoteArgs args)
		{
			return !IsNumeric(args.Field);
		}

		private void LogBadData(BadDataFoundArgs args)
		{
			Console.WriteLine($"BAD MP3 DATA:{args.RawRecord}");
		}


		/// <summary>
		/// Gets MP3 tag information for all files in the passed <paramref name="directory"/>
		/// </summary>
		/// <param name="directory">Folder to catalog</param>
		public IEnumerable<Mp3TagAndFileInfo> GetMp3InfoCollection(string directory)
		{
			IEnumerable<string> files = _reader.GetFileNamesFromDirectory(directory);

			foreach (var file in files)
			{
				TagLib.Tag mp3Info = _reader.GetTag(file);
				var info = _reader.GetFileInfo(file);

				yield return new Mp3TagAndFileInfo()
				{
					FilePath = file,
					CreateDate = info.CreateDate,
					FileSize = info.Size,
					TagInfo = mp3Info
				};
			}
		}


		/// <summary>
		/// Returns a new write path for an MP3 file (format: base + /first letter of Artist Name + /Artist Name)
		/// </summary>
		/// <param name="currentFilePath">The file's current path</param>
		/// <param name="basePath">A base path to write to</param>
		/// <returns>The new file path</returns>
		public string GetAlphaAndArtistPath(string currentFilePath, string basePath = null)
		{
			string root = currentFilePath.Substring(0, currentFilePath.LastIndexOf('\\'));
			TagLib.Tag idV2 = GetTagById3OrFileName(currentFilePath);

			string[] parts = idV2.AlbumArtists.Any()
				? idV2.AlbumArtists?[0].Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries)
				: new string[] { null };

			string album = idV2.Album ?? idV2.AlbumSort ?? "unknown";
			string artist = idV2.Performers[0]?.Trim() ?? idV2.FirstAlbumArtist?.Trim();
			string track = idV2.Track < 10 ? $"0{idV2.Track}" : idV2.Track.ToString();
			string title = idV2.Title ?? idV2.TitleSort;

			if (string.IsNullOrWhiteSpace(artist) || string.IsNullOrWhiteSpace(title))
			{
				return currentFilePath;
			}

			return $"{(basePath ?? root)}{artist[0]}\\{artist}\\{album}\\{track} - {title}.mp3";
		}

		/// <summary>
		/// Gets IdV2 Tag, filling in Artist and TrackName from filename, if missing
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public TagLib.Tag GetTagById3OrFileName(string filePath)
		{
			//Try to get all essential info from ID3 tag first
			TagLib.File mp3Obj = _reader.GetFile(filePath);
			TagLib.Tag idV2 = mp3Obj.GetTag(TagLib.TagTypes.Id3v2);

			string[] id3parts = idV2?.AlbumArtists == null || idV2.AlbumArtists.Length < 1
				? new[] { "", "" }
				: idV2.AlbumArtists?[0].Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

			var mp3Dto = new Mp3Dto();

			if (idV2?.Performers?.Length > 0)
			{
				mp3Dto.Artist = idV2.Performers[0];
			}

			string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);

			if (fileName.Contains("-"))
			{
				string[] fileParts = filePath.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

				if (fileParts?.Length > 1)
				{
					//(string artist, string title, string album, int track) parts;
					switch (fileParts.Length)
					{
						case 1:
							break;
						case 2:
							Mp3Dto dto = GetInfoFromTwoParts(fileParts[0], fileParts[1]);
							mp3Dto.Artist = dto.Artist;
							mp3Dto.Title = dto.Title;
							mp3Dto.TrackNum = dto.TrackNum;
							break;
						case 3:
							Mp3Dto dto2 = GetInfoFromThreeParts(fileParts);
							mp3Dto.Artist = dto2.Artist;
							mp3Dto.Title = dto2.Title;
							mp3Dto.Album = dto2.Album;
							mp3Dto.TrackNum = dto2.TrackNum;
							break;
						default:
							break;
					}
				}
			}

			//Add the main artist to album artists, if missing
			if (!string.IsNullOrWhiteSpace(mp3Dto.Artist) && !idV2.AlbumArtists.Any())
			{
				idV2.AlbumArtists = new string[] { mp3Dto.Artist.Trim() };
			}

			mp3Dto.Artist = id3parts[0].Trim();
			mp3Dto.Title = id3parts.Length < 2 ? "" : id3parts[1].Replace(".mp3", "").Trim();

			if (!string.IsNullOrWhiteSpace(mp3Dto.Artist) && !idV2.AlbumArtists.Any())
			{
				idV2.AlbumArtists = new string[] { mp3Dto.Artist.Trim() };
			}

			if (!string.IsNullOrWhiteSpace(mp3Dto.Title) && String.IsNullOrWhiteSpace(idV2.Title))
			{
				idV2.Title = mp3Dto.Title;
			}

			return idV2;
		}

		public Mp3Dto GetInfoFromTwoParts(string partOne, string partTwo)
		{
			Mp3Dto dto = new Mp3Dto();

			bool foundNumber = false;
			int num;

			if (int.TryParse(partOne.Trim(), out num))
			{
				foundNumber = true;
				dto.TrackNum = num;
				dto.Title = partTwo;
			}

			if (int.TryParse(partTwo.Trim(), out num))
			{
				foundNumber = true;
				dto.Album = partOne;
				dto.TrackNum = num;
			}

			if (!foundNumber)
			{
				dto.Artist = partOne;
				dto.Title = partTwo;
			}

			return dto;
		}

		private Mp3Dto GetInfoFromThreeParts(string[] parts)
		{
			if (parts?.Length != 3)
			{
				throw new ArgumentOutOfRangeException(nameof(parts), "parameter must have exactly 3 items");
			}

			var dto = new Mp3Dto();
			string partOne = parts[0];
			string partTwo = parts[1];
			string partThree = parts[2];

			bool foundNumber = false;
			int num;

			if (int.TryParse(partOne.Trim(), out num))
			{
				foundNumber = true;
				dto.TrackNum = num;
				dto.Artist = partTwo;
				dto.Title = partThree;
			}

			if (int.TryParse(partTwo.Trim(), out num))
			{
				foundNumber = true;
				dto.Artist = partOne;
				dto.TrackNum = num;
				dto.Title = partThree;
			}

			if (!foundNumber)
			{
				dto.Artist = partOne;
				dto.Album = partTwo;
				dto.Title = partThree;
			}

			return dto;
		}


		/// <summary>
		/// Corrects IDV1 and IDV2 tags
		/// </summary>
		/// <param name="mp3File"></param>
		/// <returns></returns>
		private bool ProcessMp3File(ref TagLib.File mp3File)
		{
			var idV1 = mp3File.GetTag(TagLib.TagTypes.Id3v1);
			var idV2 = mp3File.GetTag(TagLib.TagTypes.Id3v2);

			//return UseArtistToPopulateAlbum(idV1, idV2);
			//return CopyArtistToAlbumArtist(idV1, idV2);
			return GetArtistAndTitleFromFileName(mp3File.Name, idV1, idV2);
		}

		private bool UsePerformerToPopulateAlbumArtist(TagLib.Tag idV1, TagLib.Tag idV2)
		{
			bool isDirty = false;

			if (idV2?.AlbumArtists.Length > 0 && idV2.AlbumArtists[0].Contains(" - "))
			{
				string[] parts = idV2.AlbumArtists[0].Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
				var artist = idV2.Performers[0].Replace(" ", "");
				var title = idV2.Title.Replace(" ", "");

				Console.WriteLine(artist + "|" + parts[0]);
				Console.WriteLine(title + "|" + parts[1]);

				if (parts?.Length > 1
					&& parts[0].Replace(" ", "") == artist
					&& parts[1].Replace(" ", "") == title)
				{
					idV2.AlbumArtists = new string[] { parts[0].Trim() };
					isDirty = true;
				}
			}

			return isDirty;
		}

		private bool CopyArtistToAlbumArtist(TagLib.Tag idV1, TagLib.Tag idV2)
		{
			bool isDirty = false;

			if (idV2?.Performers?.Length > 0)
			{
				string artist = idV2.Performers[0];

				if (!string.IsNullOrWhiteSpace(artist) && !idV2.AlbumArtists.Any())
				{
					idV2.AlbumArtists = new string[] { artist.Trim() };
					isDirty = true;
				}
			}

			return isDirty;
		}

		private bool GetArtistAndTitleFromFileName(string file, TagLib.Tag idV1, TagLib.Tag idV2)
		{
			string fileName = file.Substring(file.LastIndexOf('\\') + 1);
			if (!fileName.Contains("-"))
			{
				return false;
			}

			string[] parts = fileName.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
			if (parts?.Length != 2)
			{
				return false;
			}

			bool isDirty = false;
			var artist = parts[0].Trim();
			var title = parts[1].Replace(".mp3", "").Trim();

			if (!string.IsNullOrWhiteSpace(artist) && !idV2.AlbumArtists.Any())
			{
				idV2.AlbumArtists = new string[] { artist.Trim() };
				isDirty = true;
			}

			if (!string.IsNullOrWhiteSpace(artist) && !idV2.Performers.Any())
			{
				idV2.Performers = new string[] { artist.Trim() };
				isDirty = true;
			}

			if (!string.IsNullOrWhiteSpace(title) && String.IsNullOrWhiteSpace(idV2.Title))
			{
				idV2.Title = title;
				isDirty = true;
			}

			return isDirty;
		}

		private bool IsNumeric(string text)
		{
			return int.TryParse(text?.Trim(), out int num);
		}
	}
}
