using Services.Input;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputService();
        }

        private void BindInputService()
        {
            Container
                .Bind<IInputService>()
                .FromInstance(new InputService())
                .AsSingle();
        }
    }
}