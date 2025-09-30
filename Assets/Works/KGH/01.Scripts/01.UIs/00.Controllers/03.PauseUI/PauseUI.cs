using Ami.BroAudio;
using Ami.BroAudio.Data;
using Code.Core.EventSystems;
using Core.Defines;
using Core.EventSystem;
using Settings.InputSystem;
using UnityEngine;

namespace Controllers.PauseUI
{
    public class PauseUI : BaseUI
    {
        [SerializeField] private UIStateMachine stateMachine;
        [SerializeField] private PlayerInputSO playerInputSO;
        private bool _isPaused = false;
        
        private void Awake()
        {
            playerInputSO.OnEsc += TogglePause;
        }
        
        private void OnDestroy()
        {
            playerInputSO.OnEsc -= TogglePause;
        }

        private void TogglePause()
        {
            if (stateMachine.CurrentUI != null && stateMachine.CurrentUI != this) return;
            
            _isPaused = !_isPaused;
            
            if (_isPaused)
            {
                GameEventBus.RaiseEvent(UIEvents.UIChangeEvent.Initialize(EnumDefines.UIType.PAUSE, false));
            }
            else
            {
                GameEventBus.RaiseEvent(UIEvents.UIChangeEvent.Initialize(EnumDefines.UIType.NONE, false));
            }
        }
        
        public void ResumeGame()
        {
            if (_isPaused)
            {
                TogglePause();
            }
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
        
        public void SetMasterVolume(float volume)
        {
            BroAudio.SetVolume(BroAudioType.All, volume);
        }
        public void SetMusicVolume(float volume)
        {
            BroAudio.SetVolume(BroAudioType.Music, volume);
        }
        public void SetSFXVolume(float volume)
        {
            BroAudio.SetVolume(BroAudioType.SFX, volume);
            BroAudio.SetVolume(BroAudioType.UI, volume);
        }
    }
}