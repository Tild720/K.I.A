using System;
using UnityEngine;

namespace Code.Chat
{
    [Serializable]
    public enum ChatType
    {
        Player, Target, Alert
    }
    [Serializable]
    public struct ChatPoints
    {
        public int points;
        
        
    }

    [Serializable]
    public struct ChatChoices : IChat
    {
        public ChatPoints point;
        public string reply;
        public ChatType ChatType { get; }
        public Message Message { get; }
    }
}