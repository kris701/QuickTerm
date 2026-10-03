using System.Text.Json.Serialization;

namespace QuickTerm.Models.Github
{
	public class GithubReleaseModel
	{
		[JsonPropertyName("tag_name")]
		public string TagName { get; set; }
		[JsonPropertyName("created_at")]
		public DateTime CreatedAt { get; set; }
		[JsonPropertyName("assets")]
		public List<GithubReleaseModelAsset> Assets { get; set; }
	}
}
