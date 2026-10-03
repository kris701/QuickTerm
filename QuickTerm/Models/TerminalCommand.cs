using System.ComponentModel.DataAnnotations;

namespace QuickTerm.Models
{
	public class TerminalCommand
	{
		[Required]
		public string Command { get; set; } = "";
		[Required]
		public TerminalTypes TerminalType { get; set; } = TerminalTypes.PowerShell;
		[Required]
		public bool ShowTerminal { get; set; } = true;
		[Required]
		public bool PauseOnCompletion { get; set; } = true;
	}
}
