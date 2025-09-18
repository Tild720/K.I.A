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
            if(!_isGrillOn) return;
            
            if (_boxChecker.BoxOverlapCheck())
            {
                GameObject[] foodIngredients = _boxChecker.GetOverlapData();
                
                
            }
        }

        public void OnGrill(bool isFirstClick) => _isGrillOn = isFirstClick;
    }
}