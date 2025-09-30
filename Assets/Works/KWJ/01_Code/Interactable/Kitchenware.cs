using KWJ.Define;
using KWJ.Interactable.PickUpable;
using KWJ.OverlapChecker;
using UnityEngine;
using UnityEngine.Events;

namespace KWJ.Interactable
{
    public class Kitchenware : MonoBehaviour
    {
        public UnityEvent onGrillingEvent;
        public UnityEvent offGrillingEvent;
        
        [SerializeField] private BoxOverlapChecker boxChecker;
        
        [SerializeField] private CookingType cookingType;
        
        protected bool m_IsOn;
        protected bool m_IsHasIngredient;
        
        protected virtual void Update()
        {
            if (!m_IsOn)
            {
                offGrillingEvent?.Invoke();
                return;
            }
            
            if (boxChecker.BoxOverlapCheck())
            {
                GameObject[] foodIngredients = boxChecker.GetOverlapData();
                
                onGrillingEvent?.Invoke();

                foreach (var foodIngredient in foodIngredients)
                {
                    CookableIngredient cookable
                        = foodIngredient.GetComponentInChildren<CookableIngredient>();
                    
                    if (cookable ==null || (cookable.CookingType & cookingType) != cookingType) continue;

                    m_IsHasIngredient = true;

                    cookable.CookingTimer(Time.deltaTime);
                }
            }
            else
            {
                m_IsHasIngredient = false;
                offGrillingEvent?.Invoke();
            }
        }

        public void OnKitchenwera() => m_IsOn = !m_IsOn;
    }
}