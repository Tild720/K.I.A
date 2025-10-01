using System;
using TMPro;
using UnityEngine;

namespace Works.Tild.Code
{
    public class TrustManager : MonoBehaviour
    {
        public int Trust { get; set; }
        public static TrustManager Instance { get; private set; }
        [SerializeField] private TMP_Text currency;

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
            currency.text = $"신뢰도 {Trust}%";
        }

        public void RemoveTrust(int percentage)
        {
        
            Trust -= percentage;
            currency.text = $"신뢰도 {Trust}%";
            if (Trust > 30)
            {
                currency.color = Color.red;
            }
            else if (Trust < 30)
            {
                currency.color = Color.green;
            }
            if (Trust <= 0)
            {
                Debug.Log("신뢰도 바닥.");
            }
        }
        

        public void AddTrust(int percentage)
        {
            
            Trust = Mathf.Min(Trust + percentage, 100);;
            currency.text = $"신뢰도 {Trust}%";
        }
        
    }
}