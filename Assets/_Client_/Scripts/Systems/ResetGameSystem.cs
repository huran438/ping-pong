using Client.Components;
using Client.Models;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.Core.Runtime;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace Client.Systems
{
    public class ResetGameSystem : SFECSSystem
    {
        [SFInject]
        private readonly SessionModel _sessionModel;

        private readonly EcsFilterInject<Inc<Ball, TransformRef, Direction>> _filter;
        private readonly EcsFilterInject<Inc<Paddle, TransformRef>> _paddleFilter;


        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transformRef = ref _filter.Pools.Inc2.Get(entity);
                ref var direction = ref _filter.Pools.Inc3.Get(entity);

                var viewPos = Camera.main.WorldToViewportPoint(transformRef.value.position);

                if (viewPos.y < 0f)
                {
                    _sessionModel.PlayerTopScore += 1;
                    transformRef.value.position = Vector3.zero;
                    direction.value = Random.insideUnitCircle;

                    var angle = Random.Range(-45f, 45f);
                    direction.value = (Quaternion.Euler(0f, 0f, angle) * Vector3.up).normalized;
                }
                else if (viewPos.y > 1f)
                {
                    _sessionModel.PlayerBottomScore += 1;
                    transformRef.value.position = Vector3.zero;
                    var angle = Random.Range(-45f, 45f);
                    direction.value = -(Quaternion.Euler(0f, 0f, angle) * Vector3.up).normalized;
                }
            }
        }
    }
}