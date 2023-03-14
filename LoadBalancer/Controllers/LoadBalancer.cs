using LoadBalancer.LoadBlancer;

namespace LoadBalancer.Controllers
{
    public class LoadBalancer : ILoadBalancer
    {
        private List<string> _services;
        private ILoadBalancerStrategy _activeStrategy;

        public LoadBalancer()
        {
            ILoadBalancerStrategy activeStrategy = new RoundRobin();
            _services = new List<string>();
        }

        public int AddServices(string url)
        {
            _services.Add(url);

            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public int RemoveServices(int id)
        {
            if(_services.Count != 0)
                _services.RemoveAt(id);

            throw new NotImplementedException();
        }

        public void SetActiveStrategy(ILoadBalancerStrategy strategy)
        {
            _activeStrategy = strategy;
        }
    }
}
