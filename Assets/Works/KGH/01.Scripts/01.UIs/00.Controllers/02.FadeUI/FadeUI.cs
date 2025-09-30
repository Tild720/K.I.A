using Code.Core.EventSystems;
using Core.EventSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Controllers.FadeUI
{
    public class FadeUI : BaseUI
    {
        private void Awake()
        {
            GameEventBus.AddListener<FadeEvent>(OnFadeEvent);
        }
        private void OnDestroy()
        {
            GameEventBus.RemoveListener<FadeEvent>(OnFadeEvent);
        }

        private void OnFadeEvent(FadeEvent obj)
        {
            _ = OnFadeEventTask(obj);
        }
        private async UniTaskVoid OnFadeEventTask(FadeEvent evt)
        {
            await Show();
            evt.onComplete?.Invoke();
            await Hide();
        }
    }
}