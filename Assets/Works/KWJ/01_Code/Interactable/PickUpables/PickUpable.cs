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

        protected bool m_canPickUp = true;

        public GameObject GameObject => gameObject;
        
        public Rigidbody Rigidbody => m_rigidbody;
        protected Rigidbody m_rigidbody;
        
        protected Collider m_collider;

        protected virtual void Awake()
        {
            m_rigidbody = GetComponentInChildren<Rigidbody>();
            m_collider = GetComponentInChildren<Collider>();
        }

        public virtual void SetCanPickUp(bool canPickUp)
        {
            m_canPickUp = canPickUp;
            m_rigidbody.isKinematic = !canPickUp;
            m_rigidbody.useGravity = true;
        }

        public virtual void PointerDown(Entity entity)
        {
            if(m_canPickUp == false) return;
            
            _interactor = entity.GetCompo<PlayerInteractor>();
            _player = entity as Player;
                
            _isPickUp = true;
            m_rigidbody.useGravity = false;
            
            StartCoroutine(MoveToCatchPoint(_interactor.CatchPoint));
        }

        public virtual void PointerUp(Entity entity)
        {
            _isPickUp = false;
            
            if(m_canPickUp == false) return;
            
            if(m_rigidbody != null)
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