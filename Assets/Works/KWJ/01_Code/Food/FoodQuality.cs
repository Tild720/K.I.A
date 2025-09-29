using KWJ.Core;
using KWJ.Define;
using KWJ.Interactable.PickUpable;
using UnityEngine;

namespace KWJ.Food
{
    public class FoodQuality : MonoBehaviour
    {
        public static FoodState FoodQualityCheck(Ingredient[] ingredients)
        {
            int deduction = 0;
            
            foreach (var ingredient in ingredients)
            {
                if (ingredient is CookableIngredient cookableIngredient)
                {
                    if (cookableIngredient.CookingState != CookingState.Moderate)
                        deduction++;
                }
            }
            
            if (deduction >= 3)
                return FoodState.Bad;
            
            if (deduction >= 1)
                return FoodState.Normal;
            
            return FoodState.Good;
        }
    }
}