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

        private bool _canPickUp = true;

        public GameObject GameObject => gameObject;
        
        public Rigidbody Rigidbody => m_rigidbody;
        protected Rigidbody m_rigidbody;
        
        private Collider _collider;

        protected virtual void Awake()
        {
            m_rigidbody = GetComponentInChildren<Rigidbody>();
            _collider = GetComponentInChildren<Collider>();
        }

        protected virtual void SetCanPickUp(bool canPickUp)
        {
            _canPickUp = canPickUp;
            m_rigidbody.isKinematic = !canPickUp;
            _collider.enabled = canPickUp;
        }

        public void PointerDown(Entity entity)
        {
            if(_canPickUp == false) return;
            
            _interactor = entity.GetCompo<PlayerInteractor>();
            _player = entity as Player;
                
            _isPickUp = true;
            m_rigidbody.useGravity = false;
            
            StartCoroutine(MoveToCatchPoint(_interactor.CatchPoint));
        }

        public void PointerUp(Entity entity)
        {
            if(_canPickUp == false && _isPickUp == false) return;
            
            _isPickUp = false;
            m_rigidbody.useGravity = true;

            Vector3 forceDir = _interactor.CatchPoint.position - transform.position;
            float distance = forceDir.magnitude;
            
            m_rigidbody.AddForce(forceDir * distance * _player.PlayerStatsSo.ThrowPower, ForceMode.Impulse);
            
            _interactor = null;
        }

        private IEnumerator MoveToCatchPoint(Transform targetTrm)
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                
                m_rigidbody.position = Vector3.Lerp(transform.position,
                    targetTrm.position, 5 * Time.deltaTime);

                m_rigidbody.rotation = targetTrm.rotation;
                
                if(!_isPickUp)
                    break;
            }
        }
    }
}