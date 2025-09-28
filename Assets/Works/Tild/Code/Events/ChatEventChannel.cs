using System.Collections.Generic;
using Code.Chat;
using Code.Core.EventSystems;

namespace Works.Tild.Code.Events
{
    public static class ChatEventChannel
    {
        public static ChatEvent ChatEvent = new ChatEvent();
        public static ChoiceBtnEvent ChoiceBtnEvent = new ChoiceBtnEvent();
        public static ChoiceEvent ChoiceEvent = new ChoiceEvent();
    }
    public class ChoiceEvent : GameEvent
    {
        public List<Choice> choice;
        public ChoiceEvent Initializer(List<Choice> getChoice)
        {
            choice = getChoice;
            return this;
        }
    }
    public class ChoiceBtnEvent : GameEvent
    {
        public Choice choice;
        public ChoiceBtnEvent Initializer(Choice getChoice)
        {
            choice = getChoice;
            return this;
        }
    }

    public class ChatEvent : GameEvent
    {
        public Message message;
        public ChatEvent Initializer(Message getMessage)
        {
            message = getMessage;
            return this;
        }
    }
}