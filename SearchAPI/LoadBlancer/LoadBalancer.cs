namespace SearchAPI.LoadBlancer
{
    public class LoadBalancer : ILoadBalancer
    {
        public int AddServices(string url)
        {
            throw new NotImplementedException();
        }

        public ILoadBalancerStrategy GetActiveStrategy()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllServices()
        {
            throw new NotImplementedException();
        }

        public string NextService()
        {
            throw new NotImplementedException();
        }

        public int RemoveServices(int id)
        {
            throw new NotImplementedException();
        }

        public void SetActiveStrategy(ILoadBalancerStrategy strategy)
        {
            throw new NotImplementedException();
        }
    }
}
