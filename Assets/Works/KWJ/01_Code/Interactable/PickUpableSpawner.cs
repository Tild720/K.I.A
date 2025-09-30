using System;
using Code.Core.EventSystems;
using KWJ.Entities;
using UnityEngine;
using Works.JW.Events;

namespace KWJ.Interactable
{
    public class PickUpableSpawner : MonoBehaviour, IInteractable
    {
        //Test Code
        [SerializeField] private GameObject item;
        
        [SerializeField] private Transform itemSpawnPoint;
        [SerializeField] private int count;
        
        private PickUpable.PickUpable[] _pickUpables;
        
        private int _currentCount;
        
        private GameObject _itemPrefab;
        public GameObject GameObject => gameObject;

        private void Awake()
        {
            _itemPrefab = item;
            _currentCount = count;
            _pickUpables = new PickUpable.PickUpable[count];
        }
        
        private void OnEnable()
        {
            GameEventBus.AddListener<NPCLineEndEvent>(ResetCookingTool);
        }
        
        private void OnDisable()
        {
            GameEventBus.RemoveListener<NPCLineEndEvent>(ResetCookingTool);
        }

        private void ResetCookingTool(NPCLineEndEvent evt)
        {
            _currentCount = count;

            foreach (var pickUpable in _pickUpables)
            {
                Destroy(pickUpable);
            }

            Array.Clear(_pickUpables, 0, count);
        }

        public void PointerDown(Entity entity)
        {
            if(_currentCount <= 0) return;
            
            GameObject itemObject = Instantiate(_itemPrefab, itemSpawnPoint.position, Quaternion.identity);
            PickUpable.PickUpable pickUpable = itemObject.GetComponentInChildren<PickUpable.PickUpable>();
            
            _pickUpables[count - _currentCount] = pickUpable;
            
            _currentCount--;
        }

        public void PointerUp(Entity entity)
        {
            
        }
    }
}