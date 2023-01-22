using System;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace Client.Components
{
    [Serializable, SFGenerateComponent]
    public struct Speed : ISFComponent, IEcsAutoInit<Speed>
    {
        public float value;

        [HideInInspector]
        public float _value;

        public void AutoInit(ref Speed c)
        {
            c._value = c.value;
        }
    }
}