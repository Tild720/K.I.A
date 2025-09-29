using KWJ.Define;
using UnityEngine;

namespace KWJ.Food
{
    //액체 음식
    public class LiquidFood : Food
    {
        [SerializeField] private IngredientChecker ingredientChecker;
        
        //평가 완료
        public bool IsCompleteEvaluation => _isCompleteEvaluation;
        private bool _isCompleteEvaluation;

        public void SetFood(FoodType foodType, FoodState foodState)
        {
            m_FoodState = foodState;
            m_FoodType = foodType;
        }

        public void Reset()
        {
            _isCompleteEvaluation = false;
            ingredientChecker.Reset();
        }

        public void CreateFood()
        {
            if(_isCompleteEvaluation || ingredientChecker.IsValidIngredients == false) return;
            
            m_FoodType = ingredientChecker.FoodType;
            
            foreach (var ingredient in ingredientChecker.Ingredients)
            {
                Destroy(ingredient.gameObject);
            }
            
            m_FoodState = FoodQuality.FoodQualityCheck(ingredientChecker.Ingredients.ToArray());

            _isCompleteEvaluation = true;
        }
    }
}