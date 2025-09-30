using System;
using Code.Core.EventSystems;
using Core.Defines;

namespace Core.EventSystem
{
    public static class UIEvents
    {
        public static UIChangeEvent UIChangeEvent = new UIChangeEvent();
        public static FadeEvent FadeEvent = new FadeEvent();
    }
    public class UIChangeEvent : GameEvent
    {
        public EnumDefines.UIType UIType;
        public bool DoesFade;

        public UIChangeEvent Initialize(EnumDefines.UIType uiType, bool doesFade)
        {
            UIType = uiType;
            DoesFade = doesFade;
            
            return this;
        }
    }
    public class FadeEvent : GameEvent
    {
        public Action onComplete;
        public FadeEvent Initialize(Action onComplete)
        {
            this.onComplete = onComplete;
            return this;
        }
    }
}