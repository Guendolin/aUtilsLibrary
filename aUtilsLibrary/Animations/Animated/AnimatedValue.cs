using UnityEngine;

namespace aSystem.aUtilsLibrary
{
    public enum AnimatedFloatTypes { Linear, EaseInOut}

    public abstract class AnimatedValue<T>
    {
        [System.NonSerialized] protected AnimationCurve _cruve = GetNormalizedCurve(AnimatedFloatTypes.EaseInOut);
        [System.NonSerialized] protected float _animationTime = 1f;
        [System.NonSerialized] protected float _startTime;
        [System.NonSerialized] protected T _start;
        [System.NonSerialized] protected T _target;

        public void SetAnimation(AnimatedFloatTypes type = AnimatedFloatTypes.EaseInOut, float animationTime = 1f)
        {
            _cruve = GetNormalizedCurve(type);
            _animationTime = animationTime;
        }

        public void SetAnimation(AnimationCurve normalizedCurve, float animationTime = 1f)
        {
            _cruve = normalizedCurve;
            _animationTime = animationTime;
        }

        public T value { get { return GetValue(_cruve.Evaluate((Time.time - _startTime) / _animationTime)); } set { SetValue(value); } }
        
        protected abstract T GetValue(float t);
        protected void SetValue(T value)
        {
            _start = this.value;
            _startTime = Time.time;
            _target = value;
        }

        public void SetValue(T start, T target, float animationTime)
        {
            _start = start;
            _target = target;
            _startTime = Time.time;
            this._animationTime = animationTime;
        }

        protected static AnimationCurve GetNormalizedCurve(AnimatedFloatTypes type)
        {
            switch (type)
            {
                case AnimatedFloatTypes.Linear:
                    return AnimationCurve.Linear(0f, 0f, 1f, 1f);
                case AnimatedFloatTypes.EaseInOut:
                default:
                    return AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            }
        }
    }
}
