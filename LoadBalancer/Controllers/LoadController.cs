using LoadBalancer.LoadBlancer;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoadController : Controller
    {
        ILoadBalancer lb;

        public LoadController(ILoadBalancer lb)
        {
            this.lb = lb;
        }

        [HttpGet("Search")]
        public string Search(string terms, int numberOfResults)
        {
            HttpClient api = new HttpClient();
            string service = lb.NextService();
            api.BaseAddress = new Uri("http://"+service);
            var strategy = lb.GetActiveStrategy();
            bool isLeastConnections = strategy is LeastConnections;

            if (isLeastConnections) ((LeastConnections)strategy).AddActiveConnection(service);
            var task = api.GetStringAsync("/Search?terms=" + terms + "&numberOfResults=" + numberOfResults);
            task.Wait();
            if (isLeastConnections) ((LeastConnections)strategy).RemoveActiveConnection(service);
            return task.Result;
        }

        [HttpGet("GetServices")]        
        public List<string> GetServices()
        {
            return lb.GetAllServices();
        }

        [HttpPost("SetActiveStrategy")]
        public string SetActiveStrategy(string strategy)
        {
            if (strategy == "roundrobin")
            {
                lb.SetActiveStrategy(new RoundRobin());

                return "success";
            }
            if (strategy == "leastconnections")
            {
                lb.SetActiveStrategy(new LeastConnections());

                return "success";
            }
            else
            {
                return "fail";
            }

        }

        [HttpGet("RegisterServices")]
        public string RegisterService()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString().Replace("::ffff:", "");
            lb.AddServices(ip);

            return "success";
        }

        [HttpGet("RemoveServices")]
        public string RemoveService()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString().Replace("::ffff:", "");
            //lb.RemoveServices(ip);

            return "success";
        }
    }
}
