using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task UpdateWeatherCard(List<WeatherResponse> weatherResponses)
        {
            for (int i = 0; i < WeatherCards.Count; i++)
            {
                WeatherCards[i].Main = weatherResponses[i].Main;
                WeatherCards[i].Weather = weatherResponses[i].Weather;
            }

            return Task.CompletedTask;
        }

        public async Task SaveDataToFile(string filePath)
        {
            await ClearData(filePath);
            
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task ClearData(string filePath)
        {
            await File.WriteAllTextAsync(filePath, String.Empty);
        }
    }
    
    public static class Constants
    {
        public static readonly string[] WeatherLocations = { "London", "Kyiv", "New York", "Tokyo", "Paris", "Berlin", "Sydney", "Texas", "Boryspil" };
        
        public static readonly string WeatherCardsFile = "weatherCards.json";

        public static readonly string PanelsStateFiles = "panelsState.json";
    }
}