using LoadBalancer.LoadBlancer;

namespace LoadBalancer.Controllers
{
    public class RoundRobin : ILoadBalancerStrategy
    {
        int index = 0;
        int count;

        public string NextService(List<string> services)
        {
            count = services.Count;

            if (index == count)
            {
                index = 0;
            }

            var useNext = index;
            index++;

            return services[useNext];
        }
    }
}
