using System;
using UnityEngine;

namespace Works.Tild.Code
{
    public class TrustManager : MonoBehaviour
    {
        public int Trust { get; set; }
        public static TrustManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            Trust = 70;
        }

        public void RemoveTrust(int percentage)
        {
            Trust -= percentage;
            if (Trust <= 0)
            {
                Debug.Log("신뢰도 바닥.");
            }
        }
        

        public void AddTrust(int percentage)
        {
            Trust += Mathf.Max(Trust + percentage, 100);;
            
        }
        
    }
}