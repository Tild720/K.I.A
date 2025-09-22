using KWJ.Players;
using KWJ.Entities;
using KWJ.Interactable;
using UnityEngine;

namespace KWJ.Code.Interactable
{
    public class HighlightController : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Outline _outline;
        private InteractableChecker _interactChecker;
        
        private IInteractable _interactableTemp;
        
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
            if (_interactChecker.Interactable != null
                && _interactableTemp == _interactChecker.Interactable)
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

            _interactableTemp = _interactChecker.Interactable;
        }
    }
}