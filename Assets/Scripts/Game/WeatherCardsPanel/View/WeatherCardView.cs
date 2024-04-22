using System;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;
using Core;
using Core.Bootstrapper;
using Core.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening.Plugins.Core.PathCore;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;
using Path = System.IO.Path;

namespace Game.WeatherCardsPanel.View
{
    public class WeatherCardView : MonoBehaviour
    {
        [field: SerializeField] public WeatherCard WeatherCard { get; set; }

        [SerializeField] private Button _button;

        [SerializeField] private Image _weatherImage;

        [SerializeField] private TMP_Text _locationText;

        [SerializeField] private TMP_Text _temperatureText;

        [Inject] private Bootstrap _bootstrap;

        private void OnEnable()
        {
            _button.onClick.AddListener(DisableView);
        }

        private async void DisableView()
        {
            WeatherCard.IsVisible = false;
            gameObject.SetActive(false);

            await _bootstrap.WeatherConfig.SaveDataToFile();
        }
        
        public async UniTask SetView()
        {
            if (!WeatherCard.IsVisible)
            {
                gameObject.SetActive(false);
            }
            
            _locationText.text = WeatherCard.Name;
            _temperatureText.text = WeatherCard.Main.Temp.ToString();
            
            _weatherImage.sprite = await GetSprite(WeatherCard.Weather[0].Icon);
        }
        
        private async UniTask<Sprite> GetSprite(string icon)
        {
            Sprite sprite = null;
            
            UnityWebRequest www = UnityWebRequestTexture.GetTexture($"http://openweathermap.org/img/wn/{icon}@2x.png");
            await www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) 
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                
                sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);
            }

            return sprite;
        }
    }
}