using UnityEngine;

namespace aSystem.aUtilsLibrary.Spring
{
    [System.Serializable]
    public class Vector2Spring : Spring<Vector2>
    {
        public override void Reset()
        {
            value = target;
            velocity = Vector2.zero;
        }

        public override Vector2 Update(float deltaTime)
        {
            Vector2 toTarget = target - value;

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
