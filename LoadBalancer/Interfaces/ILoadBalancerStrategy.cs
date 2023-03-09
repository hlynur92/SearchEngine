namespace LoadBalancer.LoadBlancer
{
    public interface ILoadBalancerStrategy
    {
        public string NextService(List<string> services);

    }
}
