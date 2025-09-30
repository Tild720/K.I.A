using Core.Defines;
using Cysharp.Threading.Tasks;
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
        public VisualElement VisualElement
        {
            get
            {
                if (_visualElement == null)
                {
                    _visualElement = GetComponent<VisualElement>();
                }
                return _visualElement;
            }
        }

        private Button _button;
        public bool IsSelected => _visualElement.CurrentState == ConstDefine.SELECTED;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnSelectHandler);
        }

        private void OnSelectHandler()
        {
            if (IsSelected)
                VisualElement.RemoveState(ConstDefine.SELECTED).Forget();
            else
                VisualElement.AddState(ConstDefine.SELECTED, 20).Forget();
        }

        public void SetFood(FoodSO food)
        {
            iconImage.sprite = food.icon;
            nameText.text = food.foodName;
            priceText.text = food.price.ToString();
        }
    }
}