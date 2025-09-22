using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIs.Visuals
{
    public class VisualElement : MonoBehaviour
    {
        private Dictionary<string, int> states;
        private string currentState;
        public Action<string> OnStateChanged;
        
        public void AddState(string stateName, int priority)
        {
            if (states == null)
                states = new Dictionary<string, int>();

            states[stateName] = priority;
        }
        
        public void RemoveState(string stateName)
        {
            if (states == null)
                return;

            states.Remove(stateName);
        }
    }
}