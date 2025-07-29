using Infastracture.Services;

namespace UI.Services.Window
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);
    }
}