using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Works.JW.Code.UI
{
    public class EndingUI : MonoBehaviour
    {
        [SerializeField,TextArea] private string mainLine;
        [SerializeField] private float mainLineAnimationTime;
        [SerializeField] private TextMeshProUGUI mainTextUI;
        [SerializeField] private Image image;

        private WaitForSeconds _mainWait;
        
        private void Awake()
        {
            _mainWait = new WaitForSeconds(mainLineAnimationTime);
            image.gameObject.SetActive(false);

            StartCoroutine(TextAnimation(mainTextUI, mainLine, _mainWait, () =>
            {
                image.gameObject.SetActive(true);
            }));
        }

        public void Exit()
        {
            Application.Quit();
            Debug.Log("나가짐");
        }

        private IEnumerator TextAnimation(TextMeshProUGUI ui, string line, WaitForSeconds wait, Action endCallback = null)
        {
            ui.SetText(line);
            ui.maxVisibleCharacters = 0;
            
            for (int i = 0; i < line.Length; i++)
            {
                yield return wait; 
                ui.maxVisibleCharacters++;
            }

            yield return wait;
            endCallback?.Invoke();
        }
    }
}