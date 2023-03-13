using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;

namespace SearchAPI.Controllers
{
    public class LoadBalancerUtil : IHostedService
    {
        private string ip;
        private HttpResponseMessage response;

        public void fetchIpAddress()
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
                ip = Regex.Replace(reader.ReadToEnd(), "\t|\n|\r", "");
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

        public async void notifyLoadBalancerAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost/");
                //Yours string value.
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("MyStringContent", "someString")
                });

                //Sending http post request.
                response = await httpClient.PostAsync($"rest/of/apiadress/", content);
            }

            //Here you save your response to Entity:
            var contentStream = await response.Content.ReadAsStreamAsync();

            //Options to mach yours naming styles.
            /*var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };*/

            //Here you go. Yours response as an entity:
            var result = await JsonSerializer.DeserializeAsync<string>(contentStream);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            fetchIpAddress();
            notifyLoadBalancerAsync();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            notifyLoadBalancerAsync();

            return Task.CompletedTask;
        }
    }
}
