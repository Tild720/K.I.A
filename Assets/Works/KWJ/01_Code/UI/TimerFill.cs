using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KWJ.UI
{
    public class TimerFill : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI timerText;
        
        [SerializeField] private float disableTime;
        
        private void OnEnable()
        {
            DisableTimer();
        }

        private async void DisableTimer()
        {
            await Awaitable.WaitForSecondsAsync(disableTime, destroyCancellationToken);
            gameObject.SetActive(false);
        }

        public void SetFill(float amount, float time)
        {
            image.fillAmount = amount;
            timerText.text = time.ToString("0.0");
        }
    }
}