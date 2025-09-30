using Core.Defines;
using Foods;
using TMPro;
using UIs.Visuals;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace UIs.Controllers.ShopUI.FoodUI
{
    public class FoodItemUI : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI priceText;
        private VisualElement _visualElement;
        public VisualElement VisualElement => _visualElement ??= GetComponent<VisualElement>();
        private Button _button;
        public bool IsSelected => _visualElement.CurrentState == ConstDefine.SELECTED;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnSelectHandler);

            _visualElement = GetComponent<VisualElement>();
        }

        private void OnSelectHandler()
        {
            if (IsSelected)
                _ = VisualElement.RemoveState(ConstDefine.SELECTED);
            else
                _ = VisualElement.AddState(ConstDefine.SELECTED, 20);
        }

        public void SetFood(FoodSO food)
        {
            iconImage.sprite = food.icon;
            nameText.text = food.foodName;
            priceText.text = food.price.ToString();
        }
    }
}