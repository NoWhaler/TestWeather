using Game.IntroductionWindow.Model;
using Game.SettingsPanel.Model;

namespace Game.SettingsPanel.Presenter
{
    public class SettingsPresenter
    {
        private SettingsModel _settingsModel;
        private IntroductionModel _introductionModel;
        
        public SettingsPresenter(SettingsModel settingsModel, IntroductionModel introductionModel)
        {
            _settingsModel = settingsModel;
            _introductionModel = introductionModel;
        }

        public void OnCloseButtonClick()
        {
            _settingsModel.SetOpenState(false);
        }

        public void OnIntroductionWindowButtonClick()
        {
            _introductionModel.SetOpenState(true);
        }

        public void OnSoundButtonClick(bool state)
        {
            _settingsModel.SetSoundState(state);
        }

        public void OnMusicButtonClick(bool state)
        {
            _settingsModel.SetMusicState(state);
        }
    }
}
