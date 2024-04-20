using Core.Data;
using UnityEngine;

namespace Core.Bootstrapper
{
    public class Bootstrap : MonoBehaviour
    {
        public WeatherConfig WeatherConfig { get; set; }
        
        public PanelsStateConfig PanelsStateConfig { get; set; }
    }
}