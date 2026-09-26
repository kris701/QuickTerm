using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Text.Json;

namespace QuickTerm.Models
{
    public class ConfigModel
    {
        [Required]
        public List<TerminalNode> Nodes { get; set; } = new List<TerminalNode>();

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
	}
}
