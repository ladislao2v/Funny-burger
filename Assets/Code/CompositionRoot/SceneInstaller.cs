using Code.Movement;
using Code.Services.ClientsService;
using Code.Services.Input;
using Code.Services.PopupService;
using Code.Units;
using Plugins.StateMachine.StateFactory;
using UnityEngine;
using Zenject;

namespace Code.CompositionRoot
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [Header("UI")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PopupContainer _popupContainer;

        [Header("Units")] 
        [SerializeField] private Chef _chef;
        [SerializeField] private ClientsService _clientService;

        public override void InstallBindings()
        {
            BindJoystick();
            BindInputService();
            BindPlayer();
            BindPlayerRouter();
            BindClientService();
            BindPopupService();
            BindStateFactory();
            BindStateMachine();
        }

        private void BindClientService()
        {
            Container.BindInterfacesAndSelfTo<ClientsService>().FromInstance(_clientService).AsSingle();
        }

        private void BindPopupService()
        {
            Container.BindInterfacesAndSelfTo<PopupContainer>().FromInstance(_popupContainer).AsSingle();
            Container.BindInterfacesAndSelfTo<PopupService>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<Plugins.StateMachine.Core.StateMachine>().AsCached();
        }

        private void BindStateFactory()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
        }
    }
}