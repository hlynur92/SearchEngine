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

            api.BaseAddress = new Uri(lb.NextService());

            var task = api.GetStringAsync("/Search?terms=" + terms + "&numberOfResults=" + numberOfResults);
            task.Wait();

            var result = task.Result;

            return result;
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

        [HttpPost("RegisterServices")]
        public string RegisterService()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            lb.AddServices(ip);

            return "success";
        }

        [HttpPost("RemoveServices")]
        public string RemoveService()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            //lb.RemoveServices(ip);

            return "success";
        }
    }
}
