using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Foods;
using TMPro;
using UIs.Visuals;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace UIs.Controllers.ShopUI.FoodUI
{
    public class FoodInfoUI : MonoBehaviour
    {
        [Header("Info Ref")] [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI foodNameText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI foodDescriptionText;
        [SerializeField] private Transform ingredientItemParent;
        [SerializeField] private GameObject ingredientItemPrefab;

        [Header("Counters Ref")] [SerializeField]
        private Button addButton;

        [SerializeField] private Button subtractButton;
        [SerializeField] private TMP_InputField countInputField;

        [Header("Confirm Ref")] [SerializeField]
        private Button confirmButton;
        [SerializeField] private ConfirmPopUp confirmPopUp;

        private List<IngredientItemUI> _ingredientItems = new List<IngredientItemUI>();

        private VisualElement _addButtonVisualElement;
        private VisualElement _subtractButtonVisualElement;
        private VisualElement _confirmButtonVisualElement;

        private FoodSO _currentFood;
        
        private int Count => int.TryParse(countInputField.text, out int value) ? value : 0;
        public int Money;
        
        private void Awake()
        {
            _addButtonVisualElement = addButton.GetComponent<VisualElement>();
            _subtractButtonVisualElement = subtractButton.GetComponent<VisualElement>();
            _confirmButtonVisualElement = confirmButton.GetComponent<VisualElement>();

            addButton.onClick.AddListener(OnAdd);
            subtractButton.onClick.AddListener(OnSubtract);
            confirmButton.onClick.AddListener(OnConfirm);

            countInputField.onEndEdit.AddListener(CheckValidInput);
            countInputField.onValueChanged.AddListener(CheckMaxCharacters);
        }

        private void OnDestroy()
        {
            addButton.onClick.RemoveListener(OnAdd);
            subtractButton.onClick.RemoveListener(OnSubtract);
            confirmButton.onClick.RemoveListener(OnConfirm);
            countInputField.onEndEdit.RemoveListener(CheckValidInput);
            countInputField.onValueChanged.RemoveListener(CheckMaxCharacters);
        }

        private void CheckMaxCharacters(string arg0)
        {
            if (arg0.Length > 3)
            {
                countInputField.text = arg0.Substring(0, 3);
            }
        }

        private void CheckValidInput(string str)
        {
            if (!int.TryParse(str, out int value) || value < 0)
            {
                countInputField.text = "0";
                return;
            }

            int maxBuyable = 99; // TODO: Calculate based on player's resources
            if (value > maxBuyable)
            {
                countInputField.text = maxBuyable.ToString();
                value = maxBuyable;
            }
            else
            {
                countInputField.text = value.ToString();
            }

            CheckButton();
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
            
            CheckButton();
        }

        public void OnAdd()
        {
            var count = Count;
            count++;
            countInputField.text = count.ToString();

            CheckButton();
        }

        public void OnSubtract()
        {
            var count = Count;
            if (Count > 0)
            {
                count--;
                countInputField.text = count.ToString();
            }

            CheckButton();
        }

        private void CheckButton()
        {
            if (Count <= 0)
            {
                _subtractButtonVisualElement.AddState("disabled", 20).Forget();
                subtractButton.interactable = false;

                _confirmButtonVisualElement.AddState("disabled", 10).Forget();
                confirmButton.interactable = false;
            }
            else
            {
                _subtractButtonVisualElement.RemoveState("disabled").Forget();
                subtractButton.interactable = true;

                _confirmButtonVisualElement.RemoveState("disabled").Forget();
                confirmButton.interactable = true;
            }
            
            bool canPurchaseMore = Money >= Count * _currentFood.price;
            if (!canPurchaseMore)
            {
                _addButtonVisualElement.AddState("disabled", 20).Forget();
                addButton.interactable = false;
            }
            else
            {
                _addButtonVisualElement.RemoveState("disabled").Forget();
                addButton.interactable = true;
            }
        }

        public void OnConfirm()
        {
            if (Count <= 0) return;
            confirmPopUp.SetUp(_currentFood, Count);
        }
    }
}