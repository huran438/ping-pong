using System;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Components
{
    [Serializable, SFGenerateComponent]
    public struct MeshRendererRef : ISFComponent
    {
        public MeshRenderer value;
    }
}