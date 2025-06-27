using System.Collections.Generic;
using System.Linq;
using StaticData;
using UnityEngine;

namespace Infastracture.Services
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.MonsterTypeID, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeID monsterType) =>
            _monsters.TryGetValue(monsterType, out MonsterStaticData staticData) 
                ? staticData 
                : null;
    }
}