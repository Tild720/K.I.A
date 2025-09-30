using System;
using DG.Tweening;
using Foods;

namespace Core.Defines
{
    public class StructDefines
    {
        [Serializable]
        public struct TransitionData
        {
            public float duration;
            public Ease ease;
        }

        [Serializable]
        public struct IngredientData
        {
            public IngredientSO ingredient;
            public int count;
        }
    }
}