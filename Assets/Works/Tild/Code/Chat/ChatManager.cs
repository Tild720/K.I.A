using System;
using System.Collections;
using System.Collections.Generic;
using Code.Core.EventSystems;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
        [SerializeField] private ScrollRect chatScrollRect;
        [SerializeField] private CanvasGroup chatGroup;
        private readonly ChoiceEvent _choiceEvent = ChatEventChannel.ChoiceEvent;
        private readonly ChatEndedEvent _chatEndedEvent = ChatEventChannel.ChatEndedEvent;
<<<<<<< Updated upstream
=======
        private readonly ResultEndEvent ResultEvent = ScoreEventChannel.ResultEndEvent;
>>>>>>> Stashed changes
        
        private int _chatIndex = 0;
        private bool _isChoiced = false;

        private float currentMoney;
        
        public int Point { get; set; }

        private void Start()
        {
            StartChat();
        }
        private void ScrollToBottomSmooth()
        {
          
            StartCoroutine(ScrollDelayed());
        }

        private IEnumerator ScrollDelayed()
        {
            yield return null; 
            Canvas.ForceUpdateCanvases();
            chatScrollRect.DOVerticalNormalizedPos(0f, 0.2f);
        }
        private void Awake()
        {
            GameEventBus.AddListener<ChoiceBtnEvent>(OnChoiceBtnEvent); 
<<<<<<< Updated upstream
=======
            GameEventBus.AddListener<ResultEndEvent>(NextChat); 
       
>>>>>>> Stashed changes
        }

        private void OnChoiceBtnEvent(ChoiceBtnEvent obj)
        {
            StartCoroutine(ChoiceReply(obj.choice));
        }

        private IEnumerator ChoiceReply(Choice choice)
        {
            Debug.Log(choice.action);
            ChatBubble plrBubble = Instantiate(playerBubble, bubbleParent);
            plrBubble.Initialize(choice.message.message);
            ScrollToBottomSmooth();
            yield return new WaitForSeconds(choice.message.delay);

            if (choice.action == "예산 요청")
            {
                Debug.Log("예산요청");
                int rand = UnityEngine.Random.Range(0, 100);
                bool isSuccess = rand < TrustManager.Instance.Trust;

                if (isSuccess)
                {

                    Message successMessage = chatLists[_chatIndex]
                        .SuccessMessages[UnityEngine.Random.Range(0, chatLists[_chatIndex].SuccessMessages.Count)];
                    
                    ChatBubble bubble = Instantiate(targetBubble, bubbleParent);
                    bubble.Initialize(successMessage.message);
                    ScrollToBottomSmooth();
                
                    float multiplier = UnityEngine.Random.Range(1.3f, 2f);
                    multiplier = Mathf.Round(multiplier * 100f) / 100f;
                    bubble.Replace(multiplier);


                    int trustLoss = Mathf.CeilToInt(10f * multiplier); 
                    TrustManager.Instance.RemoveTrust(trustLoss);


                    currentMoney *= multiplier;

                    yield return new WaitForSeconds(successMessage.delay);
                }
                else
                {
               

                    Message failMessage = chatLists[_chatIndex]
                        .FailMessages[UnityEngine.Random.Range(0, chatLists[_chatIndex].FailMessages.Count)];
                    
                    ChatBubble bubble = Instantiate(targetBubble, bubbleParent);
                    bubble.Initialize(failMessage.message);
                    ScrollToBottomSmooth();


                    TrustManager.Instance.RemoveTrust(UnityEngine.Random.Range(5, 10));
                    
                    yield return new WaitForSeconds(failMessage.delay);
                }


                Choice leave = new Choice();
                leave.action = "나가기";
                leave.message.message = "출발하겠습니다.";
                leave.message.delay = 3f;
                GameEventBus.RaiseEvent(_choiceEvent.Initializer(new List<Choice> { choice, leave }));
            }
            else
            {
                Debug.Log("기타.." + choice.action);
                ChatBubble bubble = Instantiate(targetBubble, bubbleParent);
                bubble.Initialize("행운을 빕니다.");
                ScrollToBottomSmooth();
                yield return new WaitForSeconds(choice.message.delay);

                Debug.Log("ChatEnded");
                _isChoiced = true;
                GameEventBus.RaiseEvent(_chatEndedEvent.Initializer(currentMoney, chatLists[_chatIndex].Region));
                chatGroup.DOFade(0, 1);
                yield return new WaitForSeconds(1); 
                foreach (Transform child in bubbleParent)
                {
                    Destroy(child.gameObject);
                }

                    
                    chatGroup.blocksRaycasts = false;
                    Debug.Log("CCC");
                
            }
        }


<<<<<<< Updated upstream
        public void NextChat()
=======
        public void NextChat(ResultEndEvent evt)
>>>>>>> Stashed changes
        {
            Debug.Log("NextChat");
            _chatIndex++;
            if (_chatIndex < chatLists.Count)
            {
<<<<<<< Updated upstream
           
                StartChat();
=======
                
                StartCoroutine(DelayedChat());
>>>>>>> Stashed changes
            }
            else
            {
                chatGroup.DOFade(1, 1).OnComplete(() =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                });
                ;
                Debug.Log("모든 대화가 끝났습니다.");
            }
        }
<<<<<<< Updated upstream
=======

        IEnumerator DelayedChat()
        {
            yield return new WaitForSeconds(6);
            StartChat();
        }
>>>>>>> Stashed changes
        
        public void StartChat()
        {
            GameEventBus.RaiseEvent(_choiceEvent.Initializer(new List<Choice>()));
            chatGroup.DOFade(1, 1).OnComplete(() =>
            {
                currentMoney = chatLists[_chatIndex].Region.Money;
                StopAllCoroutines(); 
                if (_chatIndex < chatLists.Count)
                {
                    StartCoroutine(PlayChatCoroutine(chatLists[_chatIndex]));
                }
                chatGroup.blocksRaycasts = true;
            });
         
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
                    ScrollToBottomSmooth();
                    
             
                    yield return new WaitForSeconds(msg.delay);
                    
                  
                   
                }
                if (currentChat.ChatType == ChatType.Target && currentChat.Choices.Count > 0)
                {
                    Debug.Log("ChoiceEvent");
                    GameEventBus.RaiseEvent(_choiceEvent.Initializer(currentChat.Choices));
                    yield return new WaitUntil(() => _isChoiced == true);
                    _isChoiced = false;
                }
            }
        }
    }
}
