using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using SFramework.ECS.Runtime.Tween;
using UnityEngine;

namespace _Client_.Scripts._ECS.Systems
{
    public class SpeedTweenSystem : SFECSSystem
    {
        private readonly EcsFilterInject<Inc<Speed, SpeedTween>> _filter;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var speed = ref _filter.Pools.Inc1.Get(entity);
                ref var to = ref _filter.Pools.Inc2.Get(entity);

                to._elapsedTime += to.unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

                var t = Mathf.Clamp((to._elapsedTime - to.delay) / to.duration, 0.0f, 1.0f);

                speed.value = SFMathFXHelper.CurvedValueECS(to.animationCurve,
                    to.startValue,
                    to.endValue, t);

                if (to._elapsedTime >= to.delay + to.duration)
                {
                    switch (to.loopType)
                    {
                        case TweenLoopType.None:
                            speed.value = to.endValue;
                            _filter.Pools.Inc2.Del(entity);
                            break;
                        case TweenLoopType.Repeat:
                            to._elapsedTime -= to.duration;
                            break;
                        case TweenLoopType.Continuous:
                            var next = to.endValue - to.startValue;
                            to.startValue = to.endValue;
                            to.endValue += next;
                            to._elapsedTime = 0f;
                            break;
                        case TweenLoopType.YoYo:
                            (to.startValue, to.endValue) = (to.endValue, to.startValue);
                            to._elapsedTime = 0f;
                            break;
                    }
                }
            }
        }
    }
}