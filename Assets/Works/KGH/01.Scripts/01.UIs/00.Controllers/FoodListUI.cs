using System.Collections.Generic;
using Foods;
using UIs.Visuals.Handlers;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Controllers
{
    public class FoodListUI : MonoBehaviour
    {
        [SerializeField] private Transform listRoot;
        [SerializeField] private List<FoodSO> foods;
        [SerializeField] private GameObject foodItemPrefab;
        [SerializeField] private FoodInfoUI foodInfoUI;
        private void Start()
        {
            foreach (var food in foods)
            {
                var item = Instantiate(foodItemPrefab, listRoot);
                var foodItemUI = item.GetComponent<FoodItemUI>();
                foodItemUI.SetFood(food);
                
                var button = item.GetComponent<Button>();
                button.onClick.AddListener(()=> HandleFoodSelect(food));
                
                var hoverHandler = item.GetComponent<HoverHandler>();
                hoverHandler.onHoverChanged += v => HandleFoodHover(v, food);
            }
        }
        private void HandleFoodHover(bool value, FoodSO food)
        {
            if (value)
            {
                foodInfoUI.SetUpFood(food);
            }
        }

        private void HandleFoodSelect(FoodSO food)
        {
        }
    }
}