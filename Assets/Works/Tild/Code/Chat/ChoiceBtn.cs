using Code.Core.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Works.Tild.Code.Events;

namespace Code.Chat
{
    public class ChoiceBtn : MonoBehaviour
    {
        [SerializeField] private TMP_Text message;
        [SerializeField] private RectTransform bubble;
        private readonly ChoiceBtnEvent _choiceBtnEvent = ChatEventChannel.ChoiceBtnEvent;
        private Choice choice;

        public void Initialize(Choice getChoice)
        {
            choice = getChoice;
            message.text = getChoice.action;
            bubble.DOScale(Vector3.one, 0.1f);
        }

        public void Disappear()
        {
            bubble.DOScale(Vector3.zero, 0.15f).OnComplete(() =>
            {
                bubble.DOKill();
                Destroy(gameObject);
            });
            
        }
      
        public void OnClick()
        {
            GameEventBus.RaiseEvent(_choiceBtnEvent.Initializer(choice));
        }
    }
}