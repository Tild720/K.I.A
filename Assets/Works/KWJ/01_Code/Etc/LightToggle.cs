using System;
using UnityEngine;

namespace KWJ.Etc
{
    public class LightToggle : MonoBehaviour
    {
        [SerializeField] private Light light;
        
        public bool isOnLight;

        private void Start()
        {
            light.enabled = isOnLight;
        }

        public void ToggleLight()
        {
            isOnLight = !isOnLight;
            light.enabled = isOnLight;
        }
    }
}