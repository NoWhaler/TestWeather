using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Bootstrapper;
using Core.Data;
using Core.Services;
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

        [Inject] [SerializeField] private Bootstrap _bootstrap;
        
        private void Awake()
        {
            _loadButton.onClick.AddListener(() => 
                StartCoroutine(LoadSecondSceneCo())
                );
        }

        private async Task LoadData()
        {
            var filePath = Path.Combine(Application.streamingAssetsPath, Constants.WeatherCardsFile);
                        
            byte[] data = null;
            #if UNITY_ANDROID && !UNITY_EDITOR
                            
                using (UnityWebRequest www = UnityWebRequest.Get(filePath))
                {
                    www.SendWebRequest();
                    while (!www.isDone) { }
                    data = www.downloadHandler.data;
                }
            #else
                data = await File.ReadAllBytesAsync(filePath);
            #endif

            string loadedJsonString = Encoding.UTF8.GetString(data);

            _bootstrap.WeatherConfig = null;
            
            try
            {
                _bootstrap.WeatherConfig = JsonConvert.DeserializeObject<WeatherConfig>(loadedJsonString);

                if (_bootstrap.WeatherConfig == null)
                {
                    _bootstrap.WeatherConfig = new WeatherConfig().InitializeDefaultCards();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to deserialize items from JSON: " + e.Message);
                
                _bootstrap.WeatherConfig = new WeatherConfig().InitializeDefaultCards();
            }
            
            var weatherResponses = await HttpService.GetWeather(_bootstrap.WeatherConfig.WeatherCards.
                Select(x => x.Name).ToArray())!;

            if (_bootstrap.WeatherConfig.WeatherCards.Count == weatherResponses.Count)
            {
                await _bootstrap.WeatherConfig.UpdateWeatherCard(weatherResponses);
            }
        }

        private void LoadPanelsData()
        {
            var filePath = Path.Combine(Application.streamingAssetsPath, Constants.PanelsStateFiles);
                                    
            byte[] data = null;
            #if UNITY_ANDROID && !UNITY_EDITOR
                            
                using (UnityWebRequest www = UnityWebRequest.Get(filePath))
                {
                    www.SendWebRequest();
                    while (!www.isDone) { }
                    data = www.downloadHandler.data;
                }
            #else
                data = File.ReadAllBytes(filePath);
            #endif

            string loadedJsonString = Encoding.UTF8.GetString(data);

            _bootstrap.PanelsStateConfig = null;
            
            try
            {
                _bootstrap.PanelsStateConfig = JsonConvert.DeserializeObject<PanelsStateConfig>(loadedJsonString);

                if (_bootstrap.PanelsStateConfig == null)
                {
                    _bootstrap.PanelsStateConfig = new PanelsStateConfig().InitializeDefaultValues();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to deserialize items from JSON: " + e.Message);
                
                _bootstrap.PanelsStateConfig = new PanelsStateConfig().InitializeDefaultValues();
            }
        }
        
        private IEnumerator LoadSecondSceneCo()
        {
            LoadPanelsData();
            Task task = LoadData();
            
            yield return new WaitUntil(() => task.IsCompleted);
            
            AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            
            operation.allowSceneActivation = false;
            
            while (!operation.isDone)
            {
                _loadingBar.UpdateFraction(operation.progress);
                
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }
                
                operation.allowSceneActivation = true;
                
                yield return null;
            }
        }
    }
}