using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.Chat
{
    public class ChatBubble : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI message;
        [SerializeField] private RectTransform bubble;
        [SerializeField] private CanvasGroup chatGroup;

        public void Initialize(string msg)
        {
            message.SetText(msg);
            message.ForceMeshUpdate();
            int cnt = message.textInfo.lineCount;
            Debug.Log(cnt);
            if (cnt > 1)
            {
                Debug.Log("slashed");
                Vector2 size = bubble.sizeDelta;
                size.y = 120;
                bubble.sizeDelta = size;
            }

            bubble.localScale = Vector3.zero; 
            bubble.DOScale(Vector3.one, 0.3f);
        }

        public void Replace(float multiplier)
        {
            message.text = message.text.Replace("{multiplier}", multiplier.ToString());
        }
    }
}