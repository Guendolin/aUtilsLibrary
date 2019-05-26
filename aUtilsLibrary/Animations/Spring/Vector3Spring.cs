using UnityEngine;

namespace aSystem.aUtilsLibrary.Spring
{
    [System.Serializable]
    public class Vector3Spring : Spring<Vector3>
    {
        public override void Reset()
        {
            value = target;
            velocity = Vector3.zero;
        }

        public override Vector3 Update(float deltaTime)
        {
            Vector3 toTarget = target - value;

            velocity = velocity * (1 - Mathf.Min(1.0f, Time.deltaTime * drag));
            velocity = velocity + toTarget * (strenght * deltaTime);

            if (maxSpeed > 0)
            {
                velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
            }

            value = value + velocity * deltaTime;

            return value;
        }
    }
}
