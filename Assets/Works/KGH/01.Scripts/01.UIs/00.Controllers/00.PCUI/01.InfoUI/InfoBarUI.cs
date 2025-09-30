using TMPro;
using UnityEngine;

namespace UIs.Controllers.ShopUI.InfoUI
{
    public class InfoBarUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI populationText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI moneyText;

        public void SetUpInfo(int population, int health, int money)
        {
            populationText.text = population.ToString();
            healthText.text = health.ToString();
            moneyText.text = money.ToString();
        }
    }
}