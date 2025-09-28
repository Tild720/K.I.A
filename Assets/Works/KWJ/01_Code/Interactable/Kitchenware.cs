using KWJ.Define;
using KWJ.Interactable.PickUpable;
using KWJ.OverlapChecker;
using UnityEngine;

namespace KWJ.Interactable
{
    public class Kitchenware : MonoBehaviour
    {
        [SerializeField] private BoxOverlapChecker boxChecker;
        
        [SerializeField] private CookingType cookingType;
        
        protected bool m_IsOn;

        protected virtual void Update()
        {
            if(!m_IsOn) return;
            
            if (boxChecker.BoxOverlapCheck())
            {
                GameObject[] foodIngredients = boxChecker.GetOverlapData();

                foreach (var foodIngredient in foodIngredients)
                {
                    CookableIngredient cookable
                        = foodIngredient.GetComponentInChildren<CookableIngredient>();
                    
                    if (cookable ==null || (cookable.CookingType & cookingType) != cookingType) continue;

                    cookable.CookingTimer(Time.deltaTime);
                }
            }
        }

        public void OnKitchenwera() => m_IsOn = !m_IsOn;
    }
}