using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace Client.Systems
{
    public class PaddleDesktopControlSystem : SFECSSystem
    {
        private readonly EcsFilterInject<Inc<Paddle, Direction, TransformRef, Speed>, Exc<AI>> _filter;

        protected override void Tick(ref IEcsSystems systems)
        {
            if (Application.isMobilePlatform) return;

            foreach (var entity in _filter.Value)
            {
                ref var direction = ref _filter.Pools.Inc2.Get(entity);
                ref var paddleTransform = ref _filter.Pools.Inc3.Get(entity).value;
                ref var speed = ref _filter.Pools.Inc4.Get(entity);

                if (Input.GetKey(KeyCode.A))
                {
                    direction.value = Vector3.left;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    direction.value = Vector3.right;
                }
                else
                {
                    direction.value = Vector3.zero;
                }

                paddleTransform.position += direction.value * (speed._value * Time.deltaTime);
            }
        }
    }
}