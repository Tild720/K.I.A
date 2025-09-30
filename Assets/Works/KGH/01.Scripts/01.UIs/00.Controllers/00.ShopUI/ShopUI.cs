using System;
using Code.Core.EventSystems;
using Core.EventSystem;
using UIs.Controllers.ShopUI.InfoUI;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Works.Tild.Code.Events;

namespace UIs.Controllers.ShopUI
{
    public class ShopUI : MonoBehaviour
    {
        public UnityEvent OnShopOpened;
        [SerializeField] private PopUpUI popUpUI;
        [SerializeField] private Canvas canvas;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        [SerializeField] private CinemachineCamera camera;
        [SerializeField] private int priorityWhenOpen = 15;

        private void Awake()
        {
            GameEventBus.AddListener<ChatEndedEvent>(OnChatEnded);
            GameEventBus.AddListener<PurchaseEvent>(OnPurchase);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<ChatEndedEvent>(OnChatEnded);
            GameEventBus.RemoveListener<PurchaseEvent>(OnPurchase);
        }

        private void OnPurchase(PurchaseEvent obj)
        {
            canvas.enabled = false;
            graphicRaycaster.enabled = false;
            camera.Priority.Value = -1;
        }

        private void OnChatEnded(ChatEndedEvent obj)
        {
            GameEventBus.RaiseEvent(UIEvents.FadeEvent.Initialize(() =>
            {
                canvas.enabled = true;
                graphicRaycaster.enabled = true;
                OnShopOpened?.Invoke();
                popUpUI.ShowPopUp(obj.Money, obj.NextRegion);
                camera.Priority.Value = priorityWhenOpen;
            }));
        }
    }
}