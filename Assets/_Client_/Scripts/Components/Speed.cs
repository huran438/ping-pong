using System;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Components
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