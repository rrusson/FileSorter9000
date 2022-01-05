using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3Mangler
{
    class Program
    {
        static void Main(string[] args)
        {
            //var processor = new Mp3Processor();
            //processor.CleanFiles(@"D:\\Code\\FileSorter9000\\Targets\\");
            var info = new Mp3InfoExtractor();

            info.CreateMp3DataFileAsync(@"E:\temp\MUSIC SORTED").Wait();
            Console.ReadKey();
        }
    }
}
