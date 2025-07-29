using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "StaticData/Window static data")]
    public class WindowsStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}