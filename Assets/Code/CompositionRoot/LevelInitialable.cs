using Code.Services.ClientsService;
using Code.Services.Input;
using Code.Services.PopupService;
using Code.Services.TutorialService;
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
    
    [Header("Tutorial")]
    [SerializeField] TutorialService _tutorialService;
    
    private IJoystickProvider _joystickProvider;
    private IPopupContainerProvider _popupContainerProvider;
    private IPlayerProvider _playerProvider;
    private IClientsServiceProvider _clientsServiceProvider;
    private ITutorialServiceProvider _tutorialServiceProvider;

    [Inject]
    private void Construct(IJoystickProvider joystickProvider, IPopupContainerProvider popupContainerProvider,
    IPlayerProvider playerProvider, IClientsServiceProvider clientsServiceProvider, 
    ITutorialServiceProvider tutorialServiceProvider)
    {
        _clientsServiceProvider = clientsServiceProvider;
        _playerProvider = playerProvider;
        _popupContainerProvider = popupContainerProvider;
        _joystickProvider = joystickProvider;
        _tutorialServiceProvider = tutorialServiceProvider;
    }
    
    public void Initialize()
    {
        _joystickProvider.Set(_joystick);
        _popupContainerProvider.Set(_popupContainer);
        _playerProvider.Set(_chef);
        _clientsServiceProvider.Set(_clientService);
        _tutorialServiceProvider.Set(_tutorialService);
    }
}
