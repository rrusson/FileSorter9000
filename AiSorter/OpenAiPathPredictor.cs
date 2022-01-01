using OpenAI_API;

using System.Threading.Tasks;

namespace AiSorter
{
	public class OpenAiPathPredictor : IPathPredictor
	{
		public async Task<string> GetSuggestedPathAsync(string input)
		{
			var api = new OpenAIAPI(engine: Engine.Davinci);

			var result = await api.Completions.CreateCompletionAsync(input, temperature: 0.1);
			return result.ToString();
		}
	}
}
