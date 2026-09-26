using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuickTerm.Models
{
    public class TerminalNode
    {
		[Required]
		public string Name { get; set; } = "";
		[Required]
		public TerminalCommand? Command { get; set; } = null;
		[Required]
		public List<TerminalNode> Nodes { get; set; } = new List<TerminalNode>();
	}
}
