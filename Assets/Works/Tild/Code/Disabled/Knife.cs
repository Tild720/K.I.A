using UnityEngine;

namespace Works.Tild.Code
{
    public class Knife : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Vector3 hitPoint = collision.contacts[0].point;

            ISliceable sliceable = collision.gameObject.GetComponent<ISliceable>();
            if (sliceable != null)
            {
                sliceable.OnKnifeTouched(hitPoint);
            }
        }
    }
}