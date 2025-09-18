using System;
using KWJ.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace KWJ.Interactable
{
    public class Button : MonoBehaviour, IInteractable
    {
        public UnityEvent<bool> onClick;

        private bool _isFirstClick;

        public void PointerDown(Entity entity)
        {
            _isFirstClick = !_isFirstClick;
            onClick?.Invoke(_isFirstClick);
        }

        public void PointerUp(Entity entity)
        {
      
        }

        public GameObject GameObject { get; }
    }
}
