using Data;
using System.Collections.Generic;
using System;

[Serializable]
public class LootPieceDataDictionary
{
    public List<string> Keys = new List<string>();
    public List<LootPieceData> Values = new List<LootPieceData>();

    public void Add(string key, LootPieceData value)
    {
        Keys.Add(key);
        Values.Add(value);
    }

    public LootPieceData Get(string key)
    {
        int index = Keys.IndexOf(key);
        return index >= 0 ? Values[index] : null;
    }

    public void Remove(string key)
    {
        int index = Keys.IndexOf(key);
        if (index >= 0)
        {
            Keys.RemoveAt(index);
            Values.RemoveAt(index);
        }
    }

    public bool ContainsKey(string key) =>
        Keys.Contains(key);
}