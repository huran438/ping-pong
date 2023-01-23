using _Client_.Scripts.Components;
using _Client_.Scripts.Enums;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class PaddleCollisionSystem : SFECSSystem
    {
        private readonly EcsFilterInject<Inc<Paddle, TransformRef>> _paddleFilter;
        private readonly EcsFilterInject<Inc<Wall, TransformRef>> _wallFilter;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var paddleEntity in _paddleFilter.Value)
            {
                ref var paddleTransform = ref _paddleFilter.Pools.Inc2.Get(paddleEntity).value;

                foreach (var wallEntity in _wallFilter.Value)
                {
                    ref var wall = ref _wallFilter.Pools.Inc1.Get(wallEntity);
                    ref var wallTransform = ref _wallFilter.Pools.Inc2.Get(wallEntity).value;

                    if (wall.type == WallType.Left)
                    {
                        if (paddleTransform.position.x - paddleTransform.localScale.x / 2f <=
                            wallTransform.position.x + wallTransform.localScale.x / 2f)
                        {
                            paddleTransform.position =
                                new Vector3(
                                    wallTransform.position.x + wallTransform.localScale.x / 2f +
                                    paddleTransform.localScale.x / 2f + 0.1f,
                                    paddleTransform.position.y);
                        }
                    }

                    if (wall.type == WallType.Right)
                    {
                        if (paddleTransform.position.x + paddleTransform.localScale.x / 2f >=
                            wallTransform.position.x - wallTransform.localScale.x / 2f)
                        {
                            paddleTransform.position =
                                new Vector3(
                                    wallTransform.position.x - wallTransform.localScale.x / 2f -
                                    paddleTransform.localScale.x / 2f - 0.1f,
                                    paddleTransform.position.y);
                        }
                    }
                }
            }
        }
    }
}