using System;
using UnityEngine;

namespace Works.JW.Code
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(instance.gameObject);
        }

        public void SetCursor(bool isVis)
        {
            Cursor.visible = isVis;
            Cursor.lockState = isVis ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}