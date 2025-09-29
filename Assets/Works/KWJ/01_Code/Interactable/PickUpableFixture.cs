using UnityEngine;

namespace KWJ.Interactable
{
    public class PickUpableFixture : MonoBehaviour
    {
        [SerializeField] private Transform fixedPoint;

        public void FixedPickUpalbe(PickUpable.PickUpable pickUpable)
        {
            pickUpable.transform.position = fixedPoint.position;
            pickUpable.transform.rotation = fixedPoint.rotation;
            pickUpable.SetCanPickUp(false);
        }
    }
}