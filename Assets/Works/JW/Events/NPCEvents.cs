using Code.Core.EventSystems;

namespace Works.JW.Events
{
    public static class NPCEvents
    {
        public static readonly NPCLineEndEvent NpcLineEndEvent = new NPCLineEndEvent();
        public static readonly NPCDeadEvent NPCDeadEvent = new NPCDeadEvent();
    }

    public class NPCLineEndEvent : GameEvent
    { }
    
    public class NPCDeadEvent : GameEvent
    { }
}