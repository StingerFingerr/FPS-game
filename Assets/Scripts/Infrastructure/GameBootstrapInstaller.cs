using Services.Input;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapInstaller: MonoInstaller
    {
        [SerializeField] private InputService input;
        public override void InstallBindings()
        {
            BindInputService();
        }

        private void BindInputService()
        {
            Container
                .Bind<IInputService>()
                .To<InputService>()
                .FromComponentInNewPrefab(input)
                .AsSingle();
        }
    }
}