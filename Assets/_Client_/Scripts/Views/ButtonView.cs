using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;
using SFramework.UI.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Client_.Scripts.Views.UI
{
    public class ButtonView : SFWidgetView
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private Color _normalColor = Color.white, _pressedColor = Color.white;

        [SerializeField]
        private float _colorSwitchDuration = 0.1f;

        private Tween<Color> _colorTween;

        protected override void PointerDown(PointerEventData eventData)
        {
            _colorTween?.Cancel();
            _colorTween = _image.TweenGraphicColor(_pressedColor, _colorSwitchDuration).SetUseUnscaledTime(true);
        }

        protected override void PointerUp(PointerEventData eventData)
        {
            _colorTween?.Cancel();
            _colorTween = _image.TweenGraphicColor(_normalColor, _colorSwitchDuration).SetUseUnscaledTime(true);
        }

        protected override void PointerClick(PointerEventData eventData)
        {
            Debug.Log($"Button: {Widget} Clicked!\n" + eventData);
        }
    }
}