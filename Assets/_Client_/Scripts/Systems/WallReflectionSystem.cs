using Client.Components;
using Client.Enum;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace Client.Systems
{
    public class WallReflectionSystem : SFECSSystem
    {
        private readonly EcsFilterInject<Inc<Ball, Direction, TransformRef>> _ballFilter;
        private readonly EcsFilterInject<Inc<Wall, TransformRef>> _wallFilter;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var ballEntity in _ballFilter.Value)
            {
                ref var ballDirection = ref _ballFilter.Pools.Inc2.Get(ballEntity);
                ref var ballTransform = ref _ballFilter.Pools.Inc3.Get(ballEntity).value;

                if (ballDirection.value == Vector3.zero) continue;

                foreach (var wallEntity in _wallFilter.Value)
                {
                    ref var wall = ref _wallFilter.Pools.Inc1.Get(wallEntity);
                    ref var wallTransform = ref _wallFilter.Pools.Inc2.Get(wallEntity).value;

                    if (ballDirection.value.x < 0 && wall.type == WallType.Left)
                    {
                        if (ballTransform.position.x - ballTransform.localScale.x / 2f <=
                            wallTransform.position.x + wallTransform.localScale.x / 2f)
                        {
                            ballDirection.value = Vector3.Reflect(ballDirection.value, Vector3.right);
                        }
                    }

                    if (ballDirection.value.x > 0 && wall.type == WallType.Right)
                    {
                        if (ballTransform.position.x + ballTransform.localScale.x / 2f >=
                            wallTransform.position.x - wallTransform.localScale.x / 2f)
                        {
                            ballDirection.value = Vector3.Reflect(ballDirection.value, Vector3.left);
                        }
                    }
                }
            }
        }
    }
}