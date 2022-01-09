using System.Threading.Tasks;

//using AiSorter;

namespace FileSorter9000.Core.Services
{
	public class PathUpdateService
	{
		//TODO: Add dependency injection for service implementation
		//public IPathPredictor PredictionService { get; private set; } = new OpenAiPathPredictor();

		//public PathUpdateService(IPathPredictor predictor)
		//{
		//	PredictionService = predictor;
		//}

		//public async Task<string> GetAiPredictedPathAsync(string path)
		//{
		//	return await PredictionService.GetSuggestedPathAsync(path).ConfigureAwait(true);
		//}

		public Task<string> GetId3TagRulesPathAsync(string path)
		{
			var predictor = new Mp3Mangler.Mp3Processor();
			string testFilePath = @"..\..\..\FileSorter9000\Assets\NerdRockFromTheSun.mp3";
			string newPath = predictor.GetAlphaAndArtistPath(testFilePath, @"C:\sorted\");
			
			//TODO: make Mp3Mangler more async
			return Task.FromResult(newPath);
		}
	}
}
