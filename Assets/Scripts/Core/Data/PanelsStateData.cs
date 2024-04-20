using System;

namespace Core.Data
{
    [Serializable]
    public class PanelsStateData
    {
        public bool IntroductionWindowOpenState { get; set; }
        
        public bool SettingsOpenState { get; set; }
        
        public float SoundValue { get; set; }
        
        public float MusicValue { get; set; }
    }
}