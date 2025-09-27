using UnityEngine;

namespace KWJ.Etc
{
    public class MoveAboveTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float height;
        private void Update()
        {
            if (target == null)
            {
                Debug.LogWarning("Target이 없습니다.");
                return;
            }
            
            transform.position = target.position + Vector3.up * height;
        }
    }
}