using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace Client.Systems
{
    public class BallMovementSystem : SFECSSystem
    {
        private readonly EcsFilterInject<Inc<Ball, Speed, Direction ,TransformRef>> _filter;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var speed = ref _filter.Pools.Inc2.Get(entity);
                ref var direction = ref _filter.Pools.Inc3.Get(entity);
                ref var transformRef = ref _filter.Pools.Inc4.Get(entity);
                transformRef.value.position += direction.value * speed.value * Time.deltaTime;
            }
        }
    }
}