using System;
using UnityEngine;

namespace Logic
{
    public class UniqueID : MonoBehaviour
    {
        public string ID;
        
        public void GenerateId() => 
            ID = $"{gameObject.scene.name}_{Guid.NewGuid().ToString()}";
    }
}