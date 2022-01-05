using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3Mangler
{
    public class FileReader
    {
        public IEnumerable<string> GetFileNamesFromDirectory(string dirName)
        {
            var files = System.IO.Directory.GetFiles(dirName, "*.mp3", System.IO.SearchOption.AllDirectories);
            return files;
        }

        public (long Size, DateTime CreateDate) GetFileInfo(string path)
		{
            var info = new System.IO.FileInfo(path);
            
            return (info.Length, info.CreationTime);
        }

        /// <summary>
        /// Returns a TagLib file reference with metadata for MP3 file at <paramref name="filePath"/>
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>TagLib.File object based on the MP3</returns>
        public TagLib.File GetFile(string filePath)
        {
            TagLib.File tagFile = TagLib.File.Create(filePath);

            return tagFile;
        }

        /// <summary>
        /// Returns complete metadata for MP3 file at <paramref name="filePath"/>
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>TagLib.Tag metadata for the MP3</returns>
        public TagLib.Tag GetTag(string filePath)
        {
            TagLib.File tagFile = TagLib.File.Create(filePath);

            return tagFile.GetTag(TagLib.TagTypes.Id3v2);
        }
    }
}
