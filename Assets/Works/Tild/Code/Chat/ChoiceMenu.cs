using System;
using System.Collections.Generic;
using Code.Core.EventSystems;
using UnityEngine;

namespace Code.Chat
{
    public class ChoiceMenu : MonoBehaviour
    {
        public GameObject choiceParent;
        public ChoiceBtn choicePrefab;

        private void Awake()
        {
            GameEventBus.AddListener();
        }

        public void ChoiceHandler(List<string> choice)
        {
            foreach (var message in choice)
            {
                ChoiceBtn choiceBtn = Instantiate(choicePrefab, choiceParent.transform);
                choiceBtn.Initialize(message);
                
            }
        }
        
        
    }
}