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
        [SerializeField] private List<GameObject> ingredients = new List<GameObject>();
        [SerializeField] private Transform itemSpawnPoint;
        
        [SerializeField] private GameObject juk, burger, rottenmeat, soup, steak, toast;
        
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
                Destroy(ingredient);
            }
            
            ingredients.Clear();
        }
        
        private void CreateItem(PurchaseEvent evt)
        {
            switch (evt.food.foodName)
            {
                case "맨죽" :
                    juk.SetActive(true);
                    break;
                case "햄버거" :
                    burger.SetActive(true);
                    break;
                case "유통기한 고기" :
                    rottenmeat.SetActive(true);
                    break;
                case "토스트" :
                    juk.SetActive(true);
                    break;
                case "수프" :
                    soup.SetActive(true);
                    break;
                case "스테이크" :
                    steak.SetActive(true);
                    break;
            }
            
            for (int i = 0; i < evt.count; i++)
            {
                foreach (var ingred in evt.food.ingredient)
                {
                    for (int j = 0; j < ingred.count; j++)
                    {
                        GameObject item = Instantiate(ingred.ingredient.ingredientPrefab, itemSpawnPoint.position, Quaternion.identity);

                        if (item == null)
                        {
                            Debug.LogError("ingredientPrefab이 없습니다.");
                            continue;
                        }

                        
                        
                        ingredients.Add(item);
                    }
                }
            }
        }
    }
}