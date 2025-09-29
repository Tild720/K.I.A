using System;
using System.Collections.Generic;
using Core.Defines;
using Foods;
using UIs.Visuals.Handlers;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Controllers.FoodUI
{
    public class FoodListUI : MonoBehaviour
    {
        [SerializeField] private Transform listRoot;
        [SerializeField] private List<FoodSO> foods;
        [SerializeField] private GameObject foodItemPrefab;
        [SerializeField] private FoodInfoUI foodInfoUI;

        private Dictionary<FoodSO, FoodItemUI> _foodItemUIs = new Dictionary<FoodSO, FoodItemUI>();
        private FoodSO _selectedFood;

        private void Start()
        {
            foreach (var food in foods)
            {
                var item = Instantiate(foodItemPrefab, listRoot);
                var foodItemUI = item.GetComponent<FoodItemUI>();
                _foodItemUIs.Add(food, foodItemUI);
                foodItemUI.SetFood(food);

                foodItemUI.VisualElement.OnStateChanged += str => HandleStateChange(str, food);
            }
            
            HandleStateChange(ConstDefine.HOVER, foods[0]);
        }

        private void HandleStateChange(string state, FoodSO food)
        {
            if (_selectedFood != null)
            {
                var previousSelectedItem = _foodItemUIs[_selectedFood];
                if (!previousSelectedItem.IsSelected)
                {
                    _selectedFood = null;
                }
            }
            
            if (state == ConstDefine.SELECTED)
            {
                if (_selectedFood != null && _selectedFood != food)
                {
                    var previousSelectedItem = _foodItemUIs[_selectedFood];
                    previousSelectedItem.VisualElement.RemoveState(ConstDefine.SELECTED);
                }

                _selectedFood = food;
                foodInfoUI.SetUpFood(food);
            }
            else if (state == ConstDefine.HOVER)
            {
                if (_selectedFood == null)
                {
                    foodInfoUI.SetUpFood(food);
                }
            }
        }
    }
}