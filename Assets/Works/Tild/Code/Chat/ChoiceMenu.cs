using System;
using System.Collections.Generic;
using Code.Core.EventSystems;
using UnityEngine;
using Works.Tild.Code.Events;

namespace Code.Chat
{
    public class ChoiceMenu : MonoBehaviour
    {
        [SerializeField] private GameObject choiceParent;
        [SerializeField] private ChoiceBtn choicePrefab;
        private readonly ChatEvent chatEvent = ChatEventChannel.ChatEvent;
        private List<ChoiceBtn> choices = new List<ChoiceBtn>();

        private void Awake()
        {
            GameEventBus.AddListener<ChoiceBtnEvent>(PressedHandler);
            GameEventBus.AddListener<ChoiceEvent>(ChoiceHandler);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<ChoiceBtnEvent>(PressedHandler);
            GameEventBus.RemoveListener<ChoiceEvent>(ChoiceHandler);
        }

        private void PressedHandler(ChoiceBtnEvent obj)
        {
            foreach (ChoiceBtn choice in choices)
            {
                choice.Disappear();
            }
            GameEventBus.RaiseEvent(chatEvent);
        }

        public void ChoiceHandler(ChoiceEvent obj)
        {
            choices = new List<ChoiceBtn>();
            foreach (var message in obj.choice)
            {
            
                ChoiceBtn choiceBtn = Instantiate(choicePrefab, choiceParent.transform);
                choices.Add(choiceBtn);
                choiceBtn.Initialize(message);
            }
        }
        
        
    }
}   