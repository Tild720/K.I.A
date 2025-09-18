
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
        [SerializeField] private CookingType cookingType;
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

        private void Update()
        {
            if (doneness01 >= donenessModerate)
                cookingState = CookingState.Moderate;
            else if (doneness01 >= donenessExcessive)
                cookingState = CookingState.Excessive;
        }

        public void CookingTimer(float time)
        {
            doneness01 += time;
        }
    }
}