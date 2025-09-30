using Core.Defines;
using UnityEngine;

namespace Foods
{
    [CreateAssetMenu(fileName = "Food", menuName = "SO/Food/Food", order = 0)]
    public class FoodSO : ScriptableObject
    {
        public string foodName;
        public GameObject foodPrefab;
        public Sprite icon;
        public int price;
        [TextArea]
        public string description;
        public StructDefines.IngredientData[] ingredient;
    }
}