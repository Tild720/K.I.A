using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.Chat
{
    public class ChoiceBtn : MonoBehaviour
    {
        [SerializeField] private TMP_Text message;
        [SerializeField] private RectTransform bubble;

        public void Initialize(string msg)
        {
            message.text = msg;
            bubble.DOScale(Vector3.one, 1);
        }
    }
}