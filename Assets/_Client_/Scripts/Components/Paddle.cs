using System;
using _Client_.Scripts.Enums;
using SFramework.ECS.Runtime;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct Paddle : ISFComponent
    {
        public PaddleType type;
    }
}