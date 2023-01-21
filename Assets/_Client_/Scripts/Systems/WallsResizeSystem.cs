using Client.Components;
using Client.Enum;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace Client.Systems
{
    public class WallsResizeSystem : SFECSSystem
    {
        private readonly EcsFilterInject<Inc<Wall, TransformRef>> _wallFilter;

        protected override void Tick(ref IEcsSystems systems)
        {
            foreach (var wallEntity in _wallFilter.Value)
            {
                ref var wall = ref _wallFilter.Pools.Inc1.Get(wallEntity);
                ref var wallTransform = ref _wallFilter.Pools.Inc2.Get(wallEntity).value;

                float cameraHeight = Camera.main.orthographicSize * 2;
                float cameraWidth = cameraHeight * Camera.main.aspect;

                if (!Mathf.Approximately(wallTransform.localScale.y, cameraHeight))
                {
                    wallTransform.localScale = new Vector3(wallTransform.localScale.x, cameraHeight,
                        wallTransform.localScale.z);
                }
                
                if (wall.type == WallType.Left)
                {
                    if (!Mathf.Approximately(wallTransform.position.x, -cameraWidth / 2f))
                    {
                        wallTransform.position = new Vector3(-cameraWidth / 2f + wallTransform.localScale.x / 2f, 0f, 0f);
                    }
                }
                
                if (wall.type == WallType.Right)
                {
                    if (!Mathf.Approximately(wallTransform.position.x, cameraWidth / 2f))
                    {
                        wallTransform.position = new Vector3(cameraWidth / 2f - wallTransform.localScale.x / 2f, 0f, 0f);
                    }
                }
            }
        }
    }
}