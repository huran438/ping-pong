using _Client_.Scripts.Components;
using _Client_.Scripts.Services.Skins;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.Core.Runtime;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Systems
{
    public class GameStartSystem : SFECSSystem
    {
        [SFInject]
        private readonly ISkinsService _skinsService;
        
        private readonly EcsFilterInject<Inc<GameStartEvent>> _filter;
        private readonly EcsFilterInject<Inc<Paddle, TransformRef>> _paddleFilter;
        private readonly EcsFilterInject<Inc<Ball, TransformRef, Direction, MeshRendererRef>> _ballFilter;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {

                foreach (var paddleEntity in _paddleFilter.Value)
                {
                    ref var transform = ref _paddleFilter.Pools.Inc2.Get(paddleEntity);
                    transform.value.position = new Vector3(0f, transform.value.position.y);
                }

                foreach (var ballEntity in _ballFilter.Value)
                {
                    ref var transform = ref _ballFilter.Pools.Inc2.Get(ballEntity).value;
                    ref var direction = ref _ballFilter.Pools.Inc3.Get(ballEntity);
                    ref var meshRenderer = ref _ballFilter.Pools.Inc4.Get(ballEntity).value;

                    transform.position = Vector3.zero;
                    
                    var angle = Random.Range(-45f, 45f);
                    var sign = Random.value > 0.5f ? -1 : 1;
                    direction.value = sign * (Quaternion.Euler(0f, 0f, angle) * Vector3.up).normalized;

                    var skin = _skinsService.GetSkinData(_skinsService.ActiveSkin);
                    meshRenderer.sharedMaterial.color = skin.Color;
                }
                
                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}