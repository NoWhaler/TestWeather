using UnityEngine;
using Zenject;

namespace Core.Bootstrapper
{
    public class BootstrapperInstaller: MonoInstaller
    {
        [SerializeField] private Bootstrap bootstrap;
        
        public override void InstallBindings()
        {
            Container.Bind<Bootstrap>().FromInstance(bootstrap).AsSingle().NonLazy();
        }
    }
}