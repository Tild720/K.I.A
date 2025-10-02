using UnityEngine;

namespace KWJ.Food
{
    public class StackFood : Food
    {
        [SerializeField] private IngredientStackChecker ingredientChecker;

        //평가 완료
        private bool _isCompleteEvaluation;

        private void Update()
        {
            if(_isCompleteEvaluation || ingredientChecker.IsCompleteStack == false) return;
            
            m_FoodType = ingredientChecker.FoodType;
            
            foreach (var ingredient in ingredientChecker.Ingredients)
            {
                if(ingredient == null) continue;
                
                ingredient.CompleteCooking(transform);
            }
            
            m_FoodState = FoodQuality.FoodQualityCheck(ingredientChecker.Ingredients.ToArray());

            _isCompleteEvaluation = true;
        }
    }
}