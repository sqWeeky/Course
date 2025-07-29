using System.Collections.Generic;
using System.Linq;
using StaticData;
using StaticData.Windows;
using UI.Services.Window;
using UnityEngine;

namespace Infastracture.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataMonstersPath = "StaticData/Monsters";
        private const string StaticDataLevelsPath = "StaticData/Levels";
        private const string StaticDataWindowsPath = "StaticData/UI/WindowStaticData";
        
        private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public void Load()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(StaticDataMonstersPath)
                .ToDictionary(x => x.MonsterTypeID, x => x);

            _levels = Resources.LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);
            _windowConfigs = Resources.Load<WindowsStaticData>(StaticDataWindowsPath).Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeID monsterType) =>
            _monsters.TryGetValue(monsterType, out MonsterStaticData staticData)
                ? staticData
                : null;

        public LevelStaticData ForLevel(string sceneKye) =>
            _levels.TryGetValue(sceneKye, out LevelStaticData staticData)
                ? staticData
                : null;


        public WindowConfig ForWindow(WindowId windowId) =>
            _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
                ? windowConfig
                : null;
    }
}