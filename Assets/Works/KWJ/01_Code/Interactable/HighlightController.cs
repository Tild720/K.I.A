using KWJ.Players;
using KWJ.Entities;
using KWJ.Interactable;
using UnityEngine;

namespace KWJ.Code.Interactable
{
    public class HighlightController : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Outline outline;
        private InteractableChecker _interactChecker;
        
        private IInteractable _interactableTemp;
        
        private bool _isFirstOnFocus = true;

        public void Initialize(Entity entity)
        {
            _interactChecker =  entity.GetCompo<InteractableChecker>();
        }

        private void OnDisable()
        {
            outline.OnUnfocus();
        }

        private void Update()
        {
            if (_interactChecker.Interactable != null
                && _interactableTemp == _interactChecker.Interactable)
            {
                if(!_isFirstOnFocus) return;
                
                _isFirstOnFocus = false;
                
                outline.OnFocus(_interactChecker.Interactable.GameObject);
            }
            else
            {
                outline.OnUnfocus();
                _isFirstOnFocus = true;
            }

            _interactableTemp = _interactChecker.Interactable;
        }
    }
}