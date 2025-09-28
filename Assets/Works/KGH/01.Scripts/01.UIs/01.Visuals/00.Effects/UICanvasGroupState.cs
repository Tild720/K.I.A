using DG.Tweening;
using UnityEngine;

namespace UIs.Visuals.Effects
{
    public class UICanvasGroupState : MonoBehaviour, IUIState
    {
        [SerializeField] private string stateName = "default";
        public string StateName => stateName;
        public float targetAlpha = 1f;
        
        private CanvasGroup _target;
        public void Initialize(VisualElement owner)
        {
            _target = owner.GetComponent<CanvasGroup>();
            if (_target == null)
                Debug.LogWarning($"[UICanvasGroupState] No CanvasGroup component found on {owner.name}");
        }

        public void PlayEffect()
        {
            var previousAlpha = _target.alpha;
            DOVirtual.Float(previousAlpha, targetAlpha, 0.2f, value => _target.alpha = value).SetEase(Ease.OutSine).SetUpdate(true);
        }
    }
}