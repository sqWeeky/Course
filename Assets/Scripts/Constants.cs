using UnityEngine;

public class Constants : MonoBehaviour
{
    public class PlayerSettings
    {
        public static readonly float MoveSpeed = 5f;
    }

    public class AssetPath
    {
        public const string PlayerPath = "Player";
        public const string HubPath = "Hub/Hud";
        public const string Spawner = "Enemies/SpawnPoint";
        public const string Loot = "Loot/Loot";
    }

    public class InputService
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string ButtonFire = "Fire";
    }
}