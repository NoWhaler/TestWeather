using Core.Bootstrapper;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class BootstrapperInstaller: MonoInstaller
    {
        [SerializeField] private Bootstrap _bootstrap;
        
        public override void InstallBindings()
        {
            Container.Bind<Bootstrap>().FromInstance(_bootstrap).AsSingle().NonLazy();
        }
    }
}