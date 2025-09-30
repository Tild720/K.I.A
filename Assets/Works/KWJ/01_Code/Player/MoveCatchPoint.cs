using KWJ.Entities;
using UnityEngine;

namespace KWJ.Players
{
    public class MoveCatchPoint : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Transform _catchPoint;
        
        [SerializeField] private float maxScroll;
        [SerializeField] private float minScroll;
        
        private Camera _camera;
        private Player _agent;

        private float _scrollZ;
        public void Initialize(Entity entity)
        {
            _agent = entity as Player;
            _camera = Camera.main;
            _scrollZ = minScroll;
        }
        private void Update()
        {
            Vector3 localOffset = new Vector3(0, 0, _scrollZ);
            _catchPoint.position = _camera.transform.position + _camera.transform.rotation * localOffset;
            _catchPoint.rotation = _camera.transform.rotation;
        }

        private void OnEnable()
        {
            _agent.PlayerInputSo.ScrollAction += OnScrollAction;
        }

        private void OnScrollAction(float scrollZ)
        {
            _scrollZ += scrollZ * 0.1f;
            _scrollZ = Mathf.Clamp(_scrollZ, minScroll, maxScroll);
        }

        private void OnDisable()
        {
            _agent.PlayerInputSo.ScrollAction -= OnScrollAction;
        }
    }
}