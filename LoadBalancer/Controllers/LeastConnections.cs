using LoadBalancer.LoadBlancer;
using System.Xml.Linq;

namespace LoadBalancer.Controllers
{
    public class LeastConnections : ILoadBalancerStrategy
    {
        IDictionary<string, int> service_connection_tracking = new Dictionary<string, int>();
        public string NextService(List<string> services)
        {
            //Remove any services that no longer exist
            service_connection_tracking.ToList().ForEach(kvp => {
                if (!services.Contains(kvp.Key)) service_connection_tracking.Remove(kvp.Key);
            });
            //Add tracking to services that aren't already being tracked
            services.ForEach(service => {
                if (!service_connection_tracking.ContainsKey(service)) {
                    service_connection_tracking[service] = 0;
                }
            });
            return service_connection_tracking.MinBy(kvp => kvp.Value).Key;
        }
        public void AddActiveConnection(string service)
        {
            service_connection_tracking[service] += 1;
            Console.WriteLine("Added active connection for "+service+". Active Connections: "+ service_connection_tracking[service]);
        }
        public void RemoveActiveConnection(string service)
        {
            int curr_val = service_connection_tracking[service];
            service_connection_tracking[service] = curr_val > 0 ? curr_val - 1 : 0;

            Console.WriteLine("Removed active connection for " + service + ". Active Connections: " + service_connection_tracking[service]);
        }
    }
}
