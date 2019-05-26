﻿using UnityEngine;

namespace aSystem.aUtilsLibrary.Spring
{
    [System.Serializable]
    public abstract class Spring<T>
    {
        public float strenght = 100f;
        public float drag = 10f;
        public float maxSpeed = 0f;

        [System.NonSerialized] public T velocity;
        [System.NonSerialized] public T value;
        [System.NonSerialized] public T target;

        public void SetTarget(T target)
        {
            this.target = target;
        }

        public abstract T Update(float deltaTime);
        public abstract void Reset();
    }
}