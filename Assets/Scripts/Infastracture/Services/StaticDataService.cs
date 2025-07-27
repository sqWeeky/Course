using System.Collections.Generic;
using System.Linq;
using StaticData;
using UnityEngine;

namespace Infastracture.Services
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;

        public void Load()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.MonsterTypeID, x => x);

            _levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeID monsterType) =>
            _monsters.TryGetValue(monsterType, out MonsterStaticData staticData)
                ? staticData
                : null;

        public LevelStaticData ForLevel(string sceneKye) =>
            _levels.TryGetValue(sceneKye, out LevelStaticData staticData)
                ? staticData
                : null;
    }
}