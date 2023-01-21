using System;
using SFramework.ECS.Runtime;

namespace Client.Components
{
    [Serializable, SFGenerateComponent]
    public struct Speed : ISFComponent
    {
        public float value;
    }
}