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
    public sealed class SceneInstaller : MonoInstaller, IInitializable
    {
        [Header("UI")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PopupContainer _popupContainer;

        [Header("Units")] 
        [SerializeField] private Chef _chef;
        [SerializeField] private ClientsService _clientService;

        public override void InstallBindings() { }

        public void Initialize()
        {
            Container.Resolve<IJoystickProvider>().Set(_joystick);
            Container.Resolve<IPopupContainerProvider>().Set(_popupContainer);
            Container.Resolve<IPlayerProvider>().Set(_chef);
            Container.Resolve<IClientsServiceProvider>().Set(_clientService);
        }
    }
}