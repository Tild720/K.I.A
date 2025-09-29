using System;
using System.Collections.Generic;
using System.Linq;
using KWJ.Define;
using KWJ.Interactable.PickUpable;
using KWJ.OverlapChecker;
using UnityEngine;

namespace KWJ.Food
{
    [Serializable]
    public struct IngredientCount
    {
        [field: SerializeField] public IngredientType ingredientType;
        [field: SerializeField] public int ingredientCount;
    }
    
    [Serializable]
    public class FoodRecipe
    {
        [field: SerializeField] public FoodType foodType;
        [field:SerializeField] public List<IngredientCount> IngredientCounts { get; private set; }  = new List<IngredientCount>();
    }
    
    public class IngredientChecker : MonoBehaviour
    {
        [SerializeField] private BoxOverlapChecker boxChecker;

        //요리 조합법 리스트
        [SerializeField] private List<FoodRecipe> foodRecipes = new List<FoodRecipe>();
        
        //요리 조합법 확인용 리스트
        private Dictionary<IngredientType, List<Ingredient>> _foodRecipeChecks = new Dictionary<IngredientType, List<Ingredient>>();
        public List<Ingredient> Ingredients => _ingredients;
        private List<Ingredient> _ingredients = new List<Ingredient>();
        
        public FoodType FoodType => _foodType;
        private FoodType _foodType;
        public bool IsValidIngredients => _isValidIngredients;
        private bool _isValidIngredients;

        private void Update()
        {
            if(_isValidIngredients || !boxChecker.BoxOverlapCheck()) return;
            
            GameObject[] foodIngredients = boxChecker.GetOverlapData();
            
            Ingredient[] ingredients = foodIngredients.Select(i 
                => i.GetComponentInChildren<Ingredient>()).ToArray();
            
            if(ingredients.Length == 0) return;
            
            IngredientCheck(ingredients);
        }

        public void Reset()
        {
            _ingredients.Clear();
            _foodType = FoodType.None;
            _isValidIngredients = false;
            _foodRecipeChecks.Clear();
        }

        private void IngredientCheck(Ingredient[] ingredients)
        {
            _foodRecipeChecks.Clear();
            
            //_ingredientChecks에 재료들 세팅
            foreach (var ingredient in ingredients)
            {
                if(_foodRecipeChecks.ContainsKey(ingredient.IngredientType) == false)
                    _foodRecipeChecks.Add(ingredient.IngredientType, new List<Ingredient>());
                
                _foodRecipeChecks[ingredient.IngredientType].Add(ingredient);
            }
            
            //foodRecipes에서 실제 있는 조합법인지 찾기
            foreach (var foodRecipe in foodRecipes)
            { 
                int cnt = foodRecipe.IngredientCounts.Count;
                
                foreach (var foodRecipeCheck in _foodRecipeChecks)
                {
                    if (TryGetIngredientCount(foodRecipe.IngredientCounts, foodRecipeCheck.Key, out var count) == false
                        || _foodRecipeChecks[foodRecipeCheck.Key].Count < count) break;

                    cnt--;
                }
                
                if (cnt == 0)
                {
                    _foodType = foodRecipe.foodType;
                    break;
                }
            }
            
            if(_foodType == FoodType.None) return;

            foreach (var ingredientCountCheck in _foodRecipeChecks)
                foreach (var ingredient in ingredientCountCheck.Value)
                    _ingredients.Add(ingredient);
            
            _foodRecipeChecks.Clear();
            _isValidIngredients = true;
        }
        private bool TryGetIngredientCount(List<IngredientCount> ingredientCounts, IngredientType ingredientType, out int count)
        {
            count = 0;
            
            foreach (var ingredientCount in ingredientCounts)
                if (ingredientCount.ingredientType == ingredientType)
                {
                    count = ingredientCount.ingredientCount;
                    return true;
                }

            return false;
        }
    }
}
