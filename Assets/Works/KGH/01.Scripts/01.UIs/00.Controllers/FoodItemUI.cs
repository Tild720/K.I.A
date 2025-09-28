using Foods;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Controllers
{
    public class FoodItemUI : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI priceText;
        public void SetFood(FoodSO food)
        {
            iconImage.sprite = food.icon;
            nameText.text = food.foodName;
            priceText.text = food.price.ToString();
        }
    }
}