using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;

namespace SearchAPI.Controllers
{
    public class LoadBalancerUtil : IHostedService
    {
        private HttpResponseMessage response;

        public async void notifyLoadBalancerAsync(string url)
        {
            do
            {
               Thread.Sleep(5000);
               using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://loadbalancer-1");
                    //Yours string value.

                    //Sending http post request.
                    response = await httpClient.PostAsync(url, null);
                }
            } while (response.StatusCode != HttpStatusCode.OK);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            notifyLoadBalancerAsync("/Load/RegisterService");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            notifyLoadBalancerAsync("/Load/RemoveService");

            return Task.CompletedTask;
        }
    }
}
