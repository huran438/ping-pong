using System;
using UnityEngine.Serialization;

namespace _Client_.Scripts.Services.Skins
{
    [Serializable]
    public class SkinModel
    {
        public SkinModel(string name)
        {
            Name = name;
        }

        public string Name { get; }

        
        public bool Unlocked;
    }
}