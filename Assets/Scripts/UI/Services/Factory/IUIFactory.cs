using System.Threading.Tasks;
using Infastracture.Services;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        Task CreateUIRoot();
    }
}