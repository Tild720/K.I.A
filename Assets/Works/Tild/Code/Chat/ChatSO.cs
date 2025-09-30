using System.Collections.Generic;
using Region;
using UnityEngine;

namespace Code.Chat
{
    [CreateAssetMenu(fileName = "Chat", menuName = "SO/Chat")]
    public class ChatSO : ScriptableObject
    {
        public List<Chat> Chats;

        public RegionSO Region;

        public List<Message> SuccessMessages;
        public List<Message> FailMessages;
    }
}