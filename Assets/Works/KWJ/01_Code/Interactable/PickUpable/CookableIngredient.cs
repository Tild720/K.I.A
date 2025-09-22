
using System;
using UnityEngine;

namespace KWJ.Interactable.PickUpable
{
    public enum CookingType
    {
        None = -1,
        
        Boilable, //삶기
        Bakeable, //굽기
        Heatable, //데우기
        
        Max,
    }
    public enum CookingState
    {
        None = -1,
        
        Insufficient, //부족함
        Moderate, //적당함
        Excessive, //과함
        
        Max,
    }
    public class CookableIngredient : PickUpableObject
    {
        public CookingType CookingType => cookingCookingType;
        [SerializeField] private CookingType cookingCookingType;
        public CookingState CookingState => cookingState;
        [Space]
        [SerializeField] private CookingState cookingState;
        [SerializeField] [Range(0, 1)] private float doneness01;
        [SerializeField] [Range(0, 1)] private float donenessModerate;
        [SerializeField] [Range(0, 1)] private float donenessExcessive;

        private void OnValidate()
        {
            if (donenessModerate >= donenessExcessive)
            {
                donenessModerate = donenessExcessive - 0.1f;
            }
        }

        public void SetCookingState(CookingState state) => cookingState = state;

        public void CookingTimer(float time)
        {
            doneness01 += time * 0.2f;
            
            if (doneness01 >= donenessModerate && cookingState == CookingState.Insufficient)
                cookingState = CookingState.Moderate;
            else if (doneness01 >= donenessExcessive && cookingState == CookingState.Moderate)
                cookingState = CookingState.Excessive;
        }
    }
}