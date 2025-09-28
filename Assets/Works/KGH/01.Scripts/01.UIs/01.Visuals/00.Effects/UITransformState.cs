using Core.Defines;
using DG.Tweening;
using UnityEngine;

namespace UIs.Visuals.Effects
{
    public class UITransformState : MonoBehaviour, IUIState
    {
        [SerializeField] private string stateName = "default";
        public string StateName => stateName;
        [SerializeField] private Vector2 targetScale = Vector2.one;
        [SerializeField] private StructDefines.TransitionData transition = new StructDefines.TransitionData
        {
            duration = 0.2f,
            ease = Ease.OutSine
        };

        private RectTransform _target;

        public void Initialize(VisualElement owner)
        {
            _target = owner.GetComponent<RectTransform>();
            if (_target == null)
                Debug.LogWarning($"[UITransformState] No RectTransform component found on {owner.name}");
        }

        public void PlayEffect<T>(T before) where T : IUIState
        {
            _target?.DOScale(targetScale, transition.duration).SetEase(transition.ease).SetUpdate(true);
        }
    }
}