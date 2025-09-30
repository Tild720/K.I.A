using Code.Core.EventSystems;
using Core.EventSystem;
using Foods;
using KWJ.Entities;
using KWJ.Interactable.PickUpable;
using UnityEngine;

namespace KWJ.Interactable
{
    public class ItemSpawner : MonoBehaviour, IInteractable
    {
        //Test Code
        [SerializeField] private GameObject item;
        
        [SerializeField] private Transform itemSpawnPoint;
        [SerializeField] private int count;
        
        private GameObject _itemPrefab;
        public GameObject GameObject => gameObject;

        private void Start()
        {
            //Test Code
            _itemPrefab = item;
        }

        public void SetIngredient(IngredientSO ingredientSo)
        {
            _itemPrefab = ingredientSo.ingredientPrefab;
        }

        public void PointerDown(Entity entity)
        {
            //if(count <= 0) return;
            
            count--;
            GameObject itemObject = Instantiate(_itemPrefab, itemSpawnPoint.position, Quaternion.identity);
            Ingredient ingredient = itemObject.GetComponentInChildren<Ingredient>();
            GameEventBus.RaiseEvent(GetItemEvents.GetItemEvent.Initialize(ingredient));
        }

        public void PointerUp(Entity entity)
        {
            
        }
    }
}