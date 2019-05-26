using UnityEngine;

namespace aSystem.aUtilsLibrary
{
    public enum AnimatedFloatTypes { Linear, EaseInOut}

    public class AnimatedFloat
    {
        private AnimationCurve _cruve;
        private float _animationTime;
        private float _startTime;
        private float _start;
        private float _target;



        public AnimatedFloat(AnimatedFloatTypes type = AnimatedFloatTypes.EaseInOut, float animationTime = 1f)
        {
            _cruve = GetNormalizedCurve(type);
            _animationTime = animationTime;
        }

        public AnimatedFloat(AnimationCurve normalizedCurve, float animationTime = 1f)
        {
            _cruve = normalizedCurve;
            _animationTime = animationTime;

        }

        public float value
        {
            get
            {
                return Mathf.Lerp(_start, _target, _cruve.Evaluate((Time.time - _startTime)/ _animationTime));
            }

            set
            {
                _start = this.value;
                _startTime = Time.time;
                _target = value;
            }
        }

        public void SetValue(float start, float target, float animationTime)
        {
            _start = start;
            _target = target;
            _startTime = Time.time;
            this._animationTime = animationTime;
        }

        private AnimationCurve GetNormalizedCurve(AnimatedFloatTypes type)
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
