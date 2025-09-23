using System;
using System.Collections;
using KWJ.Entities;
using KWJ.Interactable;
using KWJ.Players;
using UnityEngine;
using UnityEngine.Events;

namespace Works.KWJ._01_Code.Interactable
{
    public class Door : MonoBehaviour, IInteractable
    {
        public UnityEvent onOpenCloseEvent;
        
        [SerializeField] private float openingPower;
        [Space]
        [SerializeField] private float minCloseLimit;
        [SerializeField] private float maxCloseLimit;
        
        private PlayerInteractor _interactor;
        private bool _isPickUp;

        public GameObject GameObject => gameObject;
        private Rigidbody _rigidbody;

        private bool _isOpen;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            SetOpen(false);
        }

        public void PointerDown(Entity entity)
        {
            _interactor = entity.GetCompo<PlayerInteractor>();
            _isPickUp = true;

            if (!_isOpen)
            {
                onOpenCloseEvent?.Invoke();
                
                _rigidbody.AddForce(Vector3.back * 2f, ForceMode.Impulse);
                SetOpen(true);
            }
            
            StartCoroutine(MoveToCatchPoint(_interactor.CatchPoint));
        }

        public void PointerUp(Entity entity)
        {
            _isPickUp = false;
            _interactor = null;
            
            float anglesX = Mathf.DeltaAngle(0f, transform.eulerAngles.x);

            if (anglesX > minCloseLimit && anglesX < maxCloseLimit)
            {
                onOpenCloseEvent?.Invoke();
                
                SetOpen(false);
            }
        }

        private void SetOpen(bool isOpen)
        {
            _isOpen = isOpen;
            _rigidbody.isKinematic = !isOpen;
        }

        private IEnumerator MoveToCatchPoint(Transform targetTrm)
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                
                _rigidbody.linearVelocity = Vector3.Lerp(transform.localPosition,
                    targetTrm.position, openingPower * Time.deltaTime);
                
                if(!_isPickUp)
                    break;
            }
        }
    }
}