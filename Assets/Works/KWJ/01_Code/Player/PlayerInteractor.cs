using KWJ.Players;
using UnityEngine;
using KWJ.Entities;

namespace KWJ.Players
{
    public class PlayerInteractor : MonoBehaviour, IEntityComponent
    {
        public Transform CatchPoint => _catchPoint;
        [SerializeField] private Transform _catchPoint;
        
        private Player _agent;
        private InteractableChecker _interChecker;
        public bool IsInteracting => _isInteracting;
        private bool _isInteracting;
        
        public void Initialize(Entity entity)
        {
            _agent = entity as Player;
            
            _interChecker = entity.GetCompo<InteractableChecker>();
        }

        private void OnEnable()
        {
            _agent.PlayerInputSo.OnInteractAction += OnInteract;
        }
        private void OnDisable()
        {
            _agent.PlayerInputSo.OnInteractAction -= OnInteract;
        }

        private void OnInteract(bool obj)
        {
            _isInteracting = obj;

            if(_interChecker.Interactable == null) return;
            
            if(_isInteracting)
                _interChecker.Interactable.PointerDown(_agent);
            else
                _interChecker.Interactable.PointerUp(_agent);
        }
    }
}