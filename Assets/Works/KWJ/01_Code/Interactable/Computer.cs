using KWJ.Entities;
using KWJ.Players;
using UnityEngine;
using UnityEngine.Events;

namespace KWJ.Interactable
{
    public class Computer : MonoBehaviour, IInteractable
    {
        public UnityEvent onComputerEvent;
        public UnityEvent offComputerEvent;
        
        [SerializeField] private Transform cameraPoint;
        public GameObject GameObject => gameObject;
        
        private PlayerMovement _playerMovement;
        private Player _player;
        
        private bool _isInteracting;

        public void PointerDown(Entity entity)
        {
            _player = entity as Player;
            _playerMovement = entity.GetCompo<PlayerMovement>();

            if (_player == null)
            {
                Debug.LogWarning("Player가 null 입니다.");
                return;
            }

            if (!_isInteracting)
            {
                _player.CinemaCamera.Follow = cameraPoint;
                _playerMovement.IsStopMovement = true;
                _isInteracting = true;
                
                offComputerEvent?.Invoke();
            }
            else
            {
                _player.CinemaCamera.Follow = _player.HeadPoint;
                _playerMovement.IsStopMovement = false;
                _isInteracting = false;
                
                onComputerEvent?.Invoke();
            }
        }

        public void PointerUp(Entity entity)
        {
            if(_isInteracting) return;
                
            _player = null;
            _playerMovement = null;
        }
    }
}
