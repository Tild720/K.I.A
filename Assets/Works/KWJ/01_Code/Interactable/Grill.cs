using KWJ.Interactable.PickUpable;
using KWJ.OverlapChecker;
using UnityEngine;

namespace KWJ.Interactable
{
    public class Grill : MonoBehaviour
    {
        [SerializeField] private BoxOverlapChecker _boxChecker;
        
        [SerializeField] private CookingType cookingType;
        
        private bool _isGrillOn;

        private void Update()
        {
            //print(_isGrillOn);
            
            if(!_isGrillOn) return;
            
            if (_boxChecker.BoxOverlapCheck())
            {
                GameObject[] foodIngredients = _boxChecker.GetOverlapData();

                foreach (var foodIngredient in foodIngredients)
                {
                    CookableIngredient cookable
                        = foodIngredient.GetComponentInChildren<CookableIngredient>();

                    if (cookable.CookingType == cookingType)
                    {
                        cookable.CookingTimer(Time.deltaTime);
                    }
                }
            }
        }

        public void OnGrill() => _isGrillOn = !_isGrillOn;
    }
}