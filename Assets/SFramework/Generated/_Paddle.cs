﻿using SFramework.ECS.Runtime;
using Client.Components;
using UnityEngine;

namespace SFramework.Generated
{
#if IL2CPP_OPTIMIZATIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption(Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    [DisallowMultipleComponent, AddComponentMenu("SFComponents/Paddle"), RequireComponent(typeof(SFEntity))]
    public sealed class _Paddle : SFComponent<Paddle> {}
}
