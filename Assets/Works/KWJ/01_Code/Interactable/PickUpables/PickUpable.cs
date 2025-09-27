using System.Collections;
using KWJ.Entities;
using KWJ.Players;
using UnityEngine;

namespace KWJ.Interactable.PickUpable
{
    [RequireComponent(typeof(Rigidbody))]
    public class PickUpable : MonoBehaviour, IInteractable
    {
        private PlayerInteractor _interactor;
        private Player _player;
        private bool _isPickUp;

        public GameObject GameObject => gameObject;
        private Rigidbody _rigidbody;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void PointerDown(Entity entity)
        {
            _interactor = entity.GetCompo<PlayerInteractor>();
            _player = entity as Player;
                
            _isPickUp = true;
            _rigidbody.useGravity = false;
            
            StartCoroutine(MoveToCatchPoint(_interactor.CatchPoint));
        }

        public void PointerUp(Entity entity)
        {
            _isPickUp = false;
            _rigidbody.useGravity = true;

            Vector3 forceDir = _interactor.CatchPoint.position - transform.position;
            float distance = forceDir.magnitude;
            
            _rigidbody.AddForce(forceDir * distance * _player.PlayerStatsSo.ThrowPower, ForceMode.Impulse);
            
            _interactor = null;
        }

        private IEnumerator MoveToCatchPoint(Transform targetTrm)
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                
                _rigidbody.position = Vector3.Lerp(transform.position,
                    targetTrm.position, 5 * Time.deltaTime);

                _rigidbody.rotation = targetTrm.rotation;
                
                if(!_isPickUp)
                    break;
            }
        }
    }
}