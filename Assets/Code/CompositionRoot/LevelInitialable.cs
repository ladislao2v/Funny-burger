using Code.Services.ClientsService;
using Code.Services.Input;
using Code.Services.PopupService;
using Code.Units;
using UnityEngine;
using Zenject;

public sealed class LevelInitialable : MonoBehaviour, IInitializable
{
    [Header("UI")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private PopupContainer _popupContainer;

    [Header("Units")] 
    [SerializeField] private Chef _chef;
    [SerializeField] private ClientsService _clientService;
    
    private IJoystickProvider _joystickProvider;
    private IPopupContainerProvider _popupContainerProvider;
    private IPlayerProvider _playerProvider;
    private IClientsServiceProvider _clientsServiceProvider;

    [Inject]
    private void Construct(IJoystickProvider joystickProvider, IPopupContainerProvider popupContainerProvider,
    IPlayerProvider playerProvider, IClientsServiceProvider clientsServiceProvider)
    {
        _clientsServiceProvider = clientsServiceProvider;
        _playerProvider = playerProvider;
        _popupContainerProvider = popupContainerProvider;
        _joystickProvider = joystickProvider;
    }
    
    public void Initialize()
    {
        _joystickProvider.Set(_joystick);
        _popupContainerProvider.Set(_popupContainer);
        _playerProvider.Set(_chef);
        _clientsServiceProvider.Set(_clientService);
    }
}
