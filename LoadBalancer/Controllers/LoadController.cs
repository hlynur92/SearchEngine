using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoadController : Controller
    {
        LoadBalancer lb;

        public LoadController(LoadBalancer lb)
        {
            this.lb = lb;
        }

        [HttpGet]
        public string Search(string terms, int numberOfResults)
        {
            HttpClient api = new HttpClient();

            api.BaseAddress = new Uri(lb.NextService());

            var task = api.GetStringAsync("/Search?terms=" + terms + "&numberOfResults=" + numberOfResults);
            task.Wait();

            var result = task.Result;

            return result;
        }

        [HttpPost]
        public void RegisterService()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            lb.AddServices(ip);
        }

        [HttpPost]
        public void RemoveService()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            //lb.RemoveServices(ip);
        }
    }
}
