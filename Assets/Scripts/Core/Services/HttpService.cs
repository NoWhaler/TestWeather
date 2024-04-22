﻿using System;
using System.Collections.Generic;
using CI.HttpClient;
using Core.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Services
{
    public class HttpService: MonoBehaviour
    {
        public async UniTask<List<WeatherResponse>> GetWeather(string[] locations)
        {
            var client = new HttpClient();
        
            List<WeatherResponse> weatherResponses = new List<WeatherResponse>();
        
            foreach (var location in locations)
            {
                HttpResponseMessage response = await client.GetAsync(new Uri(
                    $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid=25670c4aca0306eff7ea6db60c890493"));
        
                if (response.IsSuccessStatusCode)
                {
                    weatherResponses.Add(response.ReadAsJson<WeatherResponse>());
                }
            }
        
            return weatherResponses;
        }
    }
}