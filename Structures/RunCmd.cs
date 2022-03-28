using System.Diagnostics;

namespace Structures;

public class RunCmd
{
    public static void RunCommand(string cmd, string args)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = cmd;
        start.Arguments = string.Format("{0}", args);
        start.UseShellExecute = true;
        Process.Start(start);
    }
}