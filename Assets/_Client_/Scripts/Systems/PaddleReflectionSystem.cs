using _Client_.Scripts.Components;
using _Client_.Scripts.Enums;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using SFramework.ECS.Runtime.Tween;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class PaddleReflectionSystem : SFECSSystem
    {
        private readonly EcsFilterInject<Inc<Ball, Direction, TransformRef>> _filter;
        private readonly EcsFilterInject<Inc<Paddle, TransformRef>> _paddleFilter;
        private readonly EcsPoolInject<InputSpeed> _inputSpeedPool;
        private readonly EcsPoolInject<SpeedTween> _speedTweenPool;
        private readonly EcsPoolInject<Speed> _speedPool;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var ballEntity in _filter.Value)
            {
                ref var ballDirection = ref _filter.Pools.Inc2.Get(ballEntity);
                ref var ballTransform = ref _filter.Pools.Inc3.Get(ballEntity).value;

                if (ballDirection.value == Vector3.zero) continue;

                foreach (var paddleEntity in _paddleFilter.Value)
                {
                    ref var paddle = ref _paddleFilter.Pools.Inc1.Get(paddleEntity);
                    ref var paddleTransform = ref _paddleFilter.Pools.Inc2.Get(paddleEntity).value;

                    var reflected = false;

                    if (ballDirection.value.y < 0f && paddle.type == PaddleType.Bottom)
                    {
                        if (ballTransform.position.y > paddleTransform.position.y)
                        {
                            if (ballTransform.position.y - ballTransform.localScale.x / 2f <=
                                paddleTransform.position.y + paddleTransform.localScale.y / 2f &&
                                ballTransform.position.x >
                                paddleTransform.position.x - paddleTransform.localScale.x / 2f &&
                                ballTransform.position.x <
                                paddleTransform.position.x + paddleTransform.localScale.x / 2f)
                            {
                                var reflect = Vector3.Reflect(ballDirection.value, Vector3.up);
                                var maxAngle = Random.Range(-5, 5f);
                                reflect = Quaternion.Euler(0, 0, maxAngle) * reflect;
                                ballDirection.value = reflect;
                                reflected = true;
                            }
                        }
                    }

                    if (ballDirection.value.y > 0f && paddle.type == PaddleType.Top)
                    {
                        if (ballTransform.position.y < paddleTransform.position.y)
                        {
                            if (ballTransform.position.y + ballTransform.localScale.x / 2f >=
                                paddleTransform.position.y - paddleTransform.localScale.y / 2f &&
                                ballTransform.position.x >
                                paddleTransform.position.x - paddleTransform.localScale.x / 2f &&
                                ballTransform.position.x <
                                paddleTransform.position.x + paddleTransform.localScale.x / 2f)
                            {
                                var reflect = Vector3.Reflect(ballDirection.value, Vector3.down);
                                var maxAngle = Random.Range(-5, 5f);
                                reflect = Quaternion.Euler(0, 0, maxAngle) * reflect;
                                ballDirection.value = reflect;
                                reflected = true;
                            }
                        }
                    }

                    if (reflected)
                    {
                        if (!_speedTweenPool.Value.Has(ballEntity))
                        {
                            if (_inputSpeedPool.Value.Has(paddleEntity))
                            {
                                ref var paddleInputSpeed = ref _inputSpeedPool.Value.Get(paddleEntity);

                                if (_speedPool.Value.Has(ballEntity))
                                {
                                    ref var ballSpeed = ref _speedPool.Value.Get(ballEntity);

                                    _speedTweenPool.Value.Add(ballEntity) = new SpeedTween
                                    {
                                        delay = 0,
                                        duration = 0.5f,
                                        loopType = TweenLoopType.None,
                                        animationCurve = TweenAnimationCurve.EaseInOut,
                                        startValue = ballSpeed.value *
                                                     Mathf.Clamp(paddleInputSpeed.value / 1000f, 1f, 3f),
                                        endValue = ballSpeed.value
                                    };
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}