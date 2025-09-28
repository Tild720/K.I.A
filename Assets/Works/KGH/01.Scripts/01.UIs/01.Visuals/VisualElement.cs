using System;
using System.Collections.Generic;
using UIs.Visuals.Effects;
using UnityEngine;

namespace UIs.Visuals
{
    public class VisualElement : MonoBehaviour
    {
        [SerializeField] private string defaultState = "default";
        [SerializeField] private Transform stateRoot;
        private Dictionary<string, int> _states;
        private Dictionary<string, IUIState> _effects = new Dictionary<string, IUIState>();
        private List<VisualElement> _children = new List<VisualElement>();
        private string _currentState;

        private void Awake()
        {
            if (stateRoot == null)
                stateRoot = transform;
            
            foreach (var effect in stateRoot.GetComponentsInChildren<IUIState>())
            {
                effect.Initialize(this);
                _effects[effect.StateName] = effect;
            }

            foreach (Transform child in transform)
            {
                var visualElement = child.GetComponent<VisualElement>();
                if (visualElement != null)
                    _children.Add(visualElement);
            }
            
            if (_states == null)
                _states = new Dictionary<string, int>();
            
            _states[defaultState] = 0;
            _currentState = defaultState;
        }

        public void AddState(string stateName, int priority)
        {
            _children.ForEach(c => c.AddState(stateName, priority));
            _states[stateName] = priority;
            var nextState = GetHighestPriorityState();
            if (nextState != _currentState)
            {
                var beforeState = _currentState;
                _currentState = nextState;
                if (_effects.TryGetValue(_currentState, out var nextEffect))
                {
                    var beforeEffect = default(IUIState);
                    if (beforeState != null)
                        _effects.TryGetValue(beforeState, out beforeEffect);
                    nextEffect.PlayEffect(beforeEffect);
                }
            }
        }
        
        public void RemoveState(string stateName)
        {
            if (_states == null)
                return;
            _states.Remove(stateName);
            _children.ForEach(c => c.RemoveState(stateName));
            var nextState = GetHighestPriorityState();
            if (nextState != _currentState)
            {
                var beforeState = _currentState;
                _currentState = nextState;
                if (_effects.TryGetValue(_currentState, out var nextEffect))
                {
                    var beforeEffect = default(IUIState);
                    if (beforeState != null)
                        _effects.TryGetValue(beforeState, out beforeEffect);
                    nextEffect.PlayEffect(beforeEffect);
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