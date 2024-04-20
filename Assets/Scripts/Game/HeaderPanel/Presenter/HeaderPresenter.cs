using System.IO;
using System.Threading.Tasks;
using Core.Bootstrapper;
using Core.Data;
using Game.HeaderPanel.Model;
using Game.SettingsPanel.Model;
using UnityEngine;

namespace Game.HeaderPanel.Presenter
{
    public class HeaderPresenter
    {
        private HeaderModel _headerModel;

        private SettingsModel _settingsModel;

        private Bootstrap _bootstrap;

        public HeaderPresenter(HeaderModel headerModel, SettingsModel settingsModel, Bootstrap bootstrap)
        {
            _headerModel = headerModel;
            _settingsModel = settingsModel;
            _bootstrap = bootstrap;
        }

        public void OnExitGameButtonClick()
        {
            CloseApplication();
        }

        public void OnOpenSettingsButtonClick()
        {
            _settingsModel.SetOpenState(true);
            _bootstrap.PanelsStateConfig.SetSettingsState(true);
        }

        private async Task CloseApplication()
        {
            await _bootstrap.WeatherConfig.SaveDataToFile(Path.Combine(Application.streamingAssetsPath, Constants.WeatherCardsFile));
            await _bootstrap.PanelsStateConfig.SaveStates(Path.Combine(Application.streamingAssetsPath, Constants.PanelsStateFiles));
            Application.Quit();
        }
    }
}
