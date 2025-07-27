using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class LootPieceDataDictinary
    {
        public List<string> Id = new();
        public List<LootPieceData> LootPieceData = new();

        public void Add(string key, LootPieceData value)
        {
            Id.Add(key);
            LootPieceData.Add(value);
        }

        public void Remove(string key)
        {
            int index = Id.IndexOf(key);
            Id.Remove(key);
            LootPieceData.RemoveAt(index);
        }

        public LootPieceData Get(string key)
        {
            if (Id.Contains(key))
                return LootPieceData[Id.IndexOf(key)];

            return null;
        }

        public bool Contains(string key) =>
            Id.Contains(key);
    }
}