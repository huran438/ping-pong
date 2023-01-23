using _Client_.Scripts.Components;
using _Client_.Scripts.Models;
using Leopotam.EcsLite;
using SFramework.Core.Runtime;
using SFramework.ECS.Runtime;
using SFramework.UI.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace _Client_.Scripts.Views
{
    public class ApplicationBehaviour : SFView
    {
        [SFInject]
        private readonly ISFUIService _uiService;

        [SFInject]
        private readonly ISFWorldsService _worldsService;

        [SFInject]
        private readonly SessionModel _sessionModel;

        [SFScreen]
        [SerializeField]
        private string _mainScreen, _gameScreen, _pauseScreen;

        [SFWidget]
        [SerializeField]
        private string _mainScreenPlayButton, _gamePauseButton, _pauseResumeButton, _pauseHomeButton;

        private EcsPool<GameStartEvent> _gameStartEventPool;
        private EcsPool<GameStopEvent> _gameStopEventPool;
        
        protected override void Init()
        {
            _gameStartEventPool = _worldsService.Default.GetPool<GameStartEvent>();
            _gameStopEventPool = _worldsService.Default.GetPool<GameStopEvent>();
            _uiService.OnWidgetPointerEvent += OnWidgetPointerEvent;
        }

        private void OnWidgetPointerEvent(string widget, SFPointerEventType eventType, PointerEventData eventData)
        {
            if (eventType == SFPointerEventType.Click)
            {
                if (widget == _mainScreenPlayButton)
                {
                    _uiService.CloseScreen(_mainScreen);
                    _uiService.ShowScreen(_gameScreen);
                    _gameStartEventPool.Add(_worldsService.Default.NewEntity()) = new GameStartEvent();
                }

                if (widget == _gamePauseButton)
                {
                    _uiService.CloseScreen(_gameScreen);
                    _uiService.ShowScreen(_pauseScreen);
                    Time.timeScale = 0;
                }

                if (widget == _pauseResumeButton)
                {
                    _uiService.ShowScreen(_gameScreen);
                    _uiService.CloseScreen(_pauseScreen);
                    Time.timeScale = 1f;
                }

                if (widget == _pauseHomeButton)
                {
                    _gameStopEventPool.Add(_worldsService.Default.NewEntity()) = new GameStopEvent();
                    Time.timeScale = 1f;
                    _uiService.CloseScreen(_pauseScreen);
                    _uiService.ShowScreen(_mainScreen);
                }
            }
        }

        protected void OnDestroy()
        {
            _uiService.OnWidgetPointerEvent -= OnWidgetPointerEvent;
        }
    }
}