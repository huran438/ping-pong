using Client.Components;
using Client.Enum;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using SFramework.ECS.Runtime.Tween;
using UnityEngine;

namespace Client.Systems
{
    public class PaddleAIControlSystem : SFECSSystem
    {
        private readonly EcsFilterInject<Inc<Paddle, TransformRef, Direction, Speed, AI>> _filter;
        private readonly EcsFilterInject<Inc<Ball, TransformRef, Direction>> _ballsFilter;
        private readonly EcsPoolInject<SpeedTween> _speedTweenPool;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var paddle = ref _filter.Pools.Inc1.Get(entity);
                ref var paddleTransform = ref _filter.Pools.Inc2.Get(entity).value;
                ref var paddleDirection = ref _filter.Pools.Inc3.Get(entity);
                ref var speed = ref _filter.Pools.Inc4.Get(entity);

                foreach (var ballEntity in _ballsFilter.Value)
                {
                    ref var ballTransform = ref _ballsFilter.Pools.Inc2.Get(ballEntity).value;
                    ref var ballDirection = ref _ballsFilter.Pools.Inc3.Get(ballEntity);

                    if (paddle.type == PaddleType.Top)
                    {
                        if (ballDirection.value.y > 0)
                        {
                            if (ballTransform.position.x < paddleTransform.position.x)
                            {
                                paddleDirection.value = Vector3.left;
                            }
                            else
                            {
                                paddleDirection.value = Vector3.right;
                            }
                        }
                    }

                    if (paddle.type == PaddleType.Bottom)
                    {
                        if (ballDirection.value.y < 0)
                        {
                            if (ballTransform.position.x < paddleTransform.position.x)
                            {
                                paddleDirection.value = Vector3.left;
                            }
                            else
                            {
                                paddleDirection.value = Vector3.right;
                            }
                        }
                    }

                    if (!_speedTweenPool.Value.Has(entity))
                    {
                        _speedTweenPool.Value.Add(entity) = new SpeedTween
                        {
                            delay = 0,
                            duration = Random.Range(0f, 1f),
                            loopType = TweenLoopType.None,
                            animationCurve = TweenAnimationCurve.EaseInOut,
                            startValue = 0f,
                            endValue = speed.value
                        };
                    }

                    paddleTransform.position += paddleDirection.value * (speed._value / 2f * Time.deltaTime);
                    
                   
                }
            }
        }
    }
}