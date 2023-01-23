using SFramework.ECS.Runtime;
using SFramework.ECS.Runtime.Tween;

namespace _Client_.Scripts.Components
{
    
    public struct SpeedTween : ISFComponent
    {
        public float delay;
        public float duration;
        public TweenLoopType loopType;
        public TweenAnimationCurve animationCurve;
        public float startValue;
        public float endValue;
        public bool unscaledTime;
        public float _elapsedTime;
    }
}