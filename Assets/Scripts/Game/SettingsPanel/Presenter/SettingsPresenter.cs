using Core.Bootstrapper;
using Game.IntroductionWindow.Model;
using Game.SettingsPanel.Model;

namespace Game.SettingsPanel.Presenter
{
    public class SettingsPresenter
    {
        private SettingsModel _settingsModel;
        private IntroductionModel _introductionModel;
        private Bootstrap _bootstrap;
        
        public SettingsPresenter(SettingsModel settingsModel, IntroductionModel introductionModel, Bootstrap bootstrap)
        {
            _settingsModel = settingsModel;
            _introductionModel = introductionModel;
            _bootstrap = bootstrap;
        }

        public void OnCloseButtonClick()
        {
            _settingsModel.SetOpenState(false);
            _bootstrap.PanelsStateConfig.SetSettingsState(false);
        }

        public void OnIntroductionWindowButtonClick()
        {
            _introductionModel.SetOpenState(true);
            _bootstrap.PanelsStateConfig.SetIntroductionWindowState(true);
        }

        public void OnSoundButtonClick(bool state)
        {
            _settingsModel.SetSoundState(state);
        }

        public void OnMusicButtonClick(bool state)
        {
            _settingsModel.SetMusicState(state);
        }

        public void OnMusicValueChange(float value)
        {
            _bootstrap.PanelsStateConfig.SetSoundValue(value);
        }

        public void OnSoundValueChange(float value)
        {
            _bootstrap.PanelsStateConfig.SetMusicValue(value);
        }

        public void SetValuesFromJson()
        {
            _settingsModel.SetOpenState(_bootstrap.PanelsStateConfig.GetSettingsPanelState());
            _introductionModel.SetOpenState(_bootstrap.PanelsStateConfig.GetIntroductionPanelState());
        }
    }
}
