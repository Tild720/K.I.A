using UnityEngine;

namespace KWJ.SO
{
    [CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "SO/Player/PlayerStatsSO", order = 0)]
    public class PlayerStatsSO : ScriptableObject
    {
        public int MaxHealth;
        
        [Space]
        
        public float WalkSpeed;
        public float RunSpeed;
        
        [Space]
        
        public int JumpPower;
        public int ThrowPower;
        
        [Space]
        
        public int InteractionRange;
        
        [Space]
        
        public int MaxStamina;
        public int StaminaDaley;
    }
}