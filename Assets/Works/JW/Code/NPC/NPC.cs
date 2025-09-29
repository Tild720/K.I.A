using System;
using DG.Tweening;
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
        [SerializeField] private float completeMoveWindow;
        [SerializeField] private float patience;
        [SerializeField] private float greed;
        [SerializeField] private float deadAnimationTime;

        private float _timer;
        private bool _isDead;

        public bool IsMoveCompleted => Vector3.Distance(transform.position, _agent.destination) < completeMoveWindow;

        public bool IsFront { get; set; } = false;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _timer = 0;
            _isDead = false;
        }

        public void MoveToPoint(Vector3 point)
        {
            _agent.SetDestination(point);
        }
        
        public void RotateToPoint(Vector3 point)
        { 
            Vector3 dir = point - transform.position;
            dir.y = 0;
            //dir.Normalize();
            transform.rotation = Quaternion.LookRotation(dir);
        }

        public string Speech(LineType type)
        {
            return _conversation.Speech(type);
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

        public void NPCDead(Action endCallback = null)
        {
            _isDead = true;
            transform.DOLocalRotate(new Vector3(0, 0, 90), deadAnimationTime).OnComplete(() =>
            {
                Destroy(gameObject);
                endCallback?.Invoke();
            });
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