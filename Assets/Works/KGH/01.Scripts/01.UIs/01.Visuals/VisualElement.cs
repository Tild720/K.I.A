using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UIs.Visuals.Effects;
using UnityEngine;

namespace UIs.Visuals
{
    public class VisualElement : MonoBehaviour
    {
        [SerializeField] private string defaultState = "default";
        [SerializeField] private bool isThisRoot;
        [SerializeField] private Transform stateRoot;
        private Dictionary<string, int> _states = new Dictionary<string, int>();
        private Dictionary<string, List<IUIState>> _effects = new Dictionary<string, List<IUIState>>();
        private List<VisualElement> _children = new List<VisualElement>();
        private string _currentState;
        public string CurrentState => _currentState;
        public Action<string> OnStateChanged;

        private void Awake()
        {
            if (stateRoot == null && !isThisRoot)
                stateRoot = transform;

            if (stateRoot != null && !isThisRoot)
            {
                foreach (var effect in stateRoot.GetComponentsInChildren<IUIState>())
                {
                    effect.Initialize(this);
                    if (_effects.ContainsKey(effect.StateName))
                    {
                        _effects[effect.StateName].Add(effect);
                    }
                    else
                    {
                        _effects[effect.StateName] = new List<IUIState> { effect };
                    }
                }
            }

            foreach (Transform child in transform)
            {
                var visualElement = child.GetComponent<VisualElement>();
                if (visualElement != null)
                {
                    _children.Add(visualElement);
                }
            }

            if (_states == null)
                _states = new Dictionary<string, int>();

            _states[defaultState] = 0;
            _currentState = defaultState;
        }

        public async UniTask AddState(string stateName, int priority)
        {
            if (_effects.ContainsKey(stateName) || isThisRoot)
                _states[stateName] = priority;

            // _children.ForEach(c => c.AddState(stateName, priority));
            var wh = UniTask.WhenAll(_children.ConvertAll(c => c.AddState(stateName, priority)));

            await UniTask.WhenAll(UpdateState(), wh);
        }

        public async UniTask RemoveState(string stateName)
        {
            if (_states == null)
                return;
            if (_states.ContainsKey(stateName))
                _states.Remove(stateName);
            // _children.ForEach(c => c.RemoveState(stateName));
            UniTask wh = UniTask.CompletedTask;
            if (_children.Count > 0)
                wh = UniTask.WhenAll(_children.ConvertAll(c => c.RemoveState(stateName)));

            await UniTask.WhenAll(UpdateState(), wh);
            
        }
        
        private async UniTask UpdateState()
        {
            var nextState = GetHighestPriorityState();
            if (nextState != _currentState)
            {
                _currentState = nextState;
                OnStateChanged?.Invoke(_currentState);
                
                if (_effects.TryGetValue(_currentState, out var nextEffect))
                {
                    await UniTask.WhenAll(nextEffect.ConvertAll(e => e.PlayEffect()));
                }
            }
        }

        private string GetHighestPriorityState()
        {
            var stateName = defaultState;
            int highestPriority = int.MinValue;

            foreach (var state in _states)
            {
                if (state.Value > highestPriority)
                {
                    highestPriority = state.Value;
                    stateName = state.Key;
                }
            }

            return stateName;
        }
    }
}