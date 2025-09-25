using System;
using Foods;
using KWJ.Entities;
using KWJ.Interactable;
using UnityEngine;

namespace Works.KWJ._01_Code.Interactable
{
    public class IngredientBasket : MonoBehaviour, IInteractable
    {
        [SerializeField] private IngredientSO ingredient;
        [SerializeField] private Transform ingredientSpawnPoint;
        [SerializeField] private int count;
        
        private GameObject _ingredientPrefab;
        public GameObject GameObject => gameObject;

        private void Start()
        {
            _ingredientPrefab = ingredient.ingredientPrefab;
        }

        public void SetIngredient(IngredientSO ingredient)
        {
            _ingredientPrefab = ingredient.ingredientPrefab;
        }

        public void PointerDown(Entity entity)
        {
            count--;
            Instantiate(_ingredientPrefab, ingredientSpawnPoint.position, Quaternion.identity);
        }

        public void PointerUp(Entity entity)
        {
            
        }
    }
}