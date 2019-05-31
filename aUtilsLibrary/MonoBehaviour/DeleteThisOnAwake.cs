using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aSystem.aUtilsLibrary
{
    public class DeleteThisOnAwake : MonoBehaviour
    {
        public void Awake()
        {
            Destroy(gameObject);
        }
    }
}


