using KWJ.Interactable.PickUpable;
using KWJ.OverlapChecker;
using UnityEngine;

namespace KWJ.Interactable
{
    public class Sink : MonoBehaviour
    {
        [SerializeField] private RaycastChecker raycastChecker;
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private GameObject button;
        
        private bool _isOn;

        public void OnToggle()
        {
            _isOn = !_isOn;

            if (_isOn)
            {
                particleSystem.Play();
                button.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                particleSystem.Stop();
                button.transform.rotation = Quaternion.Euler(0, 220, 0);
            }
        }
        
        private void Update()
        {
            if(!_isOn || !raycastChecker.RaycastCheck()) return;
            
            GameObject[] gameObjects = raycastChecker.GetRaycastData();

            foreach (var potObject in gameObjects)
            {
                if (potObject.TryGetComponent<Pot>(out var pot))
                {
                    pot.FillWater(Time.deltaTime * 0.5f);
                }
            }
        }
    }
}