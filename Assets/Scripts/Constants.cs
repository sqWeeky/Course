using UnityEngine;

public class Constants : MonoBehaviour
{
    public class PlayerSettings
    {
        public static readonly float MoveSpeed = 5f;
    }

    public class AssetAddress
    {
        public const string PlayerPath = "Player";
        public const string HubPath = "Hub";
        public const string Spawner = "SpawnPoint";
        public const string Loot = "Loot";
        public const string UIRoot = "UIRoot";
    }

    public class InputService
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string ButtonFire = "Fire";
    }

    public class Tag
    {
        public const string PlayerTag = "Player";
        public const string InitialPointTag = "InitialPlayerPoint";
    }
}