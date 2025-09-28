using KWJ.Define;
using KWJ.Interactable;
using KWJ.Interactable.PickUpable;
using UnityEngine;

namespace KWJ.Food
{
    public class StackFood : Food
    {
        [SerializeField] private IngredientStackChecker ingredientChecker;

        private int _deduction;

        private bool _isCompleteEvaluation;

        private void Update()
        {
            if(_isCompleteEvaluation || ingredientChecker.IsCompleteStack == false) return;

            foreach (var ingredient in ingredientChecker.Ingredients)
            {
                if (ingredient is CookableIngredient cookableIngredient)
                {
                    if (cookableIngredient.CookingState != CookingState.Moderate)
                        _deduction++;
                }
            }

            if (_deduction >= 3)
                m_FoodState = FoodState.Bad;
            else if (_deduction >= 1)
                m_FoodState = FoodState.Normal;
            else if (_deduction == 0)
                m_FoodState = FoodState.Good;

            _isCompleteEvaluation = true;
        }
    }
}