using System;
using UnityEngine;

public class MouseLock : MonoBehaviour
{ 
    void LateUpdate()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
