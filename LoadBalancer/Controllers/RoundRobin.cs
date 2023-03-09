using LoadBalancer.LoadBlancer;

namespace LoadBalancer.Controllers
{
    public class RoundRobin : ILoadBalancerStrategy
    {
        public string NextService(List<string> services)
        {
            throw new NotImplementedException();
        }
    }
}
