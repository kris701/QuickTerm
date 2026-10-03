using QuickTerm.Models;
using System.Text;

namespace QuickTerm.Services.Executors
{
	public class PowershellExecutorService : IExecutorService
	{
		public async Task ExecuteCommand(TerminalCommand command)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.WindowStyle = command.ShowTerminal ? System.Diagnostics.ProcessWindowStyle.Normal : System.Diagnostics.ProcessWindowStyle.Hidden;
			startInfo.FileName = "powershell.exe";
			var sb = new StringBuilder();
			sb.Append(command.Command);
			if (command.PauseOnCompletion)
				sb.Append(" ; Write-Host -NoNewLine 'Press any key to continue...'; $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');");
			startInfo.Arguments = sb.ToString();
			process.StartInfo = startInfo;
			process.Start();
		}
	}
}
