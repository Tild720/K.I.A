using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Chat
{
    [Serializable]
    public enum ChatType
    {
        Player, Target, Alert
    }
    [Serializable]
    public struct Choice
    {
        public string action;
        public Message message;
        
        public int points;
        public string reply;
        
    }
    [Serializable]
    public struct Message
    {
        public string message;
        public float delay;
    }
    [Serializable]
    public class Chat
    {
        public ChatType ChatType;
        public List<Message> Messages;
        public List<Choice> Choices;
        
    }
}