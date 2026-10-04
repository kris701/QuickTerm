using System.IO;
using WindowsShortcutFactory;

namespace QuickTerm.Helpers
{
	internal static class ShortcutHelper
	{
		public static void GenerateShortcut(string folder, string filename, string linkPath)
		{
			string dir = $"{folder}\\{filename}.lnk";
			if (File.Exists(dir))
				File.Delete(dir);

			using var shortcut = new WindowsShortcut
			{
				Description = "Startup shortcut for QuickTerm",
				WorkingDirectory = linkPath.Replace($"{filename}.exe", ""),
				Path = linkPath,
			};
			shortcut.Save(dir);
		}

		public static void RemoveShortcut(string folder, string filename, string linkPath)
		{
			string dir = $"{folder}\\{filename}.lnk";
			if (File.Exists(dir))
				File.Delete(dir);
		}
	}
}
