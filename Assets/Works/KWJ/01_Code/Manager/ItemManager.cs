using System.Collections.Generic;
using Code.Core.EventSystems;
using Core.EventSystem;
using Foods;
using KWJ.Core;
using KWJ.Interactable.PickUpable;
using UnityEngine;

namespace KWJ.Manager
{
    public class ItemManager : MonoSingleton<ItemManager>
    {
        [SerializeField] private List<Ingredient> ingredients = new List<Ingredient>();
        [SerializeField] private Transform itemSpawnPoint;
        private void OnEnable()
        {
            GameEventBus.AddListener<PurchaseEvent>(CreateItem);
            GameEventBus.AddListener<GetItemEvent>(AddItem);
        }
        
        private void OnDisable()
        {
            GameEventBus.RemoveListener<PurchaseEvent>(CreateItem);
            GameEventBus.RemoveListener<GetItemEvent>(AddItem);
        }
        
        private void AddItem(GetItemEvent evt)
        {
            ingredients.Add(evt.ingredient);
        }

        private void CreateItem(PurchaseEvent evt)
        {
            for (int i = 0; i < evt.count; i++)
            {
                GameObject item = Instantiate(evt.food.foodPrefab, itemSpawnPoint.position, Quaternion.identity);
                //ingredients.Add(item.GetComponent<PickUpable>());
            }
        }
    }
}