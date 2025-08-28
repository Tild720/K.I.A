using System.Collections;
using KWJ.Entities;
using KWJ.Players;
using UnityEngine;

namespace KWJ.Interactable.PickUpable
{
    public class PickUpableObject : MonoBehaviour, IInteractable
    {
        private PlayerInteractor _interactor;
        private bool _isPickUp;

        public GameObject GameObject => gameObject;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
        }

        public void PointerDown(Entity entity)
        {
            _interactor = entity.GetCompo<PlayerInteractor>();
                
            _isPickUp = true;
            _rigidbody.useGravity = false;
            
            StartCoroutine(MoveToCatchPoint(_interactor.CatchPoint));
        }

        public void PointerUp(Entity entity)
        {
            _interactor = null;
            
            _isPickUp = false;
            _rigidbody.useGravity = true;
        }


        private IEnumerator MoveToCatchPoint(Transform targetTrm)
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                
                transform.position = Vector3.Lerp(transform.position,
                    targetTrm.position, 5 * Time.deltaTime);
                
                if(!_isPickUp)
                    break;
            }
        }
    }
}