using System;
using UnityEngine;

namespace Works.Tild.Code
{
    [CreateAssetMenu(fileName = "TrustSO", menuName = "SO/Trust")]
    public class TrustSO : ScriptableObject
    {
        public int Trust { get; set; }

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
            Trust += percentage;
        }
        
    }
}