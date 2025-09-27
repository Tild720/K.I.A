using UnityEngine;
using UnityEngine.Events;

namespace KWJ.OverlapChecker
{
    public class RaycastChecker : MonoBehaviour
    {
        public UnityEvent OnTargetEnterEvent;
        
        [SerializeField] private Transform checkPoint;
        [SerializeField] private LayerMask targetMask;
        [Space]
        [SerializeField] private float length;
        [SerializeField] private int maxCount;
        
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
            int count = Physics.RaycastNonAlloc(checkPoint.position, transform.forward, _results, length, targetMask);
            
            GameObject[] targets = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                targets[i] = _results[i].collider.gameObject;
            }

            return targets;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(checkPoint == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(checkPoint.position, transform.forward * length);
        }
#endif
    }
}