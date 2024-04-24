using System;
using System.IO;
using System.Text;
using Core;
using Core.Bootstrapper;
using Core.Data;
using Core.Services;
using Cysharp.Threading.Tasks;
using Game.ProgressBar;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;
using Button = UnityEngine.UI.Button;

namespace Game.SceneLoading
{
    public class SceneLoader: MonoBehaviour
    {
        [SerializeField] private Button _loadButton;

        [SerializeField] private LoadingBar _loadingBar;

        [Inject] private Bootstrap _bootstrap;

        [Inject] private HttpService _httpService;

        private void Awake()
        {
            CreateWeatherCardsDataConfig();
            CreatePanelsDataConfig();

            _loadButton.onClick.AddListener(LoadSecondSceneCo);
        }
        
        private async UniTask LoadWeatherCardsDataToFile(string fileName)
        {
            var filePath = Path.Combine(Application.persistentDataPath, fileName);
   
            string loadedJsonString = await File.ReadAllTextAsync(filePath);

            try
            {
                if (loadedJsonString != "")
                {
                    _bootstrap.WeatherConfig = JsonConvert.DeserializeObject<WeatherConfig>(loadedJsonString);
                }
               
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to deserialize items from JSON: " + e.Message);
            }
        }

        private async UniTask LoadPanelsDataToFile(string fileName)
        {
            var filePath = Path.Combine(Application.persistentDataPath, fileName);
   
            string loadedJsonString = await File.ReadAllTextAsync(filePath);

            try
            {
                if (loadedJsonString != "")
                {
                    _bootstrap.PanelsStateConfig = JsonConvert.DeserializeObject<PanelsStateConfig>(loadedJsonString);
                }
               
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to deserialize items from JSON: " + e.Message);
            }
        }

        private async UniTask LoadFilesData()
        {
            await LoadPanelsDataToFile(Constants.PanelsStateFile);
            await LoadWeatherCardsDataToFile(Constants.WeatherCardsFile);

            var weatherResponses = await _httpService.GetWeather(Constants.WeatherLocations);
            await _bootstrap.WeatherConfig.UpdateWeatherCard(weatherResponses);
        }

        private async void CreateWeatherCardsDataConfig()
        {
            var cardsFilePath = Path.Combine(Application.persistentDataPath, Constants.WeatherCardsFile);

            if (!File.Exists(cardsFilePath))
            {
                await File.Create(cardsFilePath).DisposeAsync();
                
                _bootstrap.WeatherConfig = new WeatherConfig().InitializeDefaultCards();
                await _bootstrap.WeatherConfig.SaveDataToFile();
            }
        }

        private async void CreatePanelsDataConfig()
        {
            var panelsStatesFilePath = Path.Combine(Application.persistentDataPath, Constants.PanelsStateFile);
                        
            if (!File.Exists(panelsStatesFilePath))
            {
                await File.Create(panelsStatesFilePath).DisposeAsync();

                _bootstrap.PanelsStateConfig = new PanelsStateConfig().InitializeDefaultValues();
                await _bootstrap.PanelsStateConfig.SaveStates();
            }
        }

        private async void LoadSecondSceneCo()
        {
            UniTask loadingTask = LoadFilesData();
            
            await loadingTask;
            
            var operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            operation.allowSceneActivation = false;
            
            while (!operation.isDone)
            {
                _loadingBar.UpdateFraction(operation.progress);
                
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }

                await UniTask.DelayFrame(1);
            }
        }
    }
}