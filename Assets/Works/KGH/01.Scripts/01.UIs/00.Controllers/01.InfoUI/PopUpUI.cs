using Region;
using TMPro;
using UIs.Visuals;
using UnityEngine;

namespace UIs.Controllers.InfoUI
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
        
        private RegionManager RegionManager => RegionManager.Instance;
        
        private void Awake()
        {
            _populationText = populationText.text;
            _healthText = healthText.text;
            _moneyText = moneyText.text;
        }
        
        [ContextMenu("ShowPopUp")]
        public void ShowPopUp()
        {
            var currentRegion = RegionManager.CurrentRegion;
            if (currentRegion == null) return;
            SetUpInfo(currentRegion.population, currentRegion.health, RegionManager.Money);
            StartPopUp();
            infoBar.SetUpInfo(currentRegion.population, currentRegion.health, RegionManager.Money);
        }

        private void SetUpInfo(int population, int health, int money)
        {
            populationText.text = string.Format(_populationText, population);
            healthText.text = string.Format(_healthText, health);
            moneyText.text = string.Format(_moneyText, money);
        }
        
        private async void StartPopUp()
        {
            _ =popUpElement.AddState("popUp", 20);
            await Awaitable.WaitForSecondsAsync(popUpDuration);
            _ = popUpElement.RemoveState("popUp");
        }
    }
}