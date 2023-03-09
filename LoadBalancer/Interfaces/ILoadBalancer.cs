namespace LoadBalancer.LoadBlancer
{
    public interface ILoadBalancer
    {
        public List<string> GetAllServices();
        public int AddServices(string url);
        public int RemoveServices(int id);
        public ILoadBalancerStrategy GetActiveStrategy();
        public void SetActiveStrategy(ILoadBalancerStrategy strategy);
        public string NextService();
    }
}
