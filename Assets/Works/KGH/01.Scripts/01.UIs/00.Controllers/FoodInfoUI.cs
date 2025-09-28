using System.Collections.Generic;
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
    }
}