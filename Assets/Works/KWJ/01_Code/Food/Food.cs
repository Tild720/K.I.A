using KWJ.Define;
using UnityEngine;

namespace KWJ.Food
{
    public class Food : MonoBehaviour
    {
        public FoodState FoodState => m_FoodState;
        [SerializeField]protected FoodState m_FoodState;
        
        public FoodType FoodType => m_FoodType;
        [SerializeField]protected FoodType m_FoodType;
    }
}