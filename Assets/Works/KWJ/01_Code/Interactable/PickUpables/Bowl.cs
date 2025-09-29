using System;
using KWJ.Food;
using KWJ.OverlapChecker;
using UnityEngine;

namespace KWJ.Interactable.PickUpable
{
    public class Bowl : MonoBehaviour
    {
        [SerializeField] private BoxOverlapChecker boxChecker;
        [SerializeField] private LiquidFood liquidFood;
        [SerializeField] private GameObject water;

        private bool _isFill;

        private void Update()
        {
            if(_isFill || !boxChecker.BoxOverlapCheck()) return;

            GameObject[] gameObjects = boxChecker.GetOverlapData();

            foreach (var potObject in gameObjects)
            {
                if (potObject.TryGetComponent<Pot>(out var pot))
                {
                    if(pot.Rigidbody.useGravity == false) return;

                    if (pot.CurrentAmountWater > 0 && pot.LiquidFood.IsCompleteEvaluation)
                    {
                        liquidFood.SetFood(pot.LiquidFood.FoodType, pot.LiquidFood.FoodState);
                        _isFill = true;
                        pot.SubtractionAmountWater(0.1f);
                        water.SetActive(true);
                    }
                    
                    break;
                }
            }
        }
    }
}