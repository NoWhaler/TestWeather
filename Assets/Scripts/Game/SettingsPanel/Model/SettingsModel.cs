using Game.SettingsPanel.View;

namespace Game.SettingsPanel.Model
{
    public class SettingsModel
    {
        private SettingsView _settingsView;

        public bool CurrentOpenState { get; set; }
        
        public bool IsSoundToggled { get; set; }
        
        public bool IsMusicToggled { get; set; }
        
        public SettingsModel(SettingsView settingsView)
        {
            _settingsView = settingsView;
        }

        public void SetOpenState(bool state)
        {
            CurrentOpenState = state;
            _settingsView.DisplayOrCloseWindow(CurrentOpenState);
        }

        public void SetSoundState(bool state)
        {
            IsSoundToggled = state;
            _settingsView.SwitchOnOffSound();
        }

        public void SetMusicState(bool state)
        {
            IsMusicToggled = state;
            _settingsView.SwitchOnOffMusic();
        }
    }
}
