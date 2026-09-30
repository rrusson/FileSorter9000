using OpenAI.Chat;

using System;
using System.Threading.Tasks;

namespace AiSorter
{
	public class OpenAiPathPredictor : IPathPredictor
	{
		public async Task<string> GetSuggestedPathAsync(string input)
		{
			string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
			if (string.IsNullOrWhiteSpace(apiKey))
			{
				throw new InvalidOperationException("Set the OPENAI_API_KEY environment variable before using OpenAiPathPredictor.");
			}

			var client = new ChatClient("gpt-4o-mini", apiKey);
			var completion = await client.CompleteChatAsync(input).ConfigureAwait(false);
			return completion.Value.Content.Count > 0 ? completion.Value.Content[0].Text : string.Empty;
		}
	}
}
