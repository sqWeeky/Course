using StaticData;

namespace Infastracture.Services
{
    public interface IStaticDataService: IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeID monsterType);
    }
}