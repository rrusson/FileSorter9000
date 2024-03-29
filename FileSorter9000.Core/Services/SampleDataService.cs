﻿using System.Collections.Generic;
using System.Threading.Tasks;

using FileSorter9000.Core.Models;

namespace FileSorter9000.Core.Services
{
	public static class SampleDataService
    {
        private static ICollection<SampleImage> _gallerySampleData;

        //TODO: Remove this once image gallery page is displaying real data
        public static async Task<IEnumerable<SampleImage>> GetImageGalleryDataAsync(string localResourcesPath)
        {
            if (_gallerySampleData == null)
            {
                _gallerySampleData = new List<SampleImage>();
                for (int i = 1; i <= 10; i++)
                {
                    _gallerySampleData.Add(new SampleImage()
                    {
                        ID = $"{i}",
                        Source = $"{localResourcesPath}/SampleData/SamplePhoto{i}.png",
                        Name = $"Sample picture {i}"
                    });
                }
            }

            await Task.CompletedTask;
            return _gallerySampleData;
        }
    }
}
