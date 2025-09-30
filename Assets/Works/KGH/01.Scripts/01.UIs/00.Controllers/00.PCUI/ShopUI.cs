using System;
using Code.Core.EventSystems;
using UIs.Controllers.ShopUI.InfoUI;
using UnityEngine;
using UnityEngine.Events;
using Works.Tild.Code.Events;

namespace UIs.Controllers.ShopUI
{
    public class ShopUI : MonoBehaviour
    {
        public UnityEvent OnShopOpened;
        [SerializeField] private PopUpUI popUpUI;
        private void Awake()
        {
            GameEventBus.AddListener<ChatEndedEvent>(OnChatEnded);
        }
        private void OnDestroy()
        {
            GameEventBus.RemoveListener<ChatEndedEvent>(OnChatEnded);
        }

        private void OnChatEnded(ChatEndedEvent obj)
        {
            OnShopOpened?.Invoke();
            popUpUI.ShowPopUp(obj.Money, obj.NextRegion);
        }
    }
}