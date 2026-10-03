using System.ComponentModel.DataAnnotations;
using System.IO;

namespace QuickTerm.Models
{
	public class TerminalCommand
	{
		[Required]
		public string Command { get; set; } = "";
		[Required]
		public string WorkingDirectory { get; set; } = Directory.GetCurrentDirectory();
		[Required]
		public TerminalTypes TerminalType { get; set; } = TerminalTypes.PowerShell;
		[Required]
		public bool ShowTerminal { get; set; } = true;
		[Required]
		public bool PauseOnCompletion { get; set; } = true;
	}
}
