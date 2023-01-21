using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace Client.Systems
{
    public class PaddleDesktopControlSystem : SFECSSystem
    {
        private readonly EcsFilterInject<Inc<Paddle, Direction, SmoothSpeed, TransformRef>, Exc<AI>> _filter;

        protected override void Tick(ref IEcsSystems systems)
        {
            if (Application.isMobilePlatform) return;
            
            foreach (var entity in _filter.Value)
            {
                ref var direction = ref _filter.Pools.Inc2.Get(entity);
                ref var smoothSpeed = ref _filter.Pools.Inc3.Get(entity);
                ref var transform = ref _filter.Pools.Inc4.Get(entity).value;

                if (Input.GetKeyDown(KeyCode.A))
                {
                    smoothSpeed.startTime = Time.time;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    smoothSpeed.startTime = Time.time;
                }

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
                
                var elapsedTime = Time.time - smoothSpeed.startTime;
                var t = Mathf.Clamp(elapsedTime / smoothSpeed.duration, 0.0f, 1.0f);
                var speed = Mathf.Lerp(smoothSpeed.minValue, smoothSpeed.maxValue, t);
                transform.position += direction.value * (speed * Time.deltaTime);
            }
        }
    }
}