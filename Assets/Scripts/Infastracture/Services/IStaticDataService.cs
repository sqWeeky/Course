using StaticData;

namespace Infastracture.Services
{
    public interface IStaticDataService: IService
    {
        void Load();
        MonsterStaticData ForMonster(MonsterTypeID monsterType);
        LevelStaticData ForLevel(string sceneKye);
    }
}