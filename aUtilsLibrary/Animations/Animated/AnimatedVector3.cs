using UnityEngine;

namespace aSystem.aUtilsLibrary
{
    public class AnimatedVector3 : AnimatedValue<Vector3>
    {
        protected override Vector3 GetValue(float t)
        {
            return Vector3.LerpUnclamped(_start, _target, t);
        }
    }
}
