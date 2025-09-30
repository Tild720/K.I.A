using UnityEngine;
using Unity.Cinemachine;
using Settings.InputSystem;
using KWJ.Entities;
using KWJ.SO;

namespace KWJ.Players
{
    public class Player : Entity
    {
        [field:Header("PlayerSO")]
        [field: SerializeField] public PlayerInputSO PlayerInputSo { get; private set; }
        [field: SerializeField] public PlayerStatsSO PlayerStatsSo { get; private set; }
        
        [field:Header("Camera")]
        [field: SerializeField] public CinemachineCamera CinemaCamera { get; private set; }
        [field: SerializeField] public Transform HeadPoint { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }
}