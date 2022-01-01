using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mp3Mangler;

namespace Mp3ManglerTest
{
	[TestClass]
	public class Mp3ManglerTest
	{
		private Mp3Processor _sut;

		[TestInitialize]
		public void Setup()
		{
			_sut = new Mp3Processor();
		}


		[TestMethod]
		public void GetAlphaAndArtistPath_Test()
		{
			string testFilePath = @"..\..\..\FileSorter9000\Assets\NerdRockFromTheSun.mp3";

			string newPath = _sut.GetAlphaAndArtistPath(testFilePath, @"C:\temp\");

			//Basic algorithm should create a path with "First initial of Artist/Artist/Album/Track# - TrackName.mp3"
			Assert.AreEqual(@"C:\temp\R\Ryan Russon\Unreleased Crap\00 - Nerd Rock From The Sun.mp3", newPath);
		}
	}
}
