using System;
using Client.Enum;
using SFramework.ECS.Runtime;

namespace Client.Components
{
    [Serializable, SFGenerateComponent]
    public struct Paddle : ISFComponent
    {
        public PaddleType type;
    }
}