using UnityEngine;
using KWJ.Entities;
using KWJ.Interactable;
using KWJ.Players;

namespace Code.Players
{
    public class InteractableChecker : MonoBehaviour, IEntityComponent
    {
        public IInteractable Interactable => _interactable;
        private IInteractable _interactable;

        public bool IsHasIInteractable => _interactable != null;
        
        private PlayerInteractor _interactor;
        private Player _agent;
        
        private Camera _camera;
        
        public void Initialize(Entity entity)
        {
            _agent = entity as Player;
            _interactor = entity.GetCompo<PlayerInteractor>();
            
            _camera = Camera.main;
        }

        private void Update()
        {
            InteractableCheck();
        }

        private void InteractableCheck()
        {
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, _agent.PlayerStatsSo.InteractionRange, ~0, QueryTriggerInteraction.Collide))
            {
                if (hit.transform.TryGetComponent<IInteractable>(out var interactable))
                {
                    if(!_interactor.IsInteracting)
                        _interactable = interactable;
                    
                    return;
                }
            }

            if(!_interactor.IsInteracting)
                _interactable = null;
        }

        private void OnDrawGizmos()
        {
            if (_camera == null) return;
                
            Gizmos.color = Color.red;
            
            Gizmos.DrawRay(_camera.transform.position,
                _camera.transform.forward * _agent.PlayerStatsSo.InteractionRange);
        }
    }
}
