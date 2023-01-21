using System;
using SFramework.ECS.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Components
{
    [Serializable, SFGenerateComponent]
    public struct SmoothSpeed : ISFComponent
    {
        public float minValue;
        public float maxValue;
        public float duration;
        
        [NonSerialized]
        public float startTime;
    }
}