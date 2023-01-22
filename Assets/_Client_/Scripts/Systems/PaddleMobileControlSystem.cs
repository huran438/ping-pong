using Client.Components;
using Client.Enum;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace Client.Systems
{
    public class PaddleMobileControlSystem : SFECSSystem
    {
        private readonly EcsFilterInject<Inc<Paddle, Direction, TransformRef>, Exc<AI>> _filter;

        protected override void Tick(ref IEcsSystems systems)
        {
            if (!Application.isMobilePlatform) return;

            foreach (var entity in _filter.Value)
            {
                ref var paddle = ref _filter.Pools.Inc1.Get(entity);
                ref var direction = ref _filter.Pools.Inc2.Get(entity);
                ref var transform = ref _filter.Pools.Inc3.Get(entity).value;

                if (Input.touchCount == 0) return;

                direction.value = Vector3.zero;
                
                foreach (var touch in Input.touches)
                {
                    var worldPosition = Camera.main.ScreenToWorldPoint(touch.position);

                    if (worldPosition.y < 0 && paddle.type == PaddleType.Bottom)
                    {
                        transform.position = new Vector3(worldPosition.x, transform.position.y);
                    }
                    else if (worldPosition.y > 0 && paddle.type == PaddleType.Top)
                    {
                        transform.position = new Vector3(worldPosition.x, transform.position.y);
                    }
                }
            }
        }
    }
}