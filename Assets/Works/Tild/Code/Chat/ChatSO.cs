using System.Collections.Generic;
using UnityEngine;

namespace Code.Chat
{
    [CreateAssetMenu(fileName = "Chat", menuName = "SO/Chat")]
    public class ChatSO : ScriptableObject
    {
        public List<Chat> Chats;
        
    }
}