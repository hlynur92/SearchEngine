using LoadBalancer.LoadBlancer;

namespace LoadBalancer.Controllers
{
    public class LoadBalancer : ILoadBalancer
    {
        private List<string> _services;
        private ILoadBalancerStrategy _activeStrategy;

        public LoadBalancer()
        {
            _activeStrategy = new LeastConnections();
            _services = new List<string>();
        }

        public int AddServices(string url)
        {
            _services.Add(url);

            return _services.IndexOf(url); //Return index?
        }

        public ILoadBalancerStrategy GetActiveStrategy()
        {
            return _activeStrategy;
        }

        public List<string> GetAllServices()
        {
            return _services;
        }

        public string NextService()
        {
            return _activeStrategy.NextService(_services);
        }

        public int RemoveServices(int id)
        {
            if(_services.Count != 0)
                _services.RemoveAt(id);
            
            return id; //Return index ?
        }

        public void SetActiveStrategy(ILoadBalancerStrategy strategy)
        {
            _activeStrategy = strategy;
            Console.WriteLine("Set Active Strategy: " + strategy.GetType().Name);
        }
    }
}
