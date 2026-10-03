using System.IO;

namespace QuickTerm.Helpers
{
	internal static class ShortcutHelper
	{
		public static void GenerateShortcut(string folder, string filename, string linkPath)
		{
			string dir = $"{folder}\\{filename}.lnk";
			if (File.Exists(dir))
				File.Delete(dir);

			IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
			IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(dir);

			shortcut.Description = "Startup shortcut for QuickTerm";
			shortcut.WorkingDirectory = linkPath.Replace($"{filename}.exe", "");
			shortcut.TargetPath = linkPath;
			shortcut.Save();
		}

		public static void RemoveShortcut(string folder, string filename, string linkPath)
		{
			string dir = $"{folder}\\{filename}.lnk";
			if (File.Exists(dir))
				File.Delete(dir);
		}
	}
}
