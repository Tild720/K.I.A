using Cysharp.Threading.Tasks;
using Region;
using TMPro;
using UIs.Visuals;
using UnityEngine;

namespace UIs.Controllers.ShopUI.InfoUI
{
    public class PopUpUI : MonoBehaviour
    {
        [SerializeField] private VisualElement popUpElement;
        [SerializeField] private TextMeshProUGUI populationText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private float popUpDuration = 2f;
        [SerializeField] private InfoBarUI infoBar;
        private string _populationText;
        private string _healthText;
        private string _moneyText;
        
        private void Awake()
        {
            _populationText = populationText.text;
            _healthText = healthText.text;
            _moneyText = moneyText.text;
        }
        
        public void ShowPopUp(float getMoney, RegionSO nextRegion)
        {
            var currentRegion = nextRegion;
            if (currentRegion == null) return;
            SetUpInfo(currentRegion.population, currentRegion.health, Mathf.RoundToInt(getMoney));
            StartPopUp();
            infoBar.SetUpInfo(currentRegion.population, currentRegion.health, Mathf.RoundToInt(getMoney));
        }

        private void SetUpInfo(int population, int health, int money)
        {
            populationText.text = string.Format(_populationText, population);
            healthText.text = string.Format(_healthText, health);
            moneyText.text = string.Format(_moneyText, money);
        }
        
        private async void StartPopUp()
        {
            popUpElement.AddState("popUp", 20).Forget();
            await Awaitable.WaitForSecondsAsync(popUpDuration);
            popUpElement.RemoveState("popUp").Forget();
        }

#if UNITY_EDITOR
        [ContextMenu("ShowPopUp")]
        public void ShowPopUp()
        {
            var currentRegion = RegionManager.Instance.CurrentRegion;
            if (currentRegion == null) return;
            SetUpInfo(currentRegion.population, currentRegion.health, RegionManager.Instance.Money);
            StartPopUp();
            infoBar.SetUpInfo(currentRegion.population, currentRegion.health, RegionManager.Instance.Money);
        }
#endif
    }
}