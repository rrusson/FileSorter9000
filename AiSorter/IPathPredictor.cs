using System.Threading.Tasks;

namespace AiSorter
{
	//TODO: Add various implementations of IPathPredictor for different ML models and rules-based approaches
	public interface IPathPredictor
	{
		Task<string> GetSuggestedPathAsync(string input);
	}
}