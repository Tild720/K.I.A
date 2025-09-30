using Core.Defines;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Visuals.Effects
{
    public class UILayoutElementState : MonoBehaviour, IUIState
    {
        [SerializeField] private string stateName = "default";
        public string StateName => stateName;
        [SerializeField] private StructDefines.TransitionData transition = new() { duration = 0.2f, ease = Ease.OutSine };
        [SerializeField] private float flexibleWidth = 1f;
        [SerializeField] private float flexibleHeight = 1f;
        
        private LayoutElement _target;
        
        public void Initialize(VisualElement owner)
        {
            _target = owner.GetComponent<LayoutElement>();
            if (_target == null)
                Debug.LogWarning($"[UICanvasGroupState] No CanvasGroup component found on {owner.name}");
        }

        public UniTask PlayEffect()
        {
            var previousPreferredWidth = _target.flexibleWidth;
            var previousPreferredHeight = _target.flexibleHeight;
            
            DOVirtual.Float(previousPreferredWidth, flexibleWidth, transition.duration, value => _target.flexibleWidth = value).SetEase(transition.ease).SetUpdate(true);
            DOVirtual.Float(previousPreferredHeight, flexibleHeight, transition.duration, value => _target.flexibleHeight = value).SetEase(transition.ease).SetUpdate(true);
            
            return UniTask.WaitForSeconds(transition.duration, true);
        }
    }
}