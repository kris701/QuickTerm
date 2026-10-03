using QuickTerm.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
		[Required]
		public bool CheckForUpdates { get; set; } = false;

		public void Save()
		{
			File.WriteAllText("config.json", JsonSerializer.Serialize(this));
			ApplySetupActions();
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

		private void ApplySetupActions()
		{
			SetStartupSetting();
		}

		private void SetStartupSetting()
		{
			var module = Process.GetCurrentProcess().MainModule;
			if (module == null || module.FileName == null)
				throw new Exception("Could not find current running assembly!");

			if (RunOnStartup)
				ShortcutHelper.GenerateShortcut(
					$"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\",
					"GameWatch",
					module.FileName);
			else
				ShortcutHelper.RemoveShortcut(
					$"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\",
					"GameWatch",
					module.FileName);
		}
	}
}
