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

        public TagLib.File GetFile(string fileName)
        {
            TagLib.File tagFile = TagLib.File.Create(fileName); // track is the name of the mp3

            return tagFile;
        }


        public TagLib.Tag GetTag(string fileName)
        {
            TagLib.File tagFile = TagLib.File.Create(fileName); // track is the name of the mp3

            return tagFile.GetTag(TagLib.TagTypes.Id3v2);
        }
    }
}
