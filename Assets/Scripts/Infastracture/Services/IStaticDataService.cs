using StaticData;
using StaticData.Windows;
using UI.Services.Window;
using UnityEngine;

namespace Infastracture.Services
{
    public interface IStaticDataService: IService
    {
        void Load();
        MonsterStaticData ForMonster(MonsterTypeID monsterType);
        LevelStaticData ForLevel(string sceneKye);
        WindowConfig ForWindow(WindowId windowId);
    }
}