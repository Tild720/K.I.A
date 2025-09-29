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
    public class IngredientOrderRecipe
    {
        [field: SerializeField] public FoodType foodType;
        [field:SerializeField] public List<IngredientType> IngredientTypes { get; private set; }  = new List<IngredientType>();
    }
    
    public class IngredientStackChecker : MonoBehaviour
    {
        [SerializeField] private RaycastChecker rayChecker;
        
        [SerializeField] private List<IngredientOrderRecipe> ingredientOrders = new List<IngredientOrderRecipe>();

        public List<Ingredient> Ingredients => _ingredients;
        private List<Ingredient> _ingredients = new List<Ingredient>();
        
        public FoodType FoodType => _foodType;
        private FoodType _foodType;
        
        public bool IsCompleteStack => _isCompleteStack;
        private bool _isCompleteStack;
        
        private void Update()
        {
            if(_isCompleteStack || !rayChecker.RaycastCheck()) return;
            
            GameObject[] foodIngredients = rayChecker.GetRaycastData();
            
            Ingredient[] ingredients = foodIngredients.Select(i 
                => i.GetComponentInChildren<Ingredient>()).ToArray();

            IngredientStackCheck(ingredients);
        }

        private void IngredientStackCheck(Ingredient[] ingredients)
        {
            foreach (var ingredientOrder in ingredientOrders)
            {
                int cnt = 0;
                for (int i = 0; i < ingredients.Length; i++)
                {
                    if (ingredients[i].Rigidbody.linearVelocity.y == 0 ||
                        ingredientOrder.IngredientTypes[i] != ingredients[i].IngredientType) break;

                    cnt++;
                    
                    if (cnt == ingredientOrder.IngredientTypes.Count)
                    {
                        //재료 순서가 맞으면 리스트에 넣어주기
                        for (int j = 0; j < cnt; j++)
                        {
                            _ingredients.Add(ingredients[j]);
                        }
                        
                        _foodType = ingredientOrder.foodType;
                        
                        _isCompleteStack = true;
                        return;
                    }
                }
            }
        }
    }
}