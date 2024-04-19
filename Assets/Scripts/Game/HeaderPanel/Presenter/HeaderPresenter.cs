using Game.HeaderPanel.Model;
using Game.SettingsPanel.Model;
using UnityEngine;

namespace Game.HeaderPanel.Presenter
{
    public class HeaderPresenter
    {
        private HeaderModel _headerModel;

        private SettingsModel _settingsModel;

        public HeaderPresenter(HeaderModel headerModel, SettingsModel settingsModel)
        {
            _headerModel = headerModel;
            _settingsModel = settingsModel;
        }

        public void OnExitGameButtonClick()
        {
            CloseApplication();
        }

        public void OnOpenSettingsButtonClick()
        {
            _settingsModel.SetOpenState(true);
        }

        private void CloseApplication()
        {
            Application.Quit();
        }
    }
}
