using System;
using System.Collections;
using System.Collections.Generic;
using Code.Core.EventSystems;
using UnityEngine;
using Works.Tild.Code.Events;

namespace Code.Chat
{
    public class ChatManager : MonoBehaviour
    {
        [SerializeField] private List<ChatSO> chatLists; // 여러 ChatSO 관리
        [SerializeField] private ChatBubble playerBubble; 
        [SerializeField] private ChatBubble targetBubble; 
        [SerializeField] private ChatBubble alertBubble; 
        [SerializeField] private Transform bubbleParent; 
        private readonly ChoiceEvent _choiceEvent = ChatEventChannel.ChoiceEvent;
        
        private int _chatIndex = 0;
        private bool _isChoiced = false;

        private void Start()
        {
            StartChat();
        }

        private void Awake()
        {
            GameEventBus.AddListener<ChoiceBtnEvent>(OnChoiceBtnEvent);
        }

        private void OnChoiceBtnEvent(ChoiceBtnEvent obj)
        {
            StartCoroutine(ChoiceReply(obj.choice));
        }

        private IEnumerator ChoiceReply(Choice choice)
        {
            ChatBubble bubble = Instantiate(targetBubble, bubbleParent);
            bubble.Initialize(choice.message.message);
            yield return new WaitForSeconds(choice.message.delay);
            _isChoiced = true;
        }

        public void NextChat()
        {
            _chatIndex++;
            if (_chatIndex < chatLists.Count)
            {
                StartChat();
            }
            else
            {
                Debug.Log("모든 대화가 끝났습니다.");
            }
        }
        
        public void StartChat()
        {
            StopAllCoroutines(); 
            if (_chatIndex < chatLists.Count)
            {
                StartCoroutine(PlayChatCoroutine(chatLists[_chatIndex]));
            }
        }
    
        private IEnumerator PlayChatCoroutine(ChatSO chatSO)
        {
            for (int i = 0; i < chatSO.Chats.Count; i++)
            {
                Chat currentChat = chatSO.Chats[i];

              
                foreach (var msg in currentChat.Messages)
                {
                    ChatBubble bubble = null;

                    switch (currentChat.ChatType)
                    {
                        case ChatType.Player:
                            bubble = Instantiate(playerBubble, bubbleParent);
                            break;
                        case ChatType.Target:
                            bubble = Instantiate(targetBubble,bubbleParent);
                            break;
                        case ChatType.Alert:
                            bubble = Instantiate(alertBubble,bubbleParent);
                            break;
                    }

                    if (bubble != null)
                        bubble.Initialize(msg.message);
                    
                    
             
                    yield return new WaitForSeconds(msg.delay);
                    
                  
                   
                }
                if (currentChat.ChatType == ChatType.Target)
                {
                    GameEventBus.RaiseEvent(_choiceEvent.Initializer(currentChat.Choices));
                    yield return new WaitUntil(() => _isChoiced == true);
                    _isChoiced = false;
                }
            }
        }
    }
}
