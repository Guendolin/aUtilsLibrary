using UnityEngine;

namespace aSystem.aUtilsLibrary
{
    [System.Serializable]
    public class FloatSpring : Spring<float>
    {
        public override void Reset()
        {
            value = target;
            velocity = 0f;
        }

        public override float Update(float deltaTime)
        {
            float toTarget = target - value;

            velocity = velocity * (1 - Mathf.Min(1.0f, Time.deltaTime * drag));
            velocity = velocity + toTarget * (strenght * deltaTime);

            if (maxSpeed > 0)
            {
                velocity = Mathf.Clamp(velocity, -maxSpeed, maxSpeed);
            }

            value = value + velocity * deltaTime;

            return value;
        }
    }
}
