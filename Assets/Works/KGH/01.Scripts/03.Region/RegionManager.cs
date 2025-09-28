using System.Collections.Generic;
using UnityEngine;

namespace Region
{
    public class RegionManager : MonoBehaviour
    {
        [SerializeField] private List<RegionSO> targetRegions;
        private List<RegionSO> _regions = new List<RegionSO>();
        public static RegionManager Instance { get; private set; }

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
                
                //test code
                clone.health += 100;
                Debug.Log($"Clone {clone.regionName} Health: {clone.health} (original: {item.health})");
            }

            DontDestroyOnLoad(this.gameObject);
        }

        private int _currentRegionIndex = 0;
        public RegionSO CurrentRegion => _regions[_currentRegionIndex];

        public void NextRegion() => _currentRegionIndex = (_currentRegionIndex + 1) % _regions.Count;
    }
}