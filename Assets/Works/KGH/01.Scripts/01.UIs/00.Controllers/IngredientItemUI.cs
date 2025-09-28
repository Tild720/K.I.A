using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Controllers
{
    public class IngredientItemUI : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;
        
        public void SetIngredient(Sprite icon, string name)
        {
            iconImage.sprite = icon;
            nameText.text = name;
        }
    }
}