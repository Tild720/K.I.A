
using System;
using System.Collections;
using System.Text;
using Code.Core.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Works.Tild.Code.Events;

namespace Works.Tild.Code.Result
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private TMP_Text usedMoneyText;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text diedText;
        [SerializeField] private TMP_Text starText;
        [SerializeField] private RectTransform bar;
        [SerializeField] private CanvasGroup initGroup;
        [SerializeField] private CanvasGroup uiGroup;
        [SerializeField] private Image fadeImage;

        private void Awake()
        {
            GameEventBus.AddListener<ResultEvent>(ResultHandler);
        }
        private void ResultHandler(ResultEvent obj)
        {
            Debug.LogError("erorr");
            Debug.Log(obj.Money);
            StopAllCoroutines();
            
            // Healthfixed : 약 10~50
            int score = (int)(obj.HealthFixed / 3) - (obj.Died * 3);
            TrustManager.Instance.AddTrust(score);
            
            StartCoroutine(ShowResultCoroutine(obj, 4));
        }

        private IEnumerator ShowResultCoroutine(ResultEvent obj, int star)
        {
         
            uiGroup.DOFade(1, 0.3f); 
            uiGroup.blocksRaycasts = true;

            yield return new WaitForSeconds(2f);
            initGroup.DOFade(1, 0.1f);
            
            bar.DOScaleY(1.04f, 0.3f);
            yield return StartCoroutine(TypeText(moneyText, $"예산 {string.Format("{0:#,###}", obj.Money)}$"));
            yield return new WaitForSeconds(1.5f);

            bar.DOScaleY(1.64f, 0.3f);
            yield return StartCoroutine(TypeText(usedMoneyText, $"지출 {string.Format("{0:#,###}", obj.Used)}$"));
            yield return new WaitForSeconds(1.5f);

         
            string healthStr = $"건강 상태 {string.Format("{0:#,###}", obj.Health)}%({obj.HealthFixed})";
            bar.DOScaleY(2.17f, 0.3f);
            yield return StartCoroutine(TypeText(healthText, healthStr));
            healthText.color = obj.HealthFixed < 0 ? Color.red : Color.green;
            yield return new WaitForSeconds(1.5f);

         
            string diedStr = $"사망자 수 {obj.Died}명";
            bar.DOScaleY(2.83f, 0.3f);
            yield return StartCoroutine(TypeText(diedText, diedStr));
            diedText.color = obj.Died >= 5 ? Color.red : Color.green;
            yield return new WaitForSeconds(1.5f);

      
            string starStr = GetStarText(3);
            bar.DOScaleY(3.98f, 0.3f);
            yield return StartCoroutine(TypeText(starText, starStr));

            fadeImage.DOFade(1, 0.3f).OnComplete(() =>
            {
                initGroup.DOFade(0, 0.3f);
                bar.DOScaleY(0.49f, 0f);
                diedText.text = "";
                starText.text = "";
                healthText.text = "";
                moneyText.text = "";
                usedMoneyText.text = "";
                fadeImage.DOFade(0, 0.3f).SetDelay(2f);
                uiGroup.DOFade(0, 0.3f).SetDelay(2f).OnComplete((() =>
                {
                    uiGroup.blocksRaycasts = true;
                }));
            });
        }

        private IEnumerator TypeText(TMP_Text textComp, string fullText)
        {
            textComp.text = "";
            StringBuilder sb = new StringBuilder();

            foreach (char c in fullText)
            {
                sb.Append(c);
                textComp.text = sb.ToString();
                yield return new WaitForSeconds(0.07f); 
            }
        }

        private string GetStarText(int count)
        {
            count = Mathf.Clamp(count, 0, 5);
            string filled = new string('\u2605', count); // ★
            string empty = new string('\u2606', 5 - count); // ☆
            return filled + empty;
        }
    }
}
