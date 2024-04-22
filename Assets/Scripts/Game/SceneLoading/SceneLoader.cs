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
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
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

        private async void Awake()
        {
            CreateWeatherCardsDataConfig();
            CreatePanelsDataConfig();

            _loadButton.onClick.AddListener(LoadSecondSceneCo);
        }

        private async UniTask LoadDataToFile(string fileName)
        {
            var filePath = Path.Combine(Application.persistentDataPath, fileName);
            byte[] data = Encoding.UTF8.GetBytes("");
            
#if UNITY_ANDROID && !UNITY_EDITOR

                using (UnityWebRequest www = UnityWebRequest.Get(filePath))
                {
                    await www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.ConnectionError || 
                        www.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.LogError("Error: " + www.result);
                    }
                    else
                    {
                        if (www.downloadHandler != null)
                        {
                            data = www.downloadHandler.data;
                        }
                        else
                        {
                            Debug.LogWarning("Download handler is null");
                        }
                    }
                }
#else
                data = await File.ReadAllBytesAsync(filePath);
#endif
           
            string loadedJsonString = "";
                        
            if (data.Length > 0)
            {
                loadedJsonString = Encoding.UTF8.GetString(data);
            }
            
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
            await LoadDataToFile(Constants.PanelsStateFile);
            await LoadDataToFile(Constants.WeatherCardsFile);
            
            var weatherResponses = await _httpService.GetWeather(Constants.WeatherLocations);
            await _bootstrap.WeatherConfig.UpdateWeatherCard(weatherResponses);
        }

        private void CreateWeatherCardsDataConfig()
        {
            
            var cardsFilePath = Path.Combine(Application.persistentDataPath, Constants.WeatherCardsFile);
            
            if (!File.Exists(cardsFilePath))
            {
                File.Create(cardsFilePath).Dispose();
                
                _bootstrap.WeatherConfig = new WeatherConfig().InitializeDefaultCards();
            }
        }

        private void CreatePanelsDataConfig()
        {
            var panelsStatesFilePath = Path.Combine(Application.persistentDataPath, Constants.PanelsStateFile);
                        
            if (!File.Exists(panelsStatesFilePath))
            {
                File.Create(panelsStatesFilePath).Dispose();

                _bootstrap.PanelsStateConfig = new PanelsStateConfig().InitializeDefaultValues();
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