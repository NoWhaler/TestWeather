using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using File = System.IO.File;

namespace Core.Data
{
    public class WeatherConfig
    {
        public List<WeatherCard> WeatherCards { get; set; }

        public WeatherConfig InitializeDefaultCards()
        {
            List<WeatherCard> weatherCards = new List<WeatherCard>();

            foreach (var location in Constants.WeatherLocations)
            {
                var card = new WeatherCard()
                {
                    IsVisible = true,
                    Name = location
                };

                weatherCards.Add(card);
            }

            WeatherCards = new List<WeatherCard>(weatherCards);

            return this;
        }

        public UniTask UpdateWeatherCard(List<WeatherResponse> weatherResponses)
        {
            for (int i = 0; i < WeatherCards.Count; i++)
            {
                WeatherCards[i].Main = weatherResponses[i].Main;
                WeatherCards[i].Weather = weatherResponses[i].Weather;
            }

            return UniTask.CompletedTask;
        }

        public async UniTask SaveDataToFile()
        {
            var filePath = Path.Combine(Application.persistentDataPath, Constants.WeatherCardsFile);

            await ClearData(filePath);

            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            await File.WriteAllTextAsync(filePath, json);
        }

        public async UniTask ClearData(string filePath)
        {
            await File.WriteAllTextAsync(filePath, String.Empty);
        }
    }
}