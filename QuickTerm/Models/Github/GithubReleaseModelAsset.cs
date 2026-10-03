using System.Text.Json.Serialization;

namespace QuickTerm.Models.Github
{
	public class GithubReleaseModelAsset
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("url")]
		public string DownloadURL { get; set; }
	}
}
