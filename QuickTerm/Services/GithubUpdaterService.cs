using SerializableHttps;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Windows;

namespace QuickTerm.Services
{
	public class GithubUpdaterService
	{
		public bool UpdateAvailable { get; set; } = false;
		public string AssetURL { get; set; } = "";

		public async Task<bool> GetNewestVersion()
		{
			var http = new SerializableHttpsClient();
			http.AddHeader("X-GitHub-Api-Version", "2026-03-10");
			http.AddHeader("User-Agent", "QuickTerm-Client");
			http.AddHeader("Accept", "application/vnd.github+json");

			var url = $"https://api.github.com/repos/kris701/QuickTerm/releases/latest";
			var version = await http.GetAsync<GithubReleaseModel>(url);

			var thisVersion = Assembly.GetEntryAssembly()?.GetName().Version!;
			var thisVersionStr = $"v{thisVersion.Major}.{thisVersion.Minor}.{thisVersion.Build}";

			if (version != null && version.TagName != thisVersionStr)
			{
				var asset = version.Assets.FirstOrDefault(x => x.Name == "QuickTerm.exe");
				if (asset != null)
				{
					UpdateAvailable = true;
					AssetURL = asset.DownloadURL;
				}
			}

			return UpdateAvailable;
		}

		public async Task UpdateToNewestVersion()
		{
			if (AssetURL == "")
				return;

			HttpClient webClient = new HttpClient();
			webClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2026-03-10");
			webClient.DefaultRequestHeaders.Add("User-Agent", "QuickTerm-Client");
			webClient.DefaultRequestHeaders.Add("Accept", "application/octet-stream");
			var stream = await webClient.GetStreamAsync(AssetURL);
			var ms = new MemoryStream();
			await stream.CopyToAsync(ms);

			if (Directory.Exists("tmp"))
				Directory.Delete("tmp", true);
			Directory.CreateDirectory("tmp");
			var path = Path.Combine("tmp", "QuickTerm.exe");
			await File.WriteAllBytesAsync(path, ms.ToArray());

			Process p = new Process();
			p.StartInfo.FileName = "powershell.exe";
			p.StartInfo.Arguments = "Start-Sleep -Seconds 2 ; Remove-Item ./QuickTerm.exe ; Move-Item -Path ./tmp/QuickTerm.exe -Destination ./QuickTerm.exe ; ./QuickTerm.exe";
			p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			p.Start();
			Application.Current.Shutdown();
		}

		public class GithubReleaseModel
		{
			[JsonPropertyName("tag_name")]
			public string TagName { get; set; }
			[JsonPropertyName("created_at")]
			public DateTime CreatedAt { get; set; }
			[JsonPropertyName("assets")]
			public List<GithubReleaseModelAsset> Assets { get; set; }
		}

		public class GithubReleaseModelAsset
		{
			[JsonPropertyName("name")]
			public string Name { get; set; }
			[JsonPropertyName("url")]
			public string DownloadURL { get; set; }
		}
	}
}
