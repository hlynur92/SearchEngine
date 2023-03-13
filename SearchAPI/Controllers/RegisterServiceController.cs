using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SearchAPI.Controllers
{
    public class RegisterServiceController
    {

        public void registerService()
        {
            //Debug.WriteLine("run --name azurite -p 10000:10000 mcr.microsoft.com/azure-storage/azurite azurite-blob --blobHost 0.0.0.0");
            // Add services to the container.
            var processInfo = new ProcessStartInfo("docker");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;

            int exitCode;
            using (var process = new Process())
            {
                process.StartInfo = processInfo;
                var started = process.Start();
                //if (stdoutput)
                //{
                StreamReader reader = process.StandardOutput;
                var ip = Regex.Replace(reader.ReadToEnd(), "\t|\n|\r", "");
                if (string.IsNullOrEmpty(ip))
                {
                    Console.WriteLine("Unable to get ip of the container");
                    Environment.Exit(1);
                }
                Console.WriteLine("Azurite conatainer is listening @ {ip}");
                //}
                process.WaitForExit(12000);
                if (!process.HasExited)
                {
                    process.Kill();
                }
                exitCode = process.ExitCode;
                process.Close();
            }
        }
    }
}
