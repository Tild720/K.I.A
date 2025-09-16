using UnityEngine;

namespace Foods
{
    [CreateAssetMenu(fileName = "Food", menuName = "SO/Food", order = 0)]
    public class FoodSO : ScriptableObject
    {
        public string foodName;
        public GameObject foodPrefab;
        public int price;
        [TextArea]
        public string description;
        public int refugeePoint;
        public int governmentPoint;
        public IngredientSO[] ingredient;
    }
}