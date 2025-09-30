using System;
using Code.Core.EventSystems;
using Core.EventSystem;
using Cysharp.Threading.Tasks;
using Foods;
using TMPro;
using UIs.Visuals;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Controllers.ShopUI.FoodUI
{
    public class ConfirmPopUp : MonoBehaviour
    {
        [SerializeField] private VisualElement popupRoot;
        [Header("food")]
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI foodNameText;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private TextMeshProUGUI totalPriceText; 
        [Header("Buttons")]
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button confirmButton;
        
        private string _countFormat;
        
        private FoodSO _currentFood;
        private int count;

        private void Awake()
        {
            _countFormat = countText.text;
            cancelButton.onClick.AddListener(OnCancel);
            confirmButton.onClick.AddListener(OnConfirm);
        }
        
        private void OnDestroy()
        {
            cancelButton.onClick.RemoveListener(OnCancel);
            confirmButton.onClick.RemoveListener(OnConfirm);
        }

        public void SetUp(FoodSO food, int count)
        {
            _currentFood = food;
            this.count = count;
            icon.sprite = food.icon;
            foodNameText.text = food.foodName;
            countText.text = string.Format(_countFormat, count, food.price);
            totalPriceText.text = (food.price * count).ToString();
            popupRoot.AddState("show", 20).Forget();
        }

        private void OnCancel()
        {
            popupRoot.RemoveState("show").Forget();
        }

        private void OnConfirm()
        {
            popupRoot.RemoveState("show").Forget();
            GameEventBus.RaiseEvent(PurchaseEvents.PurchaseEvent.Initialize(_currentFood, count));
            GameEventBus.RaiseEvent(PurchaseEvents.UseMoneyEvent.Initialize(_currentFood.price * count));
        }
    }
}