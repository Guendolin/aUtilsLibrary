using UnityEngine;

namespace aSystem.aUtilsLibrary
{
    public class AnimatedFloat : AnimatedValue<float>
    {
        protected override float GetValue(float t)
        {
            return Mathf.LerpUnclamped(_start, _target, t);
        }
    }
}
