using System.IO;
using Core;
using Core.Bootstrapper;
using Core.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Game.FooterPanel.View
{
    public class FooterView : MonoBehaviour
    {
        [SerializeField] private Button _resetButton;

        [Inject] private Bootstrap _bootstrap;

        private void OnEnable()
        {
            _resetButton.onClick.AddListener(ReloadScene);
        }

        private async void ReloadScene()
        {
            await _bootstrap.WeatherConfig.ClearData(Path.Combine(Application.persistentDataPath, Constants.WeatherCardsFile));

            await _bootstrap.PanelsStateConfig.ClearData(Path.Combine(Application.persistentDataPath,
                Constants.PanelsStateFile));

            foreach (var weatherCards in _bootstrap.WeatherConfig.WeatherCards)
            {
                weatherCards.IsVisible = true;
            }

            _bootstrap.PanelsStateConfig = new PanelsStateConfig().InitializeDefaultValues();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}