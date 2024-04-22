using Core.Services;
using UnityEngine;
using Zenject;

namespace Core.Installers
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