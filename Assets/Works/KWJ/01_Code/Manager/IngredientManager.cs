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
                Destroy(ingredient.gameObject);
            }
            
            ingredients.Clear();
        }
        
        private void CreateItem(PurchaseEvent evt)
        {
            for (int i = 0; i < evt.count; i++)
            {
                foreach (var ingred in evt.food.ingredient)
                {
                    for (int j = 0; j < ingred.count; j++)
                    {
                        GameObject item = Instantiate(ingred.ingredient.ingredientPrefab, itemSpawnPoint.position, Quaternion.identity);
                        Ingredient ingredient = item.GetComponentInChildren<Ingredient>();
                        ingredients.Add(ingredient);
                        print(ingredient.gameObject.name);
                    }
                }
            }
        }
    }
}