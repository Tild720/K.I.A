using System.Collections.Generic;
using Foods;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Controllers
{
    public class FoodInfoUI : MonoBehaviour
    {
        [SerializeField] private Image icon; 
        [SerializeField] private TextMeshProUGUI foodNameText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI effectOnPeopleText;
        [SerializeField] private TextMeshProUGUI effectOnRateText;
        [SerializeField] private TextMeshProUGUI foodDescriptionText;
        [SerializeField] private GameObject ingredientItemPrefab;
        private List<IngredientItemUI> _ingredientItems = new List<IngredientItemUI>();
        
        private string _effectOnPeopleFormat;
        private string _effectOnRateFormat;
        
        private void Awake()
        {
            _effectOnPeopleFormat = effectOnPeopleText.text;
            _effectOnRateFormat = effectOnRateText.text;
        }

        public void SetUpFood(FoodSO food)
        {
            icon.sprite = food.icon;
            foodNameText.text = food.foodName;
            priceText.text = food.price.ToString();
            effectOnPeopleText.text = string.Format(_effectOnPeopleFormat, food.refugeePoint);
            effectOnRateText.text = string.Format(_effectOnRateFormat, food.governmentPoint);
            foodDescriptionText.text = food.description;

            for (int i = 0; i < _ingredientItems.Count; i++)
            {
                if (i < food.ingredient.Length)
                {
                    _ingredientItems[i].gameObject.SetActive(true);
                    _ingredientItems[i].SetIngredient(food.ingredient[i].icon, food.ingredient[i].ingredientName);
                }
                else
                {
                    _ingredientItems[i].gameObject.SetActive(false);
                }
            }

            for (int i = _ingredientItems.Count; i < food.ingredient.Length; i++)
            {
                var item = Instantiate(ingredientItemPrefab, ingredientItemPrefab.transform.parent);
                var ingredientItemUI = item.GetComponent<IngredientItemUI>();
                ingredientItemUI.SetIngredient(food.ingredient[i].icon, food.ingredient[i].ingredientName);
                _ingredientItems.Add(ingredientItemUI);
            }
        }
    }
}