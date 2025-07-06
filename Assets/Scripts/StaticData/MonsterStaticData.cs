using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeID MonsterTypeID;

        [Range(1, 100)] public int Health;

        [Range(1f, 30f)] public float Damage;

        public int MaxLoot;
        public int MinLoot;

        [Range(1f, 30f)] public float MoveSpeed;

        [Range(0f, 1f)] public float EffectiveDistance;

        [Range(0f, 1f)] public float Cleavage;

        [Range(0f, 10f)] public float CooldownAttack;

        public GameObject Prefab;
    }
}