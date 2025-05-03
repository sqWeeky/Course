using Infastracture.Factory;

namespace Infastracture.Services
{
    public class AllServices
    {
        private static AllServices s_instance;
        public static AllServices Container => s_instance ??= new AllServices();

        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService
            => Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}