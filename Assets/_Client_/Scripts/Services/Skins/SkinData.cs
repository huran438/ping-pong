using System;
using SFramework.Core.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Services.Skins
{
    [Serializable]
    public class SkinData : ISFDatabaseNode
    {
        public string Name => _name;
        public Color Color => _color;
        public int RequiredExperience => _requiredExperience;
        public ISFDatabaseNode[] Children => null;
        
        [SerializeField]
        private string _name;

        [SerializeField]
        private Color _color = Color.white;

        [SerializeField]
        private int _requiredExperience;
    }
}