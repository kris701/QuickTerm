using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
	}
}
