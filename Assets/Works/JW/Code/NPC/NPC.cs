using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Code.NPC
{
    public class NPC : MonoBehaviour
    {
        private NavMeshAgent _agent;
        [SerializeField] private NPCConversationCompo _conversation;
        [SerializeField] private LineType testLineType;
        [SerializeField] private float completeMoveWindow;
        [SerializeField] private float patience;
        [SerializeField, MinMaxRangeSlider(0, 1)] private float greed;

        private float _timer;

        public bool IsMoveCompleted => Vector3.Distance(transform.position, _agent.destination) < completeMoveWindow;

        public bool IsFront { get; set; } = false;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _timer = 0;
        }

        public void MoveToPoint(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        public void Speech(LineType type)
        {
            _conversation.Speech(type);
        }

        public bool GetFood()
        {
            if (Random.value <= greed)
            {
                Speech(LineType.RequestFood);
                return false;
            }

            return true;
        }

        private void Update()
        {
            if (IsFront)
            {
                _timer += Time.deltaTime;
                if (_timer >= patience)
                {
                    Speech(LineType.RequestFood);
                    _timer = Random.Range(0, patience / 2);
                }
            }
        }
    }
}