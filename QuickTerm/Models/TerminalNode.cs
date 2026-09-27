using System.ComponentModel.DataAnnotations;

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
