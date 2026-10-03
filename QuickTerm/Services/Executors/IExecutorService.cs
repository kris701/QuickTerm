using QuickTerm.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickTerm.Services.Executors
{
	public interface IExecutorService
	{
		public Task ExecuteCommand(TerminalCommand command);
	}
}
