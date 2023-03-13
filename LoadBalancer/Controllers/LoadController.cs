using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoadController
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

        [HttpGet]
        public void RegisterService(string ip)
        {
            lb.AddServices(ip);
        }

        [HttpGet]
        public void RemoveService(string ip)
        {
            //lb.RemoveServices(ip);
        }
    }
}
