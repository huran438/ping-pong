using Client.Components;
using Client.Enum;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace Client.Systems
{
    public class PaddleReflectionSystem : SFECSSystem
    {
        private readonly EcsFilterInject<Inc<Ball, Direction, TransformRef>> _filter;
        private readonly EcsFilterInject<Inc<Paddle, TransformRef>> _racketFilter;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var ballEntity in _filter.Value)
            {
                ref var ballDirection = ref _filter.Pools.Inc2.Get(ballEntity);
                ref var ballTransform = ref _filter.Pools.Inc3.Get(ballEntity).value;
                
                if (ballDirection.value == Vector3.zero) continue;

                foreach (var racketEntity in _racketFilter.Value)
                {
                    ref var racket = ref _racketFilter.Pools.Inc1.Get(racketEntity);
                    ref var racketTransform = ref _racketFilter.Pools.Inc2.Get(racketEntity).value;

                    if (ballDirection.value.y < 0f && racket.type == PaddleType.Bottom)
                    {
                        if (ballTransform.position.y > racketTransform.position.y)
                        {
                            if (ballTransform.position.y - ballTransform.localScale.x / 2f <=
                                racketTransform.position.y + racketTransform.localScale.y / 2f &&
                                ballTransform.position.x > racketTransform.position.x - racketTransform.localScale.x / 2f &&
                                ballTransform.position.x < racketTransform.position.x + racketTransform.localScale.x / 2f)
                            {
                                var reflect = Vector3.Reflect(ballDirection.value, Vector3.up);
                                var maxAngle = Random.Range(-5, 5f);
                                reflect = Quaternion.Euler(0, 0, maxAngle) * reflect;
                                ballDirection.value = reflect;
                            } 
                        }

                       
                    }

                    if (ballDirection.value.y > 0f && racket.type == PaddleType.Top)
                    {
                        if (ballTransform.position.y < racketTransform.position.y)
                        {
                            if (ballTransform.position.y + ballTransform.localScale.x / 2f >=
                                racketTransform.position.y - racketTransform.localScale.y / 2f &&
                                ballTransform.position.x > racketTransform.position.x - racketTransform.localScale.x / 2f &&
                                ballTransform.position.x < racketTransform.position.x + racketTransform.localScale.x / 2f)
                            {
                                var reflect = Vector3.Reflect(ballDirection.value, Vector3.down);
                                var maxAngle = Random.Range(-5, 5f);
                                reflect = Quaternion.Euler(0, 0, maxAngle) * reflect;
                                ballDirection.value = reflect;
                            }
                        }
                    }
                }
            }
        }
    }
}