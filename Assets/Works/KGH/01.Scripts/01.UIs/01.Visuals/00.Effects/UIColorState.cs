using Core.Defines;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Visuals.Effects
{
    public class UIColorState : MonoBehaviour, IUIState
    {
        [SerializeField] private string stateName = "default";
        public string StateName => stateName;
        
        [SerializeField] private Color targetColor = Color.white;
        [SerializeField] private StructDefines.TransitionData transition = new StructDefines.TransitionData
        {
            duration = 0.2f,
            ease = Ease.OutSine
        };
        
        private Graphic _target;
        public void Initialize(VisualElement owner)
        {
            _target = owner.GetComponent<Graphic>();
            if (_target == null)
                Debug.LogWarning($"[UIColorState] No Graphic component found on {owner.name}");
        }

        public void PlayEffect()
        {
            _target?.DOColor(targetColor, transition.duration).SetEase(transition.ease).SetUpdate(true);
        }
    }
}