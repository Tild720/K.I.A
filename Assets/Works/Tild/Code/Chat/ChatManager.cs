using System;
using System.Collections;
using System.Collections.Generic;
using Code.Core.EventSystems;
using UnityEngine;
using Works.Tild.Code;
using Works.Tild.Code.Events;
using Random = System.Random;

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

        private float currentMoney;
        
        public int Point { get; set; }

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
            TrustManager.Instance.AddTrust(choice.points);
            ChatBubble plrBubble = Instantiate(playerBubble, bubbleParent);
            plrBubble.Initialize(choice.message.message);
            yield return new WaitForSeconds(choice.message.delay);
            
            
            if (choice.action == "예산 요청")
            {

                int rand = UnityEngine.Random.Range(0, 100);
                bool isSuccess = rand < TrustManager.Instance.Trust;

                if (isSuccess)
                {
                    Message successMessage = chatLists[_chatIndex]
                        .SuccessMessages[UnityEngine.Random.Range(0, chatLists[_chatIndex].SuccessMessages.Count)];
                    ChatBubble bubble = Instantiate(targetBubble, bubbleParent);
                    bubble.Initialize(successMessage.message);
                    
                    float multiplier = UnityEngine.Random.Range(1, 2f);
                    int decimalPlaces = 2;
                    float result = Mathf.Round(multiplier * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);
                    bubble.Replace(result);
                    
                    TrustManager.Instance.RemoveTrust(UnityEngine.Random.Range(10,20));
                    currentMoney *= result;
                    
                    
                    yield return new WaitForSeconds(successMessage.delay);
                    _isChoiced = true; 

                }
                else
                {
                    Message failMessage = chatLists[_chatIndex]
                        .FailMessages[UnityEngine.Random.Range(0, chatLists[_chatIndex].FailMessages.Count)];
                    ChatBubble bubble = Instantiate(targetBubble, bubbleParent);
                    bubble.Initialize(failMessage.message);
                    
                    TrustManager.Instance.RemoveTrust(UnityEngine.Random.Range(0,10));
                    yield return new WaitForSeconds(failMessage.delay);
                    _isChoiced = true; 
                }
            }
            else
            {
                TrustManager.Instance.AddTrust(choice.points);
                ChatBubble bubble = Instantiate(targetBubble, bubbleParent);
                bubble.Initialize(choice.reply);
                yield return new WaitForSeconds(choice.message.delay);
                _isChoiced = true;  
            };
   
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
            currentMoney = chatLists[_chatIndex].Region.Money;
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
                if (currentChat.ChatType == ChatType.Target && currentChat.Choices.Count > 0)
                {
                    GameEventBus.RaiseEvent(_choiceEvent.Initializer(currentChat.Choices));
                    yield return new WaitUntil(() => _isChoiced == true);
                    _isChoiced = false;
                }
            }
        }
    }
}
