using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Chat
{
    public class ChatManager : MonoBehaviour
    {
        [SerializeField] private List<ChatSO> chatLists; // 여러 ChatSO 관리
        [SerializeField] private ChatBubble playerBubble; 
        [SerializeField] private ChatBubble targetBubble; 
        [SerializeField] private ChatBubble alertBubble; 
        
        private int _chatIndex = 0;
        private bool _isChoiced = false;

        private void Start()
        {
            StartChat();
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
            StopAllCoroutines(); // 진행 중인 대화 중단
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

                // 여러 메시지를 순서대로 출력
                foreach (var msg in currentChat.Messages)
                {
                    ChatBubble bubble = null;

                    switch (currentChat.ChatType)
                    {
                        case ChatType.Player:
                            bubble = Instantiate(playerBubble);
                            break;
                        case ChatType.Target:
                            bubble = Instantiate(targetBubble);
                            break;
                        case ChatType.Alert:
                            bubble = Instantiate(alertBubble);
                            break;
                    }

                    if (bubble != null)
                        bubble.Initialize(msg.message);

             
                    yield return new WaitForSeconds(msg.delay);

                  
                    yield return new WaitUntil(() => _isChoiced == true);
                    _isChoiced = false;
                }
            }
        }
    }
}
