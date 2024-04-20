using System;
using System.Collections.Generic;

namespace Core.Data
{
    [Serializable]
    public class WeatherData
    {
        public string Icon { get; set; }
    }
    
    [Serializable]
    public class WeatherResponse
    {
        public string Name { get; set; }
        
        public WeatherMain Main { get; set; }
        
        public List<WeatherData> Weather { get; set; }
    }

    [Serializable]
    public class WeatherMain
    {
        public float Temp { get; set; }
    }
    
    [Serializable]
    public class WeatherCard: WeatherResponse
    {
        public bool IsVisible { get; set; }
    }
}