using System;
using System.Collections.Generic;
using System.Linq;
using KWJ.Interactable.PickUpable;
using KWJ.OverlapChecker;
using UnityEngine;

namespace KWJ.Interactable
{
    public enum IngredientType
    {
        None = 0,
        
        TopBurgerBun,
        Patty,
        Lettuce,
        Tomato,
        BottomBurgerBun,
                
        Max,
    }

    [Serializable]
    public class IngredientOrder
    {
        [field:SerializeField] public List<IngredientType> IngredientTypes { get; private set; }  = new List<IngredientType>();
    }
    
    public class IngredientStackChecker : MonoBehaviour
    {
        [SerializeField] private RaycastChecker rayChecker;
        
        [SerializeField] private List<IngredientOrder> ingredientOrders = new List<IngredientOrder>();
        
        private bool _isCompleteStack;
        
        private void Update()
        {
            if(_isCompleteStack || !rayChecker.RaycastCheck()) return;
            
            GameObject[] foodIngredients = rayChecker.GetRaycastData();
            
            CookableIngredient[] ingredients = foodIngredients.Select(i 
                => i.GetComponentInChildren<CookableIngredient>()).ToArray();

            foreach (var ingredient in ingredients)
            {
                print(ingredient.IngredientType);
            }
            
            IngredientStackCheck(ingredients);
        }

        private void IngredientStackCheck(CookableIngredient[] ingredients)
        {
            foreach (var ingredientOrder in ingredientOrders)
            {
                int cnt = 0;
                
                for (int i = 0; i < ingredients.Length; i++)
                {
                    if (ingredientOrder.IngredientTypes[i] != ingredients[i].IngredientType) break;
                    cnt++;
                }
                
                if (cnt != ingredientOrder.IngredientTypes.Count) break;

                _isCompleteStack = true;
                break;
            }
        }
    }
}