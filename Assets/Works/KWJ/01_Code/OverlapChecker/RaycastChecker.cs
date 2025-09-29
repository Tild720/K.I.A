using System;
using UnityEngine;
using UnityEngine.Events;

namespace KWJ.OverlapChecker
{
    public class RaycastChecker : MonoBehaviour
    {
        public UnityEvent OnTargetEnterEvent;
        
        [SerializeField] private Transform checkPoint;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private Vector3 boxSize;
        [Space]
        [SerializeField] private float length;
        [SerializeField] private int maxCount;
        
        [SerializeField] private bool isBoxCast;

        private RaycastHit[] _results;
        
        private void Awake()
        {
            _results = new RaycastHit[maxCount];
        }

        public bool RaycastCheck()
        {
            bool isOverlap = Physics.Raycast(checkPoint.position, transform.forward , length, targetMask);
            
            if (isOverlap)
            {
                OnTargetEnterEvent.Invoke();
            }
            
            return isOverlap;
        }
        
        public GameObject[] GetRaycastData()
        {
            int count = 0;
            
            if(isBoxCast)
                count =Physics.BoxCastNonAlloc(checkPoint.position, boxSize,
                transform.forward, _results, Quaternion.identity, length, targetMask);
            else
                count = Physics.RaycastNonAlloc(checkPoint.position, transform.forward, _results, length, targetMask);
            
            GameObject[] targets = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                targets[i] = _results[i].collider.gameObject;
            }

            Array.Sort(targets, (a, b) =>
                Vector3.Distance(checkPoint.position, a.transform.position)
                    .CompareTo(Vector3.Distance(transform.position, b.transform.position)));
            
            return targets;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(checkPoint == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(checkPoint.position, transform.forward * length);
            
            if(!isBoxCast) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(checkPoint.position + transform.forward * length, boxSize);
        }
#endif
    }
}