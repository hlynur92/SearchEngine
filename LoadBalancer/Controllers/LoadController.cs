using Microsoft.AspNetCore.Mvc;
using System.Text;

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

        [HttpPost]
        public string registerService(string service)
        {
            throw new NotImplementedException();
        }
    }
}
