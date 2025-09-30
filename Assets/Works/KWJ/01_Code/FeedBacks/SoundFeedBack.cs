using Ami.BroAudio;
using UnityEngine;

namespace KWJ.FeedBacks
{
    public class SoundFeedBack : FeedBack
    {
        [SerializeField] private SoundID soundID;

        [SerializeField] private bool isOncePlay;
        private bool _isPlay;
        
        public override void CreateFeedback()
        {
            if(_isPlay && !isOncePlay) return;
            
            _isPlay = true;
            BroAudio.Play(soundID);
        }

        public override void StopFeedback()
        {
            if(_isPlay == false && !isOncePlay) return;
            
            _isPlay = false;
            BroAudio.Stop(soundID);
        }
    }
}