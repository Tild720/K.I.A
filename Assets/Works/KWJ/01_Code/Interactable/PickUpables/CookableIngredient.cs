using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KWJ.UI;

namespace KWJ.Interactable.PickUpable
{
    [Flags]
    public enum CookingType
    {
        None = 0,
        
        Boilable = 1 << 0, //삶기
        Bakeable = 1 << 1, //굽기
        Heatable = 1 << 2, //데우기
        
        Max = 1 << 3,
    }
    public enum CookingState
    {
        None = -1,
        
        Insufficient, //부족함
        Moderate, //적당함
        Excessive, //과함
        
        Max,
    }
    public class CookableIngredient : PickUpable
    {
        public CookingType CookingType => cookingType;
        [SerializeField] private CookingType cookingType;
        public CookingState CookingState => cookingState;
        [Space]
        [SerializeField] private CookingState cookingState;
        [SerializeField] [Range(0, 1)] private float doneness01;
        [SerializeField] [Range(0, 1)] private float donenessModerate;
        [SerializeField] [Range(0, 1)] private float donenessExcessive;

        public float CookingTime => cookingTime;
        [SerializeField] private float cookingTime;
        [Space]
        [ColorUsage(false, false)] [SerializeField] private Color _cookingColor;
        [Space]
        [SerializeField] private TimerFill timerFill;

        private float _remainingCookingTime;
        
        private List<Material> _material;

        private void OnValidate()
        {
            if (donenessModerate >= donenessExcessive)
            {
                donenessModerate = donenessExcessive - 0.1f;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            _material = new List<Material>();
            _material = GetComponentInChildren<MeshRenderer>().materials.ToList();
            
            cookingState = CookingState.Insufficient;
            doneness01 = 0f;
            _remainingCookingTime = cookingTime;
            
            timerFill.gameObject.SetActive(false);
        }
        public void SetCookingState(CookingState state) => cookingState = state;

        public void CookingTimer(float time)
        {
            if (doneness01 >= 1) return;

            if (doneness01 == 0)
                timerFill.SetCookFills(donenessModerate, donenessExcessive);
            
            if(timerFill.gameObject.activeSelf == false)
                timerFill.gameObject.SetActive(true);
            
            doneness01 += time / cookingTime;
            _remainingCookingTime -= time;
            
            timerFill.SetCookFill(doneness01, _remainingCookingTime);
            
            ChangeCookingColor(time / cookingTime);

            if (doneness01 >= donenessModerate && cookingState == CookingState.Insufficient)
            {
                cookingState = CookingState.Moderate;
            }
            else if (doneness01 >= donenessExcessive && cookingState == CookingState.Moderate)
            {
                cookingState = CookingState.Excessive;
            }
        }

        private void ChangeCookingColor(float time)
        {
            foreach (var material in _material)
            {
                material.color = Color.Lerp(material.color, _cookingColor, time);
            }
        }
    }
}