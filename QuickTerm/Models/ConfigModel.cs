using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;

namespace QuickTerm.Models
{
	public class ConfigModel
	{
		[Required]
		public List<TerminalNode> Nodes { get; set; } = new List<TerminalNode>();
		[Required]
		public bool RunOnStartup { get; set; } = false;

		public void Save()
		{
			File.WriteAllText("config.json", JsonSerializer.Serialize(this));
		}

		public ConfigModel()
		{
			Nodes = new List<TerminalNode>();
		}

		public ConfigModel(ConfigModel other)
		{
			Nodes = new List<TerminalNode>(other.Nodes);
		}

		public static ConfigModel Copy(ConfigModel other)
		{
			var item = JsonSerializer.Deserialize<ConfigModel>(JsonSerializer.Serialize(other));
			if (item == null)
				throw new Exception("Could not copy config!");
			return item;
		}
	}
}
