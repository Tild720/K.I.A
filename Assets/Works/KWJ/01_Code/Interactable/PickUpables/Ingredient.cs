using KWJ.Define;
using UnityEngine;

namespace KWJ.Interactable.PickUpable
{
    public class Ingredient : PickUpable
    {
        public IngredientType IngredientType => ingredientType;
        [SerializeField] private IngredientType ingredientType;
        
        protected bool m_IsCompleteCooking;
        
        public void CompleteCooking(Transform dish)
        {
            transform.SetParent(dish);
            m_rigidbody.isKinematic = true;
            m_IsCompleteCooking = true;
            SetCanPickUp(false);
        }
    }
}