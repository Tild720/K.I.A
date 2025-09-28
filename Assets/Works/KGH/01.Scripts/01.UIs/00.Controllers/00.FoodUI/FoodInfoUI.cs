using System.Collections.Generic;
using Foods;
using TMPro;
using UIs.Visuals;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace UIs.Controllers.FoodUI
{
    public class FoodInfoUI : MonoBehaviour
    {
        [Header("Info Ref")]
        [SerializeField] private Image icon; 
        [SerializeField] private TextMeshProUGUI foodNameText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI foodDescriptionText;
        [SerializeField] private Transform ingredientItemParent;
        [SerializeField] private GameObject ingredientItemPrefab;
        [Header("Counters Ref")]
        [SerializeField] private Button addButton;
        [SerializeField] private Button subtractButton;
        [SerializeField] private TMP_InputField countInputField;
        [Header("Confirm Ref")]
        [SerializeField] private Button confirmButton;
        
        private List<IngredientItemUI> _ingredientItems = new List<IngredientItemUI>();
        
        private VisualElement _addButtonVisualElement;
        private VisualElement _subtractButtonVisualElement;
        private VisualElement _confirmButtonVisualElement;
        
        private FoodSO _currentFood;
        
        private void Awake()
        {
            _addButtonVisualElement = addButton.GetComponent<VisualElement>();
            _subtractButtonVisualElement = subtractButton.GetComponent<VisualElement>();
            _confirmButtonVisualElement = confirmButton.GetComponent<VisualElement>();
            
            addButton.onClick.AddListener(OnAdd);
            subtractButton.onClick.AddListener(OnSubtract);
            confirmButton.onClick.AddListener(OnConfirm);
        }

        private void OnDestroy()
        {
            addButton.onClick.RemoveListener(OnAdd);
            subtractButton.onClick.RemoveListener(OnSubtract);
        }

        public void SetUpFood(FoodSO food)
        {
            _currentFood = food;
            
            icon.sprite = food.icon;
            foodNameText.text = food.foodName;
            priceText.text = food.price.ToString();
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
                var item = Instantiate(ingredientItemPrefab, ingredientItemParent);
                var ingredientItemUI = item.GetComponent<IngredientItemUI>();
                ingredientItemUI.SetIngredient(food.ingredient[i].icon, food.ingredient[i].ingredientName);
                _ingredientItems.Add(ingredientItemUI);
            }
            
            countInputField.text = "0";
        }

        public void OnAdd()
        {
            var count = int.Parse(countInputField.text);
            count++;
            countInputField.text = count.ToString();
            
            bool canPurchase = true; // TODO: Check if the player has enough resources to purchase
            if (count > 1)
            {
                _subtractButtonVisualElement.RemoveState("disabled");
                subtractButton.interactable = true;
                
                _confirmButtonVisualElement.RemoveState("disabled");
                confirmButton.interactable = true;
            }
            else
            {
                _subtractButtonVisualElement.AddState("disabled", 20);
                subtractButton.interactable = false;
            }
            
            if (!canPurchase)
            {
                _addButtonVisualElement.AddState("disabled", 20);
                addButton.interactable = false;
            }
            else
            {
                _addButtonVisualElement.RemoveState("disabled");
                addButton.interactable = true;
            }
        }
        public void OnSubtract()
        {
            var count = int.Parse(countInputField.text);
            if (count > 1)
            {
                count--;
                countInputField.text = count.ToString();
            }
            
            if (count > 1)
            {
                _subtractButtonVisualElement.RemoveState("disabled");
                subtractButton.interactable = true;
                
                _confirmButtonVisualElement.RemoveState("disabled");
                confirmButton.interactable = true;
            }
            else
            {
                _subtractButtonVisualElement.AddState("disabled", 20);
                subtractButton.interactable = false;
                
                _confirmButtonVisualElement.AddState("disabled", 10);
                confirmButton.interactable = false;
            }
        }

        public void OnConfirm()
        {
            //TODO: Confirm Purchase Logic
        }
    }
}