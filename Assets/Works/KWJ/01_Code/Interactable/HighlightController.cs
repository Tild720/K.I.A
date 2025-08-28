using Code.Players;
using KWJ.Entities;
using UnityEngine;

namespace KWJ.Code.Interactable
{
    public class HighlightController : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Outline _outline;
        private InteractableChecker _interactChecker;
        private bool isFirstOnFocus = true;

        public void Initialize(Entity entity)
        {
            _interactChecker =  entity.GetCompo<InteractableChecker>();
        }

        private void OnDisable()
        {
            _outline.OnUnfocus();
        }

        private void Update()
        {
            if (_interactChecker.Interactable != null)
            {
                if(!isFirstOnFocus) return;
                
                isFirstOnFocus = false;
                
                _outline.OnFocus(_interactChecker.Interactable.GameObject);
            }
            else
            {
                _outline.OnUnfocus();
                isFirstOnFocus = true;
            }
        }
    }
}