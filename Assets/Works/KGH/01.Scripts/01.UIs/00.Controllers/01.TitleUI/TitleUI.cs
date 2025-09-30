using Code.Core.EventSystems;
using Core.Defines;
using Core.EventSystem;
using UnityEngine;

namespace Controllers.TitleUI
{
    public class TitleUI : BaseUI
    {
        public void OnStartButtonClicked()
        {
            GameEventBus.RaiseEvent(UIEvents.UIChangeEvent.Initialize(EnumDefines.UIType.NONE, true));
        }
        public void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}