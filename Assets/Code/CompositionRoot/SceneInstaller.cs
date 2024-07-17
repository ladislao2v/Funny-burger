using Code.Configs;
using Code.Movement;
using Code.Services.Input;
using Code.Units;
using Plugins.StateMachine.StateFactory;
using UnityEngine;
using Zenject;

namespace Code.CompositionRoot
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [Header("Configs")]
        
        [Header("UI")]
        [SerializeField] private Joystick _joystick;

        [Header("Units")] 
        [SerializeField] private Chef _chef;

        public override void InstallBindings()
        {
            BindJoystick();
            BindInputService();
            BindPlayer();
            BindPlayerRouter();
            BindStateFactory();
            BindStateMachine();
        }

        private void BindPlayerRouter()
        {
            Container
                .BindInterfacesAndSelfTo<FixedPlayerRouter>()
                .AsCached();
        }

        private void BindPlayer()
        {
            Container
                .BindInterfacesAndSelfTo<Chef>()
                .FromInstance(_chef)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ChefInitializer>()
                .AsCached();
        }

        private void BindInputService()
        {
            Container
                .BindInterfacesAndSelfTo<JoystickInput>()
                .AsCached();
        }

        private void BindJoystick()
        {
            Container
                .Bind<Joystick>()
                .FromInstance(_joystick)
                .AsSingle();
        }
        
        
        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<Plugins.StateMachine.Core.StateMachine>().AsSingle();
        }

        private void BindStateFactory()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
        }
    }
}