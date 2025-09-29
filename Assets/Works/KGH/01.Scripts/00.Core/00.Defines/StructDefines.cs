using System;
using DG.Tweening;

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
    }
}