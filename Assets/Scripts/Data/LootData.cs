using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public LootPieceDataDictinary LootPiecesOnScene = new();

        public Action Changed;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            Changed?.Invoke();
        }
    }
}