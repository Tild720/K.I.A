using KWJ.Interactable.PickUpable;
using KWJ.OverlapChecker;
using UnityEngine;

namespace KWJ.Interactable
{
    public class GasStove : MonoBehaviour
    {
        [SerializeField] private BoxOverlapChecker boxChecker;
        [SerializeField] private PickUpableFixture fixture;

        private Pot _pot;
        
        private bool _hasPot;
        private bool _isOn;
        public void ToggleOn()
        {
            _isOn = !_isOn;
        }

        private void Update()
        {
            if (_hasPot)
            {
                if (_pot.IsPutdown && _isOn)
                {
                    _pot.CreateFood();
                }
            }
            
            if(_hasPot || !boxChecker.BoxOverlapCheck()) return;

            GameObject[] gameObjects = boxChecker.GetOverlapData();

            foreach (var potObject in gameObjects)
            {
                if (potObject.TryGetComponent<Pot>(out var pot))
                {
                    if(pot.Rigidbody.useGravity == false) return;
                        
                    _hasPot = true;
                    _pot = pot;

                    if (pot.IsInitComplete == false)
                        pot.Initialized(this);
                    
                    fixture.FixedPickUpalbe(pot);
                    pot.SetCanPickUp(false);
                    
                    break;
                }
            }
        }
        
        public void SetHasPot(bool hasPot)
        {
            _hasPot = hasPot;
            
            if(!hasPot)
                _pot = null;
        }
    }
}