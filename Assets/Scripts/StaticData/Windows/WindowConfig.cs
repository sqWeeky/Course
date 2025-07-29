using System;
using UI.Services.Window;
using UI.Windows;

namespace StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}