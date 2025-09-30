using Core.Defines;
using Cysharp.Threading.Tasks;
using UIs.Visuals;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public abstract class BaseUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        [SerializeField] private VisualElement _targetVisualElement;
        [field : SerializeField] public EnumDefines.UIType UIType { get; private set; }

        public virtual async UniTask Show()
        {
            if (_canvas != null)
                _canvas.enabled = true;
            if (_graphicRaycaster != null)
                _graphicRaycaster.enabled = true;

            if (_targetVisualElement != null)
            {
                await _targetVisualElement.AddState(ConstDefine.SHOW, 0);
                await _targetVisualElement.RemoveState(ConstDefine.HIDE);
            }
        }

        public virtual async UniTask Hide(bool turnOff = true)
        {
            if (_graphicRaycaster != null && turnOff)
                _graphicRaycaster.enabled = false;

            if (_targetVisualElement != null)
            {
                await _targetVisualElement.AddState(ConstDefine.HIDE, 100);
                _ = _targetVisualElement.RemoveState(ConstDefine.SHOW);
            }

            if (_canvas != null && turnOff)
                _canvas.enabled = false;
        }
        
        public void SetCanvasAndRaycasterEnabled(bool isEnabled)
        {
            if (_canvas != null)
                _canvas.enabled = isEnabled;
            if (_graphicRaycaster != null)
                _graphicRaycaster.enabled = isEnabled;
        }
    }
}