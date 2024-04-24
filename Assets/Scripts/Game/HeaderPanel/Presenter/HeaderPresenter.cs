using System.IO;
using System.Threading.Tasks;
using Core;
using Core.Bootstrapper;
using Core.Data;
using Cysharp.Threading.Tasks;
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

        public async void OnExitGameButtonClick()
        {
            await CloseApplication();
        }

        public async void OnOpenSettingsButtonClick()
        {
            _settingsModel.SetOpenState(true);
            _bootstrap.PanelsStateConfig.SetSettingsState(true);

            await _bootstrap.PanelsStateConfig.SaveStates();
        }

        private async UniTask CloseApplication()
        {
            // await _bootstrap.WeatherConfig.SaveDataToFile();
            // await _bootstrap.PanelsStateConfig.SaveStates();
            Application.Quit();
        }
    }
}
