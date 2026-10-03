using QuickTerm.Models;

namespace QuickTerm.Services.Executors
{
	public interface IExecutorService
	{
		public Task ExecuteCommand(TerminalCommand command);
	}
}
