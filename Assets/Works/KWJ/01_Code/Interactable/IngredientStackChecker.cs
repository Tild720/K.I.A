using System;
using System.Collections.Generic;
using System.Linq;
using KWJ.Define;
using KWJ.Interactable.PickUpable;
using KWJ.OverlapChecker;
using UnityEngine;

namespace KWJ.Interactable
{
    [Serializable]
    public class IngredientOrder
    {
        [field:SerializeField] public List<IngredientType> IngredientTypes { get; private set; }  = new List<IngredientType>();
    }
    
    public class IngredientStackChecker : MonoBehaviour
    {
        [SerializeField] private RaycastChecker rayChecker;
        
        [SerializeField] private List<IngredientOrder> ingredientOrders = new List<IngredientOrder>();
        
        private List<CookableIngredient> _ingredients = new List<CookableIngredient>();
        
        private bool _isCompleteStack;
        
        private void Update()
        {
            if(_isCompleteStack || !rayChecker.RaycastCheck()) return;
            
            GameObject[] foodIngredients = rayChecker.GetRaycastData();
            
            CookableIngredient[] ingredients = foodIngredients.Select(i 
                => i.GetComponentInChildren<CookableIngredient>()).ToArray();

            IngredientStackCheck(ingredients);
        }

        private void IngredientStackCheck(CookableIngredient[] ingredients)
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
                            ingredients[j].CompleteCooking(transform);
                            _ingredients.Add(ingredients[j]);
                        }
                        
                        _isCompleteStack = true;
                        return;
                    }
                }
            }
        }
    }
}