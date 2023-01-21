using Client.Components;
using Client.Models;
using Client.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using SFramework.Core.Runtime;
using SFramework.ECS.Runtime;
using UnityEngine;

namespace Client
{
    public sealed class _ContextRoot : SFContextRoot
    {
        private IEcsSystems _systems;

        protected override void PreInit()
        {
            Input.multiTouchEnabled = true;
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        protected override void Setup(SFContainer container)
        {
            container.Bind<ISFWorldsService>(new SFWorldsService());
            container.Bind<SessionModel>(new SessionModel());
        }

        protected override void Init(ISFContainer container)
        {
            var _world = container.Resolve<ISFWorldsService>().Default;
            _systems = new EcsSystems(_world, container);
            _systems
                .Add(new WallsResizeSystem())
                .Add(new PaddleAIControlSystem())
                .Add(new PaddleDesktopControlSystem())
                .Add(new PaddleMobileControlSystem())
                .Add(new PaddleCollisionSystem())
                .Add(new PaddleReflectionSystem())
                .Add(new WallReflectionSystem())
                .Add(new BallMovementSystem())
                .Add(new ResetGameSystem())

#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject()
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }
        }
    }
}