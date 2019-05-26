using UnityEngine;

namespace aSystem.aUtilsLibrary
{
    public class AnimatedVector2 : AnimatedValue<Vector2>
    {
        protected override Vector2 GetValue(float t)
        {
            return Vector2.LerpUnclamped(_start, _target, t);
        }
    }
}
