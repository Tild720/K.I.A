using UnityEngine;

namespace Foods
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "SO/Food/Ingredients", order = 0)]
    public class IngredientSO : ScriptableObject
    {
        public Sprite icon;
        public string ingredientName;
        public GameObject ingredientPrefab;
    }
}