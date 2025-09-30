using Core.Defines;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UIs.Visuals.Effects
{
    public class UICanvasGroupState : MonoBehaviour, IUIState
    {
        [SerializeField] private string stateName = "default";
        [SerializeField] private StructDefines.TransitionData transition = new() { duration = 0.2f, ease = Ease.OutSine };
        public string StateName => stateName;
        public float targetAlpha = 1f;
        
        private CanvasGroup _target;
        public void Initialize(VisualElement owner)
        {
            _target = owner.GetComponent<CanvasGroup>();
            if (_target == null)
                Debug.LogWarning($"[UICanvasGroupState] No CanvasGroup component found on {owner.name}");
        }

        public UniTask PlayEffect()
        {
            var previousAlpha = _target.alpha;
            DOVirtual.Float(previousAlpha, targetAlpha, transition.duration, value => _target.alpha = value).SetEase(transition.ease).SetUpdate(true);
            return UniTask.WaitForSeconds(transition.duration, true);
        }
    }
}