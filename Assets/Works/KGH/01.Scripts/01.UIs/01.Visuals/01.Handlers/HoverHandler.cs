using System;
using Core.Defines;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Visuals.Handlers
{
    public class HoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private VisualElement _visualElement;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _visualElement?.AddState(ConstDefine.HOVER, 10);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _visualElement?.RemoveState(ConstDefine.HOVER);
        }
    }
}