using System;
using System.Collections.Generic;
using Code.Core.EventSystems;
using KWJ.Define;
using Unity.Mathematics;
using UnityEngine;
using Works.JW.Events;
using Works.Tild.Code;
using Works.Tild.Code.Events;

namespace Region
{
    public class RegionManager : MonoBehaviour
    {
        [SerializeField] private List<RegionSO> targetRegions;
        private List<RegionSO> _regions = new List<RegionSO>();
        public static RegionManager Instance { get; private set; }
        private readonly ResultEvent ResultEvent = ScoreEventChannel.ResultEvent;

        public int Money;
        public int Used;
        public int HealthOrigin;
        public int HealthFixed;
        public int Died;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;

            foreach (var item in targetRegions)
            {
                var clone = item.Clone() as RegionSO;
                _regions.Add(clone);
            }

            DontDestroyOnLoad(this.gameObject);
            
            GameEventBus.AddListener<FoodEatEvent>(AddScore);
            GameEventBus.AddListener<NPCLineEndEvent>(ViewResult);
            GameEventBus.AddListener<NPCDeadEvent>(DeadAdding);
        }

        private void DeadAdding(NPCDeadEvent obj)
        {
            Died += 1;
        }

        private void ViewResult(NPCLineEndEvent obj)
        {
            GameEventBus.RaiseEvent(ResultEvent.Initializer(Money, Used, HealthOrigin, HealthFixed,Died));
            
            
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<FoodEatEvent>(AddScore);
        }


        public void AddScore(FoodEatEvent evt)
        {
            float multiplier = 1f;
            float baseScore = 0f;
            
            switch (evt.foodState)
            {
                case FoodState.Good:
                    multiplier = 1f;
                    break;
                case FoodState.Normal:
                    multiplier = 0.5f;
                    break;
                case FoodState.Bad:
                    multiplier = -1f;  
                    break;
            }


            switch (evt.foodType)
            {
                case FoodType.Soup:
                    baseScore = 3f;
                    break;
                case FoodType.Porridge:
                    baseScore = 1f;
                    break;
                case FoodType.LowQualityMeat:
                    baseScore = 5f;
                    break;
                case FoodType.Toast:
                    baseScore = 3f;
                    break;
                case FoodType.Sandwich:
                    baseScore = 10f;
                    break;
                case FoodType.Beef:
                    baseScore = 15f;
                    break;
            }

            float finalScore = Mathf.Min(Mathf.Max(baseScore * multiplier, -5),15);
                
            HealthFixed += (int)finalScore;
        }
 

        private int _currentRegionIndex = 0;
        public RegionSO CurrentRegion => _regions[_currentRegionIndex];

        public void NextRegion() => _currentRegionIndex = (_currentRegionIndex + 1) % _regions.Count;
    }
}