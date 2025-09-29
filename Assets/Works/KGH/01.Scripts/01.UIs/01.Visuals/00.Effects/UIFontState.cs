using Core.Defines;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UIs.Visuals.Effects
{
    public class UIFontState : MonoBehaviour, IUIState
    {
        [SerializeField] private string stateName = "default";
        public string StateName => stateName;
        [SerializeField] private StructDefines.TransitionData transition = new StructDefines.TransitionData { duration = 0.2f, ease = Ease.OutSine };
        [SerializeField] private float fontSize = 36f;
        
        private TextMeshProUGUI _target;
        public void Initialize(VisualElement owner)
        {
            _target = owner.GetComponent<TextMeshProUGUI>();
            if (_target == null)
                Debug.LogWarning($"[UIFontState] No TextMeshProUGUI component found on {owner.name}");
        }

        public void PlayEffect()
        {
            var previousFontSize = _target.fontSize;
            DOVirtual.Float(previousFontSize, fontSize, transition.duration, value => _target.fontSize = value).SetEase(transition.ease).SetUpdate(true);
        }
    }
}