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

                direction.value = Vector3.zero;

                if (Input.touchCount == 0) continue;
                var firstTouch = Input.GetTouch(0);
                var firstWorldPosition = Camera.main.ScreenToWorldPoint(firstTouch.position);
                if (firstWorldPosition.y < 0 && paddle.type == PaddleType.Bottom)
                {
                    transform.position = new Vector3(firstWorldPosition.x, transform.position.y);
                }


                if (Input.touchCount == 1) continue;
                var secondTouch = Input.GetTouch(1);
                var secondWorldPosition = Camera.main.ScreenToWorldPoint(secondTouch.position);
                if (secondWorldPosition.y > 0 && paddle.type == PaddleType.Top)
                {
                    transform.position = new Vector3(secondWorldPosition.x, transform.position.y);
                }
            }
        }
    }
}