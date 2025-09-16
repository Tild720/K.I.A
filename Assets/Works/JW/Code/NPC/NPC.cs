using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Code.NPC
{
    
    
    public class NPC : MonoBehaviour
    {
        private NavMeshAgent _agent;
        [SerializeField] private NPCConversationCompo  _conversation;

        [SerializeField] private string testLineType;
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void MoveToPoint(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        private void Update()
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
                _conversation.Speech(testLineType);
        }
    }
}