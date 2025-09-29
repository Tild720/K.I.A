using System;
using UnityEngine;

namespace Region
{
    [CreateAssetMenu(fileName = "Region", menuName = "SO/Region", order = 1)]
    public class RegionSO : ScriptableObject, ICloneable
    {
        public string regionName;
        public int population;
        public int health; //0~100
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}