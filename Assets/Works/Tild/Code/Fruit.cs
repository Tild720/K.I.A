using KWJ.Interactable.PickUpable;
using UnityEngine;

namespace Works.Tild.Code
{
    public class Fruit : MonoBehaviour, ISliceable
    {
        [SerializeField] private GameObject _fruitPiecesPrefab;
        [field: SerializeField] public int Health { get; private set; } = 1;

        [SerializeField] private float sliceForce = 5f;
        [SerializeField] private float sliceTorque = 2f;

        public void OnKnifeTouched(Vector3 hitPos)
        {
            Health--;

            if (Health <= 0)
            {
            
                GameObject pieces = Instantiate(_fruitPiecesPrefab, transform.position, transform.rotation);

               
                Rigidbody[] rbs = pieces.GetComponentsInChildren<Rigidbody>();
                foreach (var rb in rbs)
                {
                    Vector3 randomDir = (Vector3.up + Random.insideUnitSphere).normalized;
                    rb.AddForce(randomDir * sliceForce, ForceMode.Impulse);
                    rb.AddTorque(Random.insideUnitSphere * sliceTorque, ForceMode.Impulse);
                }

            
                Destroy(gameObject);
            }
        }
    }
}