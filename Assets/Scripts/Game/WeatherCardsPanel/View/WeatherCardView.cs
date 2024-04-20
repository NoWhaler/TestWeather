using System;
using System.Collections;
using System.Globalization;
using Core;
using Core.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.WeatherCardsPanel.View
{
    public class WeatherCardView : MonoBehaviour
    {
        [field: SerializeField] public WeatherCard WeatherCard { get; set; }

        [SerializeField] private Button _button;

        [SerializeField] private Image _weatherImage;

        [SerializeField] private TMP_Text _locationText;

        [SerializeField] private TMP_Text _temperatureText;

        private void OnEnable()
        {
            _button.onClick.AddListener(DisableView
                );
        }

        private void DisableView()
        {
            WeatherCard.IsVisible = false;
            gameObject.SetActive(false);
        }
        
        public void SetView()
        {
            if (!WeatherCard.IsVisible)
            {
                gameObject.SetActive(false);
            }
            
            _locationText.text = WeatherCard.Name;
            _temperatureText.text = WeatherCard.Main.Temp.ToString(CultureInfo.InvariantCulture);

            StartCoroutine(GetSprite(WeatherCard.Weather[0].Icon, (sprite) =>
            {
                if (sprite != null)
                {
                    _weatherImage.sprite = sprite;
                }
            }));
        }
        
        private IEnumerator GetSprite(string icon, Action<Sprite> callback)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture($"http://openweathermap.org/img/wn/{icon}@2x.png");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) 
            {
                Debug.Log(www.error);
                callback?.Invoke(null);
            }
            else
            {
                Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                
                Sprite sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);
                
                callback?.Invoke(sprite);
            }
        }
    }
}