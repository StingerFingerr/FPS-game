using Character;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LocationInstaller: MonoInstaller
    {
        [SerializeField] private CharacterControllerScript player;
        public override void InstallBindings()
        {
            BindPlayer();
        }

        private void BindPlayer()
        {
            Container
                .Bind<CharacterControllerScript>()
                .FromInstance(player)
                .AsSingle();
        }
    }
}