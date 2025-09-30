using System.Collections.Generic;
using Code.Core.EventSystems;
using Core.EventSystem;
using KWJ.Core;
using KWJ.Interactable.PickUpable;
using UnityEngine;
using Works.JW.Events;

namespace KWJ.Manager
{
    public class IngredientManager : MonoSingleton<IngredientManager>
    {
        [SerializeField] private List<Ingredient> ingredients = new List<Ingredient>();
        [SerializeField] private Transform itemSpawnPoint;
        private void OnEnable()
        {
            GameEventBus.AddListener<PurchaseEvent>(CreateItem);
            GameEventBus.AddListener<NPCLineEndEvent>(ResetIngredients);
        }
        
        private void OnDisable()
        {
            GameEventBus.RemoveListener<PurchaseEvent>(CreateItem);
            GameEventBus.RemoveListener<NPCLineEndEvent>(ResetIngredients);
        }

        private void ResetIngredients(NPCLineEndEvent evt)
        {
            foreach (var ingredient in ingredients)
            {
                if(ingredient == null) continue;
                
                Destroy(ingredient.gameObject);
            }
            
            ingredients.Clear();
        }
        
        private void CreateItem(PurchaseEvent evt)
        {
            for (int j = 0; j < evt.count; j++)
            {
                foreach (var ingred in evt.food.ingredient)
                {
                    for (int i = 0; i < ingred.count; i++)
                    {
                        GameObject item = Instantiate(ingred.ingredient.ingredientPrefab, itemSpawnPoint.position, Quaternion.identity);
                        Ingredient ingredient = item.GetComponentInChildren<Ingredient>();
                        ingredients.Add(ingredient);
                    }
                }
            }
        }
    }
}