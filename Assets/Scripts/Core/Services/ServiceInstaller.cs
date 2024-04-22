using UnityEngine;
using Zenject;

namespace Core.Services
{
    public class ServiceInstaller: MonoInstaller
    {
        [SerializeField] private HttpService _httpService;

        public override void InstallBindings()
        {
            Container.Bind<HttpService>().FromInstance(_httpService).AsSingle().NonLazy();
        }
    }
}