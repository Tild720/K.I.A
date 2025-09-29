using KWJ.Interactable.PickUpable;
using UnityEngine;

namespace KWJ.Interactable
{
    public class PickUpableHolder : MonoBehaviour
    {
        [SerializeField] private Transform container;

        public void HoldPickUpalbe(PickUpable.PickUpable pickUpable)
        {
            pickUpable.SetCanPickUp(false);
            pickUpable.transform.position = container.position;
            pickUpable.transform.rotation = container.rotation;
        }
    }
}