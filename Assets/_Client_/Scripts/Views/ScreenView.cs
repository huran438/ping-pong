using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;
using SFramework.UI.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Views.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ScreenView : SFScreenView
    {
        [SerializeField]
        private float showDuration, closeDuration;

        private Tween<float> _transitionTween;

        protected override void OnShowScreen()
        {
            _transitionTween?.Cancel();
            _transitionTween = CanvasGroup.TweenCanvasGroupAlpha(1f, showDuration).SetOnComplete(() =>
            {
                CanvasGroup.interactable = true;
                CanvasGroup.blocksRaycasts = true;
                ScreenShownCallback();
            }).SetUseUnscaledTime(true);
        }

        protected override void OnCloseScreen()
        {
            _transitionTween?.Cancel();
            _transitionTween = CanvasGroup.TweenCanvasGroupAlpha(0f, closeDuration).SetOnStart(() =>
            {
                CanvasGroup.interactable = false;
                CanvasGroup.blocksRaycasts = false;
            }).SetOnComplete(ScreenClosedCallback).SetUseUnscaledTime(true);
        }

        protected override void OnScreenShown()
        {
        }

        protected override void OnScreenClosed()
        {
        }
    }
}