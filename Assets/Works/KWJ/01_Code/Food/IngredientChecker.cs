using System;
using System.Collections.Generic;
using System.Linq;
using KWJ.Define;
using KWJ.Interactable.PickUpable;
using KWJ.OverlapChecker;
using UnityEngine;

namespace KWJ.Food
{
    [Serializable]
    public struct IngredientTemp
    {
        [field: SerializeField] public IngredientType ingredientType;
        [field: SerializeField] public int ingredientCount;
    }
    
    public class IngredientChecker : MonoBehaviour
    {
        [SerializeField] private BoxOverlapChecker boxChecker;

        [SerializeField] private List<IngredientTemp> ingredientCounts = new List<IngredientTemp>();
        private Dictionary<IngredientType, int> _ingredientCounts = new Dictionary<IngredientType, int>();
        private Dictionary<IngredientType, int> _ingredientCountChecks = new Dictionary<IngredientType, int>();
        
        public bool IsValidIngredients => _isValidIngredients;
        private bool _isValidIngredients;

        private void Awake()
        {
            foreach (var ingredientCount in ingredientCounts)
            {
                _ingredientCounts[ingredientCount.ingredientType] = ingredientCount.ingredientCount;
            }
        }

        private void Update()
        {
            if(_isValidIngredients || !boxChecker.BoxOverlapCheck()) return;
            
            GameObject[] foodIngredients = boxChecker.GetOverlapData();
            
            Ingredient[] ingredients = foodIngredients.Select(i 
                => i.GetComponentInChildren<Ingredient>()).ToArray();
            
            IngredientCheck(ingredients);
        }
        
        private void IngredientCheck(Ingredient[] ingredients)
        {
            _ingredientCountChecks.Clear();
            
            foreach (var ingredient in ingredients)
            {
                _ingredientCountChecks[ingredient.IngredientType] += 1;
            }
            
            foreach (var ingredientCount in _ingredientCounts)
            {
                if(_ingredientCounts[ingredientCount.Key]
                   > _ingredientCountChecks[ingredientCount.Key]) return;
            }
            
            _isValidIngredients = true;
        }
    }
}