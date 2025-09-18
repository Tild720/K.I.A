using KWJ.OverlapChecker;
using UnityEngine;

namespace KWJ.Interactable
{
    public class Grill : MonoBehaviour
    {
        [SerializeField] private BoxOverlapChecker _boxChecker;
        
        private bool _isGrillOn;

        private void Update()
        {
            if (_boxChecker.BoxOverlapCheck())
            {
                
            }
        }

        public void OnGrill(bool isFirstClick) => _isGrillOn = isFirstClick;
    }
}