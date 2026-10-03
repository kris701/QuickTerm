using QuickTerm.Models;
using System.Text;

namespace QuickTerm.Services.Executors
{
	public class CMDExecutorService : IExecutorService
	{
		public async Task ExecuteCommand(TerminalCommand command)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.WindowStyle = command.ShowTerminal ? System.Diagnostics.ProcessWindowStyle.Normal : System.Diagnostics.ProcessWindowStyle.Hidden;
			startInfo.WorkingDirectory = command.WorkingDirectory;
			startInfo.FileName = "cmd.exe";
			var sb = new StringBuilder();
			sb.Append("/C ");
			sb.Append(command.Command);
			if (command.PauseOnCompletion)
				sb.Append(" & pause");
			startInfo.Arguments = sb.ToString();
			process.StartInfo = startInfo;
			process.Start();
		}
	}
}
