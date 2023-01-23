using _Client_.Scripts.Components;
using _Client_.Scripts.Models;
using _Client_.Scripts.Services.Skins;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.Core.Runtime;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class GameStopSystem : SFECSSystem
    {
        [SFInject]
        private readonly SessionModel _sessionModel;

        private readonly EcsFilterInject<Inc<GameStopEvent>> _filter;
        private readonly EcsFilterInject<Inc<Paddle, TransformRef>> _paddleFilter;
        private readonly EcsFilterInject<Inc<Ball, TransformRef, Direction, MeshRendererRef>> _ballFilter;

        private EcsPoolInject<Direction> _directionPool;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                foreach (var paddleEntity in _paddleFilter.Value)
                {
                    if (_directionPool.Value.Has(paddleEntity))
                    {
                        _directionPool.Value.Get(paddleEntity).value = Vector3.zero;
                    }

                    ref var transform = ref _paddleFilter.Pools.Inc2.Get(paddleEntity);
                    transform.value.position = new Vector3(0f, transform.value.position.y);
                }

                foreach (var ballEntity in _ballFilter.Value)
                {
                    ref var transform = ref _ballFilter.Pools.Inc2.Get(ballEntity).value;
                    ref var direction = ref _ballFilter.Pools.Inc3.Get(ballEntity);

                    transform.position = Vector3.zero;
                    direction.value = Vector3.zero;
                }

                _sessionModel.Reset();

                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}