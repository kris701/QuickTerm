using QuickTerm.Models;
using QuickTerm.Services.Executors;

namespace QuickTerm.Services
{
	public class TerminalExecutorService
	{
		private readonly CMDExecutorService _cmdExecutor;
		private readonly PowershellExecutorService _powershellExecutor;

		public TerminalExecutorService(CMDExecutorService cmdExecutor, PowershellExecutorService powershellExecutor)
		{
			_cmdExecutor = cmdExecutor;
			_powershellExecutor = powershellExecutor;
		}

		public async Task ExecuteCommand(TerminalCommand command)
		{
			switch (command.TerminalType)
			{
				case TerminalTypes.CMD:
					await _cmdExecutor.ExecuteCommand(command);
					break;
				case TerminalTypes.PowerShell:
					await _powershellExecutor.ExecuteCommand(command);
					break;
			}
		}
	}
}
