using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KWJ.UI
{
    public class TimerFill : MonoBehaviour
    {
        [SerializeField] private Image currentCookFill;
        [SerializeField] private Image insufficientCookFill;
        [SerializeField] private Image moderateCookFill;
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

        public void SetCookFills(float insufficient, float moderateCook)
        {
            insufficientCookFill.fillAmount = insufficient;
            moderateCookFill.fillAmount = moderateCook;
        }

        public void SetCookFill(float amount, float time)
        {
            currentCookFill.fillAmount = amount;
            timerText.text = time.ToString("0.0");
        }
    }
}