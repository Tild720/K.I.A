using Ami.BroAudio;
using UnityEngine;

namespace KWJ.FeedBacks
{
    public class SoundFeedBack : FeedBack
    {
        [SerializeField] private SoundID soundID;
        
        public override void CreateFeedback()
        {
            BroAudio.Play(soundID);
        }

        public override void StopFeedback()
        {
            
        }
    }
}