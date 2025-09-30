using System;
using System.Collections.Generic;
using Code.Core.EventSystems;
using Core.Defines;
using Core.EventSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public class UIStateMachine : MonoBehaviour
    {
        [SerializeField] private EnumDefines.UIType defaultUI = EnumDefines.UIType.TITLE;
        private Dictionary<EnumDefines.UIType, BaseUI> _uiDic = new Dictionary<EnumDefines.UIType, BaseUI>();
        private BaseUI _currentUI;
        public BaseUI CurrentUI => _currentUI;

        private void Awake()
        {
            var uiElements = GetComponentsInChildren<BaseUI>(true);
            foreach (var ui in uiElements)
            {
                if (_uiDic.ContainsKey(ui.UIType))
                {
                    Debug.LogWarning($"[UIStateMachine] Duplicate UIType detected: {ui.UIType} in {ui.name}");
                    continue;
                }

                _uiDic.Add(ui.UIType, ui);
                if (ui.UIType == defaultUI)
                {
                    ui.Show().Forget();
                    _currentUI = ui;
                }
                else
                {
                    ui.Hide().Forget();
                }
            }

            GameEventBus.AddListener<UIChangeEvent>(OnUIChange);
        }
        
        private void OnDestroy()
        {
            GameEventBus.RemoveListener<UIChangeEvent>(OnUIChange);
        }

        private void OnUIChange(UIChangeEvent e)
        {
            _ = OnUIChangeTask(e);
        }

        private async UniTaskVoid OnUIChangeTask(UIChangeEvent evt)
        {
            if (_currentUI != null)
                await _currentUI.Hide(!evt.DoesFade);

            if (evt.DoesFade)
            {
                await _uiDic[EnumDefines.UIType.FADE].Show();
                _currentUI.SetCanvasAndRaycasterEnabled(false);
                await _uiDic[EnumDefines.UIType.FADE].Hide();
            }

            if (evt.UIType == EnumDefines.UIType.NONE)
            {
                _currentUI = null;
            }
            else if (_uiDic.ContainsKey(evt.UIType))
            {
                _currentUI = _uiDic[evt.UIType];
                await _currentUI.Show();
            }
            else
            {
                Debug.LogWarning($"[UIStateMachine] UIType {evt.UIType} not found in dictionary.");
            }
        }
    }
}