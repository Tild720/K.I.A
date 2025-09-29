using UnityEngine;

namespace KWJ.Food
{
    //액체 음식
    public class LiquidFood : Food
    {
        [SerializeField] private IngredientChecker ingredientChecker;
        
        //평가 완료
        private bool _isCompleteEvaluation;

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