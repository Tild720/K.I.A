using Foods;
using KWJ.Entities;
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
            Instantiate(_itemPrefab, itemSpawnPoint.position, Quaternion.identity);
        }

        public void PointerUp(Entity entity)
        {
            
        }
    }
}