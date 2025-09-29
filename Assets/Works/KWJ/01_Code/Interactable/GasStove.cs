using KWJ.OverlapChecker;
using UnityEngine;

namespace KWJ.Interactable
{
    public class GasStove : MonoBehaviour
    {
        [SerializeField] private BoxOverlapChecker boxChecker;
        [SerializeField] private PickUpableHolder holder;
        private bool hasPot;
        private void Update()
        {
            if(!boxChecker.BoxOverlapCheck()) return;

            GameObject potObject = boxChecker.GetOverlapData()[0];

            if (potObject.TryGetComponent<Pot>(out var pot))
            {
                hasPot = true;
            }
            else
                return;
            
            PickUpable.PickUpable pickUpable = potObject.GetComponentInChildren<PickUpable.PickUpable>();
            holder.HoldPickUpalbe(pickUpable);
        }

        public void CreateFood()
        {
            
        }
    }
}