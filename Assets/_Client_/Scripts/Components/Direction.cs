using System;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace Client.Components
{
    [Serializable, SFGenerateComponent]
    public struct Direction : ISFComponent
    {
        public Vector3 value;
    }
}