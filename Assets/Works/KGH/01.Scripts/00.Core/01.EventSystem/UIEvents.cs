using Code.Core.EventSystems;
using Core.Defines;

namespace Core.EventSystem
{
    public static class UIEvents
    {
        public static UIChangeEvent UIChangeEvent = new UIChangeEvent();
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
}