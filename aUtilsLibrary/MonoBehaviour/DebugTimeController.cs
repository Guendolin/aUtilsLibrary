using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTimeController : MonoBehaviour
{
    #if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Time.timeScale = Time.timeScale < 1 ? 1f : 0.2f;
        }
    }
    #endif
}
